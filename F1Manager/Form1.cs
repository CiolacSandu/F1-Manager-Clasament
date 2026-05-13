using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using F1Manager.Formulare;
using F1Manager.Servicii;

namespace F1Manager;

public partial class Form1 : Form
{
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 0x2;

    private readonly Color _bgDark = Color.FromArgb(7, 7, 7);
    private readonly Color _bgPanel = Color.FromArgb(12, 12, 12);
    private readonly Color _bgCard = Color.FromArgb(22, 22, 22);
    private readonly Color _f1Red = Color.FromArgb(225, 6, 0);
    private readonly Color _textWhite = Color.White;
    private readonly Color _textGray = Color.FromArgb(150, 150, 150);
    private readonly Color _textDim = Color.FromArgb(80, 80, 80);
    private readonly Color _gold = Color.FromArgb(255, 215, 0);
    private readonly Color _silver = Color.FromArgb(192, 192, 192);
    private readonly Color _bronze = Color.FromArgb(205, 127, 50);

    private Panel _titleBar, _mainContainer, _leftPanel, _leftContent, _rightPanel;
    private Panel _panelTopCard, _panelActions, _panelInfo;
    private Panel _panelTop1, _panelTop2, _panelTop3;
    private Panel _panelGlow, _logoCircle, _redLine;
    private Label _titleMain, _subtitle, _description, _lblTopTitle;
    private Panel _statsRow, _bottomDecor;
    private Button _btnLogin, _btnRegister, _btnClose, _btnMinimize;
    private Label _lblPiloti, _lblEchipe, _lblCurse, _lblPuncte;
    private Label _lblTop1Nume, _lblTop1Echipa, _lblTop1Puncte;
    private Label _lblTop2Nume, _lblTop2Echipa, _lblTop2Puncte;
    private Label _lblTop3Nume, _lblTop3Echipa, _lblTop3Puncte;
    private Button _btnClasamentPiloti, _btnClasamentEchipe, _btnCalendar;
    private Label _lblInfoTitle, _lblInfoSub;

    private readonly UserService _userService;
    private readonly PilotService _pilotService;
    private readonly EchipaService _echipaService;
    private readonly ClasamentService _clasamentService;
    private System.Windows.Forms.Timer _animTimer;
    private float _animOffset = 0;

    public Form1()
    {
        _userService = new UserService();
        _pilotService = new PilotService();
        _echipaService = new EchipaService();
        _clasamentService = new ClasamentService();

        InitializeComponent();
        BuildBase();
        BuildLeft();
        BuildRight();
        DoLayout();
        LoadStats();

        _animTimer = new System.Windows.Forms.Timer { Interval = 40 };
        _animTimer.Tick += (s, e) => { _animOffset += 0.04f; _panelGlow?.Invalidate(); _bottomDecor?.Invalidate(); };
        _animTimer.Start();
        this.Resize += (s, e) => DoLayout();
    }

    private void BuildBase()
    {
        this.BackColor = _bgDark;
        this.DoubleBuffered = true;

        _titleBar = new Panel
        {
            Height = 36, Dock = DockStyle.Top, BackColor = Color.FromArgb(4, 4, 4), Cursor = Cursors.SizeAll
        };
        _titleBar.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); };

        Label tl = new Label { Text = "  F1 MANAGER", ForeColor = _f1Red, Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(12, 6), AutoSize = true, BackColor = Color.Transparent };
        _titleBar.Controls.Add(tl);

        _btnClose = new Button { Text = "✕", FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0 }, ForeColor = _textGray, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10), Size = new Size(42, 28), Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand };
        _btnClose.FlatAppearance.MouseOverBackColor = _f1Red;
        _btnClose.Click += (s, e) => Application.Exit();

        _btnMinimize = new Button { Text = "─", FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0 }, ForeColor = _textGray, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10), Size = new Size(42, 28), Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand };
        _btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
        _btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

        _titleBar.Controls.Add(_btnMinimize);
        _titleBar.Controls.Add(_btnClose);

        _mainContainer = new Panel { Dock = DockStyle.Fill, BackColor = _bgDark };
        _leftPanel = new Panel { BackColor = _bgPanel };
        _leftContent = new Panel { BackColor = Color.Transparent };
        _rightPanel = new Panel { BackColor = Color.Transparent };

        _panelGlow = new Panel { BackColor = Color.Transparent, Enabled = false };
        _panelGlow.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float a = 12 + (float)(Math.Sin(_animOffset) * 7);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, _panelGlow.Width, _panelGlow.Height);
                using (PathGradientBrush br = new PathGradientBrush(gp))
                {
                    br.CenterColor = Color.FromArgb((int)a, _f1Red);
                    br.SurroundColors = new[] { Color.Transparent };
                    br.CenterPoint = new PointF(_panelGlow.Width / 2, _panelGlow.Height / 2);
                    e.Graphics.FillEllipse(br, 0, 0, _panelGlow.Width, _panelGlow.Height);
                }
            }
        };

        this.Controls.Add(_titleBar);
        this.Controls.Add(_mainContainer);
        this.Controls.Add(_panelGlow);
        _mainContainer.Controls.Add(_leftPanel);
        _mainContainer.Controls.Add(_rightPanel);
    }

    private void BuildLeft()
    {
        _logoCircle = new Panel { BackColor = Color.Transparent };
        _logoCircle.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int sz = _logoCircle.Width - 2;
            using (Brush b = new SolidBrush(_f1Red))
                e.Graphics.FillEllipse(b, 0, 0, sz, sz);
            using (Brush b = new SolidBrush(Color.White))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Graphics.DrawString("F1", new Font("Segoe UI Black", _logoCircle.Width / 3), b, new Rectangle(0, 0, sz, sz), sf);
        };

        _titleMain = new Label { Text = "F1 MANAGER", ForeColor = _textWhite, Font = new Font("Segoe UI Black", 48, FontStyle.Bold), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Transparent };
        _redLine = new Panel { BackColor = _f1Red };
        _subtitle = new Label { Text = "SISTEM COMPLET DE MANAGEMENT PENTRU FORMULA 1", ForeColor = _textGray, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Transparent };
        _description = new Label { Text = "Gestionează piloți, echipe, curse și clasamente în timp real.\nPlatforma ta completă pentru administrarea sezonului F1.", ForeColor = _textGray, Font = new Font("Segoe UI", 11), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Transparent };

        _btnLogin = AccentBtn("AUTENTIFICARE");
        _btnLogin.Click += (s, e) => OpenLoginForm();
        _btnRegister = OutlineBtn("ÎNREGISTRARE");
        _btnRegister.Click += (s, e) => OpenRegisterForm();

        _statsRow = new Panel { BackColor = Color.Transparent };
        _statsRow.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int sw = _statsRow.Width / 4;
            using (Pen p = new Pen(Color.FromArgb(40, 40, 40), 1))
            {
                e.Graphics.DrawLine(p, sw, 10, sw, _statsRow.Height - 10);
                e.Graphics.DrawLine(p, sw * 2, 10, sw * 2, _statsRow.Height - 10);
                e.Graphics.DrawLine(p, sw * 3, 10, sw * 3, _statsRow.Height - 10);
            }
        };

        _lblPiloti = StatLabel("0\nPiloți");
        _lblEchipe = StatLabel("0\nEchipe");
        _lblCurse = StatLabel("0\nCurse");
        _lblPuncte = StatLabel("0\nPuncte");
        _statsRow.Controls.Add(_lblPiloti);
        _statsRow.Controls.Add(_lblEchipe);
        _statsRow.Controls.Add(_lblCurse);
        _statsRow.Controls.Add(_lblPuncte);

        // Bottom decorative bar - rich F1 themed panel
        _bottomDecor = new Panel { BackColor = Color.Transparent };
        _bottomDecor.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int w = _bottomDecor.Width;
            int h = _bottomDecor.Height;
            if (w < 50 || h < 20) return;

            // Center point
            int cx = w / 2;
            int cy = h / 2;

            // === CHECKERED STRIP at top ===
            int sqSize = Math.Max(6, w / 80);
            int stripH = sqSize * 2;
            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < w / sqSize + 1; col++)
                {
                    bool isBlack = (row + col) % 2 == 0;
                    using (Brush b = new SolidBrush(isBlack ? Color.FromArgb(30, 30, 30) : Color.FromArgb(22, 22, 22)))
                        e.Graphics.FillRectangle(b, col * sqSize, row * sqSize + 4, sqSize, sqSize);
                }
            }

            // === F1 TRACK (curved road) ===
            int trackY = stripH + 8;
            int trackH = h - trackY - 8;
            int roadH = Math.Min(trackH - 4, 30);

            // Asphalt road
            int roadY = trackY + (trackH - roadH) / 2;
            using (Brush roadBrush = new SolidBrush(Color.FromArgb(25, 25, 28)))
            {
                e.Graphics.FillRectangle(roadBrush, 30, roadY, w - 60, roadH);
            }

            // Road borders (white lines)
            using (Pen whiteLine = new Pen(Color.FromArgb(60, 60, 60), 1))
            {
                e.Graphics.DrawLine(whiteLine, 30, roadY, w - 30, roadY);
                e.Graphics.DrawLine(whiteLine, 30, roadY + roadH, w - 30, roadY + roadH);
            }

            // Dashed center line
            using (Pen dashPen = new Pen(Color.FromArgb(50, 50, 50), 1))
            {
                dashPen.DashStyle = DashStyle.Dash;
                dashPen.DashPattern = new float[] { 12, 8 };
                e.Graphics.DrawLine(dashPen, 30, roadY + roadH / 2, w - 30, roadY + roadH / 2);
            }

            // === F1 CAR on track ===
            int carW = Math.Min(60, w / 10);
            int carH = Math.Min(20, roadH - 6);
            int carX = cx - carW / 2;
            int carY = roadY + (roadH - carH) / 2;

            // Car body
            using (Brush carBody = new SolidBrush(Color.FromArgb(180, 10, 5)))
            {
                e.Graphics.FillRectangle(carBody, carX, carY + 2, carW, carH - 4);
                // Nose cone
                e.Graphics.FillPolygon(carBody, new Point[] {
                    new Point(carX, carY + carH / 2),
                    new Point(carX - 8, carY + carH / 2 - 3),
                    new Point(carX - 8, carY + carH / 2 + 3)
                });
                // Rear wing
                e.Graphics.FillRectangle(carBody, carX + carW - 2, carY - 2, 4, carH + 4);
            }

            // Wheels
            using (Brush wheelBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                int wheelR = (carH - 2) / 2;
                e.Graphics.FillEllipse(wheelBrush, carX + 6, carY - 1, wheelR * 2, wheelR * 2);
                e.Graphics.FillEllipse(wheelBrush, carX + carW - 16, carY - 1, wheelR * 2, wheelR * 2);
            }

            // Wheel rims
            using (Pen rimPen = new Pen(Color.FromArgb(80, 80, 80), 1))
            {
                int wr = (carH - 2) / 2;
                e.Graphics.DrawEllipse(rimPen, carX + 6 + wr / 2, carY - 1 + wr / 2, wr, wr);
                e.Graphics.DrawEllipse(rimPen, carX + carW - 16 + wr / 2, carY - 1 + wr / 2, wr, wr);
            }

            // === SPEED TEXT ===
            int txtSize = Math.Max(7, Math.Min(11, h / 6));
            using (Brush txtBrush = new SolidBrush(Color.FromArgb(60, 60, 60)))
            {
                string speed = $"SPEED: 0 KM/H";
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(speed, new Font("Segoe UI", txtSize, FontStyle.Bold), txtBrush, w - 120, cy);
                }
            }

            // === LAP COUNTER ===
            using (Brush lapBrush = new SolidBrush(Color.FromArgb(55, 55, 55)))
            {
                string lapText = "LAP 0 / 0";
                e.Graphics.DrawString(lapText, new Font("Segoe UI", txtSize, FontStyle.Bold), lapBrush, new PointF(40, cy));
            }

            // === SMALL TIMING LIGHTS (like F1 start lights) ===
            int lightsY = roadY + roadH + 6;
            int lightCount = 5;
            int lightSpacing = Math.Min(18, (w - 60) / lightCount);
            int lightStartX = cx - (lightCount * lightSpacing) / 2;
            for (int i = 0; i < lightCount; i++)
            {
                int lx = lightStartX + i * lightSpacing;
                bool lit = i < (int)(_animOffset % 6);
                using (Brush lightBrush = new SolidBrush(lit ? Color.FromArgb(255, 50, 50) : Color.FromArgb(30, 10, 10)))
                {
                    e.Graphics.FillEllipse(lightBrush, lx, lightsY, 12, 12);
                }
                using (Pen lightPen = new Pen(lit ? Color.FromArgb(200, 30, 30) : Color.FromArgb(40, 20, 20), 1))
                {
                    e.Graphics.DrawEllipse(lightPen, lx, lightsY, 12, 12);
                }
            }
        };

        _leftContent.Controls.Add(_logoCircle);
        _leftContent.Controls.Add(_titleMain);
        _leftContent.Controls.Add(_redLine);
        _leftContent.Controls.Add(_subtitle);
        _leftContent.Controls.Add(_description);
        _leftContent.Controls.Add(_btnLogin);
        _leftContent.Controls.Add(_btnRegister);
        _leftContent.Controls.Add(_statsRow);
        _leftContent.Controls.Add(_bottomDecor);
        _leftPanel.Controls.Add(_leftContent);
    }

    private void BuildRight()
    {
        _panelTopCard = new Panel { BackColor = _bgCard };
        _panelTopCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen p = new Pen(Color.FromArgb(40, 40, 40)))
            using (GraphicsPath gp = RoundRect(_panelTopCard.ClientRectangle, 10))
                e.Graphics.DrawPath(p, gp);
        };

        _lblTopTitle = new Label { Text = "🏆  TOP 3 PILOȚI", ForeColor = _textWhite, Font = new Font("Segoe UI", 15, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };

        _panelTop1 = new Panel { BackColor = Color.FromArgb(45, 35, 15) };
        _panelTop1.Paint += (s, e) => { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; using (Pen p = new Pen(Color.FromArgb(80, 60, 25))) { e.Graphics.DrawRectangle(p, 0, 0, _panelTop1.Width - 1, _panelTop1.Height - 1); } };
        _lblTop1Nume = new Label { Text = "-", ForeColor = _textWhite, Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        _lblTop1Echipa = new Label { Text = "-", ForeColor = _textGray, Font = new Font("Segoe UI", 11, FontStyle.Regular), AutoSize = true, BackColor = Color.Transparent };
        _lblTop1Puncte = new Label { Text = "0 P", ForeColor = _gold, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        Label r1 = new Label { Text = "#1", ForeColor = _gold, Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent, Location = new Point(10, 6) };
        _panelTop1.Controls.Add(r1); _panelTop1.Controls.Add(_lblTop1Nume); _panelTop1.Controls.Add(_lblTop1Echipa); _panelTop1.Controls.Add(_lblTop1Puncte);

        _panelTop2 = new Panel { BackColor = Color.FromArgb(30, 30, 36) };
        _panelTop2.Paint += (s, e) => { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; using (Pen p = new Pen(Color.FromArgb(50, 50, 55))) { e.Graphics.DrawRectangle(p, 0, 0, _panelTop2.Width - 1, _panelTop2.Height - 1); } };
        _lblTop2Nume = new Label { Text = "-", ForeColor = _textWhite, Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        _lblTop2Echipa = new Label { Text = "-", ForeColor = _textGray, Font = new Font("Segoe UI", 11, FontStyle.Regular), AutoSize = true, BackColor = Color.Transparent };
        _lblTop2Puncte = new Label { Text = "0 P", ForeColor = _silver, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        Label r2 = new Label { Text = "#2", ForeColor = _silver, Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent, Location = new Point(10, 6) };
        _panelTop2.Controls.Add(r2); _panelTop2.Controls.Add(_lblTop2Nume); _panelTop2.Controls.Add(_lblTop2Echipa); _panelTop2.Controls.Add(_lblTop2Puncte);

        _panelTop3 = new Panel { BackColor = Color.FromArgb(30, 25, 22) };
        _panelTop3.Paint += (s, e) => { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; using (Pen p = new Pen(Color.FromArgb(55, 40, 30))) { e.Graphics.DrawRectangle(p, 0, 0, _panelTop3.Width - 1, _panelTop3.Height - 1); } };
        _lblTop3Nume = new Label { Text = "-", ForeColor = _textWhite, Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        _lblTop3Echipa = new Label { Text = "-", ForeColor = _textGray, Font = new Font("Segoe UI", 11, FontStyle.Regular), AutoSize = true, BackColor = Color.Transparent };
        _lblTop3Puncte = new Label { Text = "0 P", ForeColor = _bronze, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
        Label r3 = new Label { Text = "#3", ForeColor = _bronze, Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent, Location = new Point(10, 6) };
        _panelTop3.Controls.Add(r3); _panelTop3.Controls.Add(_lblTop3Nume); _panelTop3.Controls.Add(_lblTop3Echipa); _panelTop3.Controls.Add(_lblTop3Puncte);

        _panelTopCard.Controls.Add(_lblTopTitle);
        _panelTopCard.Controls.Add(_panelTop1);
        _panelTopCard.Controls.Add(_panelTop2);
        _panelTopCard.Controls.Add(_panelTop3);

        _panelActions = new Panel { BackColor = _bgCard };
        _panelActions.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen p = new Pen(Color.FromArgb(40, 40, 40)))
            using (GraphicsPath gp = RoundRect(_panelActions.ClientRectangle, 10))
                e.Graphics.DrawPath(p, gp);
        };
        _btnClasamentPiloti = SmallBtn("🏆 Clasament Piloti");
        _btnClasamentEchipe = SmallBtn("🏢 Clasament Echipe");
        _btnCalendar = SmallBtn("📅 Calendar Curse");
        _panelActions.Controls.Add(_btnClasamentPiloti);
        _panelActions.Controls.Add(_btnClasamentEchipe);
        _panelActions.Controls.Add(_btnCalendar);

        _panelInfo = new Panel { BackColor = _bgCard };
        _panelInfo.Paint += (s, e) =>
        {
            Rectangle r = _panelInfo.ClientRectangle;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen p = new Pen(Color.FromArgb(40, 40, 40)))
            using (GraphicsPath gp = RoundRect(r, 10))
                e.Graphics.DrawPath(p, gp);

            int cx = 20, cy = r.Height / 2 - 15;
            using (Pen cp = new Pen(Color.FromArgb(55, 55, 55), 2))
            {
                e.Graphics.DrawLine(cp, cx, cy + 5, cx + 70, cy + 5);
                e.Graphics.DrawLine(cp, cx + 70, cy + 5, cx + 80, cy - 5);
                e.Graphics.DrawEllipse(cp, cx + 8, cy, 16, 16);
                e.Graphics.DrawEllipse(cp, cx + 50, cy, 16, 16);
            }
        };
        _lblInfoTitle = new Label { Text = "BINE AI VENIT", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = _textWhite, AutoSize = true, BackColor = Color.Transparent };
        _lblInfoSub = new Label { Text = "Accesează orice secțiune pentru a începe gestionarea\nsezonului de Formula 1. Datele sunt sincronizate în timp real.", Font = new Font("Segoe UI", 9), ForeColor = _textGray, AutoSize = false, BackColor = Color.Transparent };
        _panelInfo.Controls.Add(_lblInfoTitle);
        _panelInfo.Controls.Add(_lblInfoSub);

        _rightPanel.Controls.Add(_panelTopCard);
        _rightPanel.Controls.Add(_panelActions);
        _rightPanel.Controls.Add(_panelInfo);
    }

    private void DoLayout()
    {
        int fw = this.ClientSize.Width;
        int fh = this.ClientSize.Height;

        _btnClose.Location = new Point(fw - 46, 4);
        _btnMinimize.Location = new Point(fw - 90, 4);

        // Left panel: 55% width, full height below titlebar
        int lw = (int)(fw * 0.55);
        _leftPanel.Size = new Size(lw, fh - 36);
        _leftPanel.Location = new Point(0, 36);

        // Glow on the edge
        int gs = Math.Min(240, fh / 4);
        _panelGlow.Size = new Size(gs, gs);
        _panelGlow.Location = new Point(lw - gs / 4, (fh - gs) / 2);

        // Left content fills entire left panel with margins
        int margin = (int)(lw * 0.04);
        int lcW = lw - margin * 2;
        int lcH = _leftPanel.Height - margin * 2;
        _leftContent.Size = new Size(lcW, lcH);
        _leftContent.Location = new Point(margin, margin);

        // Logo - scales with panel
        int logoSize = Math.Min(160, lcW / 5);
        _logoCircle.Size = new Size(logoSize, logoSize);
        _logoCircle.Location = new Point((lcW - logoSize) / 2, (int)(lcH * 0.03));

        // Title
        int titleSz = Math.Max(22, Math.Min(56, lcW / 9 + 20));
        _titleMain.Font = new Font("Segoe UI Black", titleSz, FontStyle.Bold);
        _titleMain.Size = new Size(lcW - 20, (int)(titleSz * 1.4));
        _titleMain.Location = new Point(10, logoSize + (int)(lcH * 0.04));

        // Red line
        int rlY = _titleMain.Bottom + 10;
        int rlW = Math.Min(220, lcW / 3);
        _redLine.Size = new Size(rlW, 3);
        _redLine.Location = new Point((lcW - rlW) / 2, rlY);

        // Subtitle
        int subSz = Math.Max(10, Math.Min(14, lcW / 35 + 4));
        _subtitle.Font = new Font("Segoe UI", subSz, FontStyle.Bold);
        _subtitle.Size = new Size(lcW - 40, 28);
        _subtitle.Location = new Point(20, rlY + 18);

        // Description
        int descSz = Math.Max(9, Math.Min(12, lcW / 40 + 3));
        int descY = _subtitle.Bottom + Math.Max(8, lcH / 30);
        _description.Font = new Font("Segoe UI", descSz);
        _description.Size = new Size(lcW - 60, 46);
        _description.Location = new Point(30, descY);

        // Buttons
        int btnW = Math.Max(150, Math.Min(260, lcW / 3));
        int btnH = Math.Max(36, Math.Min(56, lcH / 14));
        int btnY = descY + (int)(lcH * 0.11);
        int btnGap = Math.Max(10, (lcW - btnW * 2) / 3);

        _btnLogin.Size = new Size(btnW, btnH);
        _btnLogin.Font = new Font("Segoe UI", Math.Max(10, Math.Min(13, btnW / 18)), FontStyle.Bold);
        _btnLogin.Location = new Point(btnGap, btnY);

        _btnRegister.Size = new Size(btnW, btnH);
        _btnRegister.Font = new Font("Segoe UI", Math.Max(10, Math.Min(13, btnW / 18)), FontStyle.Bold);
        _btnRegister.Location = new Point(btnGap * 2 + btnW, btnY);

        // Stats row - fill remaining space before bottom decor
        int statW = Math.Max(200, Math.Min(540, lcW - 20));
        int statY = btnY + btnH + Math.Max(10, lcH / 20);
        _statsRow.Size = new Size(statW, Math.Max(40, Math.Min(65, lcH / 9)));
        _statsRow.Location = new Point((lcW - statW) / 2, statY);
        int segW = statW / 4;
        _lblPiloti.Size = new Size(segW - 5, _statsRow.Height - 10);
        _lblPiloti.Location = new Point(2, 0);
        _lblPiloti.Font = new Font("Segoe UI", Math.Max(8, Math.Min(12, segW / 14)), FontStyle.Bold);
        _lblPiloti.TextAlign = ContentAlignment.MiddleCenter;
        _lblEchipe.Size = new Size(segW - 5, _statsRow.Height - 10);
        _lblEchipe.Location = new Point(segW, 0);
        _lblEchipe.Font = new Font("Segoe UI", Math.Max(8, Math.Min(12, segW / 14)), FontStyle.Bold);
        _lblEchipe.TextAlign = ContentAlignment.MiddleCenter;
        _lblCurse.Size = new Size(segW - 5, _statsRow.Height - 10);
        _lblCurse.Location = new Point(segW * 2, 0);
        _lblCurse.Font = new Font("Segoe UI", Math.Max(8, Math.Min(12, segW / 14)), FontStyle.Bold);
        _lblCurse.TextAlign = ContentAlignment.MiddleCenter;
        _lblPuncte.Size = new Size(segW - 5, _statsRow.Height - 10);
        _lblPuncte.Location = new Point(segW * 3, 0);
        _lblPuncte.Font = new Font("Segoe UI", Math.Max(8, Math.Min(12, segW / 14)), FontStyle.Bold);
        _lblPuncte.TextAlign = ContentAlignment.MiddleCenter;

        // Bottom decor - fills remaining space at the bottom
        int bdY = statY + _statsRow.Height + 8;
        int bdH = _leftContent.Height - bdY - 5;
        if (bdH > 20)
        {
            _bottomDecor.Size = new Size(lcW - 40, bdH);
            _bottomDecor.Location = new Point(20, bdY);
            _bottomDecor.Visible = true;
        }
        else
            _bottomDecor.Visible = false;

        // Right panel: remaining width
        int rpx = lw + (int)(fw * 0.015);
        int rpw = fw - rpx - (int)(fw * 0.015);
        int rpy = 40;
        int rph = fh - rpy - (int)(fh * 0.015);
        _rightPanel.Size = new Size(Math.Max(rpw, 50), Math.Max(rph, 100));
        _rightPanel.Location = new Point(rpx, rpy);

        int cm = (int)(_rightPanel.Width * 0.015);
        int tcw = _rightPanel.Width - cm * 2;

        // Top pilots card: 60% of right panel height
        int tch = (int)(_rightPanel.Height * 0.60);
        _panelTopCard.Size = new Size(tcw, tch);
        _panelTopCard.Location = new Point(cm, cm);

        _lblTopTitle.Location = new Point((int)(tcw * 0.025), 14);

        // Top 3 panels fill the card width
        int tpGap = (int)(tcw * 0.015);
        int tpw = (tcw - tpGap * 4) / 3;
        int tph = tch - 60;
        int ty = 48;

        _panelTop1.Size = new Size(tpw, tph);
        _panelTop1.Location = new Point(tpGap, ty);
        int lbFS = Math.Max(10, Math.Min(14, tpw / 14));
        _lblTop1Nume.Font = new Font("Segoe UI", lbFS, FontStyle.Bold);
        _lblTop1Nume.Location = new Point(tpw / 3, (int)(tph * 0.15));
        _lblTop1Echipa.Font = new Font("Segoe UI", Math.Max(9, Math.Min(11, tpw / 18)), FontStyle.Regular);
        _lblTop1Echipa.Location = new Point(tpw / 3, (int)(tph * 0.55));
        _lblTop1Puncte.Font = new Font("Segoe UI", Math.Max(12, Math.Min(18, tpw / 12)), FontStyle.Bold);
        _lblTop1Puncte.Location = new Point(tpw - (int)(tpw * 0.35), (int)(tph * 0.15));

        _panelTop2.Size = new Size(tpw, tph);
        _panelTop2.Location = new Point(tpGap * 2 + tpw, ty);
        _lblTop2Nume.Font = new Font("Segoe UI", lbFS, FontStyle.Bold);
        _lblTop2Nume.Location = new Point(tpw / 3, (int)(tph * 0.15));
        _lblTop2Echipa.Font = new Font("Segoe UI", Math.Max(9, Math.Min(11, tpw / 18)), FontStyle.Regular);
        _lblTop2Echipa.Location = new Point(tpw / 3, (int)(tph * 0.55));
        _lblTop2Puncte.Font = new Font("Segoe UI", Math.Max(12, Math.Min(18, tpw / 12)), FontStyle.Bold);
        _lblTop2Puncte.Location = new Point(tpw - (int)(tpw * 0.35), (int)(tph * 0.15));

        _panelTop3.Size = new Size(tpw, tph);
        _panelTop3.Location = new Point(tpGap * 3 + tpw * 2, ty);
        _lblTop3Nume.Font = new Font("Segoe UI", lbFS, FontStyle.Bold);
        _lblTop3Nume.Location = new Point(tpw / 3, (int)(tph * 0.15));
        _lblTop3Echipa.Font = new Font("Segoe UI", Math.Max(9, Math.Min(11, tpw / 18)), FontStyle.Regular);
        _lblTop3Echipa.Location = new Point(tpw / 3, (int)(tph * 0.55));
        _lblTop3Puncte.Font = new Font("Segoe UI", Math.Max(12, Math.Min(18, tpw / 12)), FontStyle.Bold);
        _lblTop3Puncte.Location = new Point(tpw - (int)(tpw * 0.35), (int)(tph * 0.15));

        // Action buttons card
        int ach = Math.Max(40, Math.Min(80, (int)(_rightPanel.Height * 0.10)));
        int acy = cm + tch + cm;
        _panelActions.Size = new Size(tcw, ach);
        _panelActions.Location = new Point(cm, acy);

        int sbw = Math.Max(100, Math.Min(200, (tcw - 40) / 3));
        int sbh = Math.Max(28, Math.Min(40, ach - 20));
        int sby = (ach - sbh) / 2;
        _btnClasamentPiloti.Size = new Size(sbw, sbh);
        _btnClasamentPiloti.Font = new Font("Segoe UI", Math.Max(8, Math.Min(11, sbw / 16)), FontStyle.Regular);
        _btnClasamentPiloti.Location = new Point(12, sby);
        _btnClasamentEchipe.Size = new Size(sbw, sbh);
        _btnClasamentEchipe.Font = new Font("Segoe UI", Math.Max(8, Math.Min(11, sbw / 16)), FontStyle.Regular);
        _btnClasamentEchipe.Location = new Point(18 + sbw, sby);
        _btnCalendar.Size = new Size(sbw, sbh);
        _btnCalendar.Font = new Font("Segoe UI", Math.Max(8, Math.Min(11, sbw / 16)), FontStyle.Regular);
        _btnCalendar.Location = new Point(24 + sbw * 2, sby);

        // Info card - fills remaining space
        int icy = acy + ach + cm;
        int ich = _rightPanel.Height - icy - cm;
        _panelInfo.Size = new Size(tcw, Math.Max(ich, 20));
        _panelInfo.Location = new Point(cm, icy);

        _lblInfoTitle.Font = new Font("Segoe UI", Math.Max(10, Math.Min(15, ich / 6)), FontStyle.Bold);
        _lblInfoTitle.Location = new Point(110, Math.Max(5, ich / 3 - 15));
        _lblInfoSub.Font = new Font("Segoe UI", Math.Max(7, Math.Min(10, ich / 10)));
        _lblInfoSub.Size = new Size(tcw - 130, 40);
        _lblInfoSub.Location = new Point(110, Math.Max(20, ich / 3 + 10));
    }

    private Button AccentBtn(string text)
    {
        return new Button
        {
            Text = text, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(200, 5, 0) },
            BackColor = _f1Red, ForeColor = _textWhite, Font = new Font("Segoe UI", 12, FontStyle.Bold), Cursor = Cursors.Hand
        };
    }

    private Button OutlineBtn(string text)
    {
        Button btn = new Button
        {
            Text = text, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 2, MouseOverBackColor = Color.FromArgb(225, 6, 0, 30) },
            BackColor = Color.Transparent, ForeColor = _f1Red, Font = new Font("Segoe UI", 12, FontStyle.Bold), Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderColor = _f1Red;
        btn.Paint += (s, e) =>
        {
            Button b = s as Button;
            if (b != null)
            {
                using (Pen p = new Pen(_f1Red, 2))
                using (GraphicsPath gp = RoundRect(new Rectangle(1, 1, b.Width - 2, b.Height - 2), 6))
                { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; e.Graphics.DrawPath(p, gp); }
            }
        };
        return btn;
    }

    private Button SmallBtn(string text)
    {
        Button btn = new Button
        {
            Text = text, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 50, 50) },
            BackColor = Color.FromArgb(32, 32, 32), ForeColor = Color.FromArgb(220, 220, 220),
            Font = new Font("Segoe UI", 11, FontStyle.Regular), Cursor = Cursors.Hand, TextAlign = ContentAlignment.MiddleCenter
        };
        btn.Paint += (s, e) =>
        {
            Button b = s as Button;
            if (b != null)
            {
                using (Pen p = new Pen(Color.FromArgb(50, 50, 50)))
                using (GraphicsPath gp = RoundRect(new Rectangle(1, 1, b.Width - 2, b.Height - 2), 6))
                { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; e.Graphics.DrawPath(p, gp); }
            }
        };
        return btn;
    }

    private Label StatLabel(string text)
    {
        return new Label { Text = text, ForeColor = _textWhite, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Transparent };
    }

    private GraphicsPath RoundRect(Rectangle r, int rad)
    {
        GraphicsPath gp = new GraphicsPath();
        int d = rad * 2;
        gp.AddArc(r.X, r.Y, d, d, 180, 90);
        gp.AddArc(r.Right - d - 1, r.Y, d, d, 270, 90);
        gp.AddArc(r.Right - d - 1, r.Bottom - d - 1, d, d, 0, 90);
        gp.AddArc(r.X, r.Bottom - d - 1, d, d, 90, 90);
        gp.CloseFigure();
        return gp;
    }

    private void LoadStats()
    {
        try
        {
            var totiPilotii = _pilotService.GetAll();
            var toateEchipele = _echipaService.GetAll();
            var clasamentGeneral = _clasamentService.GetClasamentGeneral();

            _lblPiloti.Text = $"{totiPilotii.Count}\nPiloți";
            _lblEchipe.Text = $"{toateEchipele.Count}\nEchipe";
            _lblCurse.Text = $"0\nCurse";
            _lblPuncte.Text = $"{clasamentGeneral.Sum(c => c.Puncte)}\nPuncte";

            var top3 = _clasamentService.GetClasamentGeneral().Take(3).ToList();

            if (top3.Count >= 1)
            {
                var p1 = totiPilotii.FirstOrDefault(p => p.PilotID == top3[0].PilotID);
                var e1 = p1 != null ? toateEchipele.FirstOrDefault(e => e.EchipaID == p1.EchipaID) : null;
                _lblTop1Nume.Text = p1?.Nume ?? "-";
                _lblTop1Echipa.Text = e1?.Nume ?? "-";
                _lblTop1Puncte.Text = $"{top3[0].Puncte} P";
            }
            if (top3.Count >= 2)
            {
                var p2 = totiPilotii.FirstOrDefault(p => p.PilotID == top3[1].PilotID);
                var e2 = p2 != null ? toateEchipele.FirstOrDefault(e => e.EchipaID == p2.EchipaID) : null;
                _lblTop2Nume.Text = p2?.Nume ?? "-";
                _lblTop2Echipa.Text = e2?.Nume ?? "-";
                _lblTop2Puncte.Text = $"{top3[1].Puncte} P";
            }
            if (top3.Count >= 3)
            {
                var p3 = totiPilotii.FirstOrDefault(p => p.PilotID == top3[2].PilotID);
                var e3 = p3 != null ? toateEchipele.FirstOrDefault(e => e.EchipaID == p3.EchipaID) : null;
                _lblTop3Nume.Text = p3?.Nume ?? "-";
                _lblTop3Echipa.Text = e3?.Nume ?? "-";
                _lblTop3Puncte.Text = $"{top3[2].Puncte} P";
            }
        }
        catch { }
    }

    private void OpenLoginForm()
    {
        LoginForm loginForm = new LoginForm();
        loginForm.Show();
        this.Hide();
    }

    private void OpenRegisterForm()
    {
        RegisterForm registerForm = new RegisterForm();
        registerForm.Show();
        this.Hide();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    }
}