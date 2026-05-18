using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using F1Manager.Modele;
using F1Manager.Servicii;

namespace F1Manager.Formulare
{
    public partial class ChampionshipForm : Form
    {
        private ClasamentService clasamentService = new ClasamentService();
        private List<Clasament> topPiloti = new List<Clasament>();
        private List<Clasament> topEchipe = new List<Clasament>();
        private System.Windows.Forms.Timer animationTimer;
        private int animationStep = 0;
        private bool showingPilot = true;
        private bool started = false;
        private bool transitioning = false;
        private int transitionFade = 0;

        // Animation phase: 0=inactive, 1=3rd place rising, 2=2nd place rising, 3=1st place rising, 4=complete
        private int phase = 1;
        // Progress of current phase (0.0 to 1.0)
        private float phaseProgress = 0f;

        public ChampionshipForm()
        {
            InitializeComponent();
            this.Load += ChampionshipForm_Load;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
        }

        private void ChampionshipForm_Load(object sender, EventArgs e)
        {
            topPiloti = clasamentService.GetClasamentGeneral();
            topEchipe = clasamentService.GetClasamentEchipe();

            if (topPiloti.Count == 0 && topEchipe.Count == 0)
            {
                MessageBox.Show("Nu există date de clasament.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            this.BackColor = Color.FromArgb(18, 18, 18);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;

            phase = 1;
            phaseProgress = 0f;

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 20;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            animationStep++;
            if (!started && animationStep > 2) started = true;

            if (transitioning)
            {
                transitionFade += 5;
                if (transitionFade >= 100)
                {
                    transitioning = false;
                    transitionFade = 0;
                    animationStep = 0;
                    phase = 1;
                    phaseProgress = 0f;
                    started = true;
                }
                this.Invalidate();
                return;
            }

            if (phase <= 3)
            {
                phaseProgress += 0.025f;
                if (phaseProgress >= 1.0f)
                {
                    phaseProgress = 1.0f;
                    phase++;
                }
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            float alpha = transitioning ? transitionFade / 100f : 1.0f;

            if (transitioning && alpha < 0.5f)
            {
                DrawBackground(g);
            }

            if (showingPilot)
            {
                DrawPodium(g, centerX, centerY, "🏆 CLASAMENT FINAL PILOȚI 🏆", topPiloti, true, alpha);
            }
            else
            {
                DrawPodium(g, centerX, centerY, "🏆 CLASAMENT FINAL ECHIPE 🏆", topEchipe, false, alpha);
            }
        }

        private void DrawBackground(Graphics g)
        {
            using (var brush = new LinearGradientBrush(
                new Point(0, 0), new Point(0, this.ClientSize.Height),
                Color.FromArgb(18, 18, 25), Color.FromArgb(35, 18, 18)))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void DrawPodium(Graphics g, int cx, int cy, string title, List<Clasament> list, bool isPiloti, float opacity)
        {
            DrawBackground(g);

            int alpha = (int)(opacity * 255);
            if (alpha > 255) alpha = 255;

            // Confetti
            if (opacity >= 0.5f && phase >= 4)
            {
                for (int i = 0; i < 50; i++)
                {
                    int x = (i * 37 + 13) % this.ClientSize.Width;
                    int y = (i * 29 + 7) % (this.ClientSize.Height / 2);
                    int size = 4 + (i % 5);
                    using (var b = new SolidBrush(Color.FromArgb(alpha, partyColors[i % partyColors.Length])))
                        g.FillEllipse(b, x, y, size, size);
                }
            }

            // Title
            using (Font tf = new Font("Segoe UI", 26, FontStyle.Bold, GraphicsUnit.Point))
            {
                SizeF sz = g.MeasureString(title, tf);
                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 255, 215, 0)))
                    g.DrawString(title, tf, b, cx - sz.Width / 2, 30);
            }

            // Podium base line
            int baseY = cy + 50;
            int w = 240;
            int gap = 25;
            int totalW = w * 3 + gap * 2;
            int startX = cx - totalW / 2;

            // Heights: 3rd=130, 2nd=170, 1st=220
            int[] heights = { 220, 170, 130 };
            // Screen order: 2nd[1] left, 1st[0] center, 3rd[2] right
            int[] orderIndices = { 1, 0, 2 };
            string[] medals = { "🥇", "🥈", "🥉" };
            string[] posTexts = { "#1", "#2", "#3" };
            Color[] medalColors = {
                Color.Gold,
                Color.Silver,
                Color.FromArgb(205, 127, 50)
            };

            for (int step = 0; step < 3; step++)
            {
                int posIdx = orderIndices[step];
                if (posIdx >= list.Count) continue;

                // Animation order: 1st first -> 2nd -> 3rd
                // posIdx=0 (1st) appears in phase 1
                // posIdx=1 (2nd) appears in phase 2
                // posIdx=2 (3rd) appears in phase 3
                int animOrder = posIdx + 1; // 1st=phase1, 2nd=phase2, 3rd=phase3

                bool visible = phase > animOrder || (phase == animOrder);

                float animScale = 0f;
                if (phase > animOrder) animScale = 1f;
                else if (phase == animOrder) animScale = EaseOutBounce(phaseProgress);

                if (!visible || animScale <= 0f) continue;

                int px = startX + step * (w + gap);
                int h = heights[posIdx];
                int podiumTop = baseY - (int)(h * animScale);
                int currentH = (int)(h * animScale);

                if (currentH < 5) continue;

                // Medal colors per actual position
                Color medColor = medalColors[posIdx];

                // Podium block
                Rectangle r = new Rectangle(px, podiumTop, w, currentH);
                using (var pb = new LinearGradientBrush(
                    new Point(px, podiumTop), new Point(px, podiumTop + currentH),
                    Color.FromArgb(alpha, Darken(medColor, 0.4f)),
                    Color.FromArgb(alpha, medColor)))
                {
                    g.FillRectangle(pb, r);
                }
                using (Pen p = new Pen(Color.FromArgb(alpha, medColor), 2))
                    g.DrawRectangle(p, r);

                // Position number on podium
                using (Font pf = new Font("Segoe UI", 20, FontStyle.Bold, GraphicsUnit.Point))
                {
                    string pt = posTexts[posIdx];
                    SizeF sz = g.MeasureString(pt, pf);
                    float tx = px + w / 2 - sz.Width / 2;
                    float ty = podiumTop + currentH - 45;
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                        g.DrawString(pt, pf, b, tx, ty);
                }

                // Medal above position
                using (Font mf = new Font("Segoe UI Emoji", 28, FontStyle.Regular, GraphicsUnit.Point))
                {
                    SizeF sz = g.MeasureString(medals[posIdx], mf);
                    float tx = px + w / 2 - sz.Width / 2;
                    float ty = podiumTop + currentH - 80;
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                        g.DrawString(medals[posIdx], mf, b, tx, ty);
                }

                // Name ABOVE the podium - fixed spacing
                string name = isPiloti ? (list[posIdx].NumePilot ?? "?") : (list[posIdx].NumeEchipa ?? "?");
                float nameAboveY = podiumTop - 5;

                using (Font nf = new Font("Segoe UI", isPiloti ? 17 : 15, FontStyle.Bold, GraphicsUnit.Point))
                {
                    // Wrap long names
                    string displayName = name;
                    if (g.MeasureString(displayName, nf).Width > w - 10)
                    {
                        if (displayName.Length > 16) displayName = displayName.Substring(0, 15) + "…";
                    }
                    SizeF sz = g.MeasureString(displayName, nf);
                    float tx = px + w / 2 - sz.Width / 2;
                    float ty = nameAboveY - sz.Height;

                    // Shadow
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha * 60 / 255, 0, 0, 0)))
                        g.DrawString(displayName, nf, sb, tx + 1, ty + 1);
                    // Text
                    using (SolidBrush nb = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                        g.DrawString(displayName, nf, nb, tx, ty);
                }

                // Team name under the driver name (for pilots)
                if (isPiloti && !string.IsNullOrEmpty(list[posIdx].NumeEchipa))
                {
                    string team = list[posIdx].NumeEchipa;
                    using (Font tf2 = new Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point))
                    {
                        if (g.MeasureString(team, tf2).Width > w - 10)
                        {
                            if (team.Length > 18) team = team.Substring(0, 17) + "…";
                        }
                        SizeF sz = g.MeasureString(team, tf2);
                        float tx = px + w / 2 - sz.Width / 2;
                        float ty = nameAboveY - sz.Height - 22;
                        using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 200, 200, 200)))
                            g.DrawString(team, tf2, b, tx, ty);
                    }
                }

                // Points label - on the podium face
                using (Font pf2 = new Font("Segoe UI", 12, FontStyle.Bold, GraphicsUnit.Point))
                {
                    string pts = $"{list[posIdx].Puncte} P";
                    SizeF sz = g.MeasureString(pts, pf2);
                    float tx = px + w / 2 - sz.Width / 2;
                    float ty = podiumTop + 8;
                    if (ty + sz.Height < podiumTop + currentH - 85)
                    {
                        using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                            g.DrawString(pts, pf2, b, tx, ty);
                    }
                }
            }

            // Instruction
            string instr;
            if (phase < 4)
                instr = "Se anunță câștigătorii... 🏁";
            else if (showingPilot)
                instr = "Apasă SPACE, ENTER sau click pentru a vedea clasamentul echipelor...";
            else
                instr = "Apasă SPACE, ENTER sau click pentru a reseta sezonul!";

            using (Font inf = new Font("Segoe UI", 13, FontStyle.Italic, GraphicsUnit.Point))
            {
                SizeF sz = g.MeasureString(instr, inf);
                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 150, 150, 150)))
                    g.DrawString(instr, inf, b, cx - sz.Width / 2, this.ClientSize.Height - 60);
            }
        }

        private float EaseOutBounce(float t)
        {
            // Bounce easing function
            if (t < 0.5f) return 4f * t * t * t;
            float f = 2f * t - 2f;
            return 1f + 0.5f * f * f * f;
        }

        private Color Darken(Color c, float factor)
        {
            return Color.FromArgb(
                (int)(c.R * (1 - factor)),
                (int)(c.G * (1 - factor)),
                (int)(c.B * (1 - factor))
            );
        }

        private static Color[] partyColors = new Color[]
        {
            Color.Gold, Color.Red, Color.DodgerBlue, Color.LimeGreen,
            Color.Purple, Color.Orange, Color.Cyan, Color.HotPink,
            Color.Yellow, Color.White
        };

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (phase >= 4 && !transitioning) NextStep();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (phase >= 4 && !transitioning) NextStep();
        }

        private void NextStep()
        {
            if (showingPilot)
            {
                showingPilot = false;
                transitioning = true;
                transitionFade = 0;
                started = false;
                animationStep = 0;
                phase = 1;
                phaseProgress = 0f;
            }
            else
            {
                animationTimer.Stop();
                this.Hide();

                DialogResult result = MessageBox.Show(
                    "🏆 SEZONUL S-A ÎNCHEIAT! 🏆\n\n" +
                    "Toate cursele au fost finalizate.\n\n" +
                    "🥇 Campionul la Piloți: " + (topPiloti.Count > 0 ? topPiloti[0].NumePilot : "-") + "\n" +
                    "🥇 Campioana la Echipe: " + (topEchipe.Count > 0 ? topEchipe[0].NumeEchipa : "-") + "\n\n" +
                    "Punctele se resetează și se adaugă un calendar F1 complet!\n" +
                    "Un nou sezon va începe!",
                    "🏆 Sezon Încheiat!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    clasamentService.ResetSezon();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}