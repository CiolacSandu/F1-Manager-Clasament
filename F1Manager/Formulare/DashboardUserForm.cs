using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;
using F1Manager.Modele;

namespace F1Manager.Formulare
{
    public partial class DashboardUserForm : Form
    {
        private ClasamentService clasamentService = new ClasamentService();

        public DashboardUserForm()
        {
            InitializeComponent();
        }

        private void DashboardUserForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            LoadDashboardData();
            ApplyDefaultHoverStyles();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Stats
                int nrPiloti = clasamentService.GetNumarPilotiInregistrati();
                labelPilotiInregistrati.Text = $"Piloți înregistrați: {nrPiloti}";

                int totalPuncte = clasamentService.GetTotalPuncteSezon();
                labelTotalPuncte.Text = $"Puncte acumulate: {totalPuncte}";

                var nextRace = clasamentService.GetUrmatoareaCursa();
                if (nextRace != null)
                {
                    labelUrmatoareaCursa.Text = $"Următoarea cursă: {nextRace.NumeCursa} ({nextRace.DataFormatted})";
                }
                else
                {
                    labelUrmatoareaCursa.Text = "Următoarea cursă: Toate cursele s-au încheiat";
                }

                // Top 3 Pilots
                var clasament = clasamentService.GetClasamentGeneral();
                if (clasament.Count >= 1)
                {
                    labelTop1Nume.Text = clasament[0].NumePilot ?? "-";
                    labelTop1Echipa.Text = clasament[0].NumeEchipa ?? "-";
                    labelTop1Puncte.Text = $"{clasament[0].Puncte} P";
                }
                if (clasament.Count >= 2)
                {
                    labelTop2Nume.Text = clasament[1].NumePilot ?? "-";
                    labelTop2Echipa.Text = clasament[1].NumeEchipa ?? "-";
                    labelTop2Puncte.Text = $"{clasament[1].Puncte} P";
                }
                if (clasament.Count >= 3)
                {
                    labelTop3Nume.Text = clasament[2].NumePilot ?? "-";
                    labelTop3Echipa.Text = clasament[2].NumeEchipa ?? "-";
                    labelTop3Puncte.Text = $"{clasament[2].Puncte} P";
                }

                // Calendar - upcoming 5 races
                var upcomingRaces = clasamentService.GetCurseUrmatoare();
                int idx = 0;
                Label[] cursaLabels = { labelCursa1, labelCursa2, labelCursa3, labelCursa4, labelCursa5 };
                foreach (var race in upcomingRaces)
                {
                    if (idx >= 5) break;
                    cursaLabels[idx].Text = $"{race.NumeCursa} - {race.DataFormatted}";
                    idx++;
                }
                for (int i = idx; i < 5; i++)
                {
                    if (i == 0 && upcomingRaces.Count == 0)
                        cursaLabels[i].Text = "Nu sunt curse programate";
                    else
                        cursaLabels[i].Text = "";
                }
                if (upcomingRaces.Count == 0)
                {
                    labelCursa1.Text = "Nu sunt curse programate";
                }

                // Latest results - Top 3 pilots
                var racesWithResults = clasamentService.GetCurseCuRezultate();
                if (racesWithResults.Count > 0)
                {
                    var lastRace = racesWithResults[0]; // most recent
                    labelRezultatCursa.Text = lastRace.NumeCursa;

                    var results = clasamentService.GetRezultateByCursa(lastRace.CursaID);
                    if (results.Count >= 1)
                        labelRezultat1.Text = $"1. {results[0].NumePilot}";
                    if (results.Count >= 2)
                        labelRezultat2.Text = $"2. {results[1].NumePilot}";
                    if (results.Count >= 3)
                        labelRezultat3.Text = $"3. {results[2].NumePilot}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea datelor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyDefaultHoverStyles()
        {
            buttonDashboard.BackColor = Color.Transparent;
            buttonPiloti.BackColor = Color.Transparent;
            buttonEchipe.BackColor = Color.Transparent;
            buttonCurse.BackColor = Color.Transparent;
            buttonClasament.BackColor = Color.Transparent;
            buttonClasamentEchipe.BackColor = Color.Transparent;
            buttonLogout.BackColor = Color.Transparent;
        }

        private void NavButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.FromArgb(200, 16, 45);
                button.ForeColor = Color.White;
            }
        }

        private void NavButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button == buttonLogout)
                {
                    button.BackColor = Color.Transparent;
                    button.ForeColor = Color.FromArgb(200, 80, 80);
                }
                else
                {
                    button.BackColor = Color.Transparent;
                    button.ForeColor = Color.WhiteSmoke;
                }
            }
        }

        private void InteractiveButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.FromArgb(230, 28, 43);
                button.ForeColor = Color.White;
            }
        }

        private void InteractiveButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.FromArgb(32, 32, 32);
                button.ForeColor = Color.WhiteSmoke;
            }
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void buttonPiloti_Click(object sender, EventArgs e)
        {
            PilotiForm form = new PilotiForm(readOnly: true);
            form.Show();
        }

        private void buttonEchipe_Click(object sender, EventArgs e)
        {
            EchipeForm form = new EchipeForm(readOnly: true);
            form.Show();
        }

        private void buttonCurse_Click(object sender, EventArgs e)
        {
            CurseForm form = new CurseForm(readOnly: true);
            form.Show();
        }

        private void buttonClasament_Click(object sender, EventArgs e)
        {
            ClasamentForm form = new ClasamentForm("Piloti");
            form.Show();
        }

        private void buttonClasamentEchipe_Click(object sender, EventArgs e)
        {
            ClasamentForm form = new ClasamentForm("Echipe");
            form.Show();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ești sigur că vrei să te deconectezi?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                LoginForm login = new LoginForm();
                login.Show();
            }
        }

        private void btnVeziClasamentComplet_Click(object sender, EventArgs e)
        {
            ClasamentForm form = new ClasamentForm("Piloti");
            form.Show();
        }

        private void btnVeziCalendarComplet_Click(object sender, EventArgs e)
        {
            CurseForm form = new CurseForm(readOnly: true);
            form.Show();
        }

        private void btnVeziRezultateComplet_Click(object sender, EventArgs e)
        {
            var racesWithResults = clasamentService.GetCurseCuRezultate();
            if (racesWithResults.Count > 0)
            {
                ClasamentForm form = new ClasamentForm("Rezultate", racesWithResults[0].CursaID);
                form.Show();
            }
            else
            {
                MessageBox.Show("Nu există rezultate pentru nicio cursă.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVeziRezultate_Click(object sender, EventArgs e)
        {
            var racesWithResults = clasamentService.GetCurseCuRezultate();
            if (racesWithResults.Count > 0)
            {
                ClasamentForm form = new ClasamentForm("Rezultate", racesWithResults[0].CursaID);
                form.Show();
            }
            else
            {
                MessageBox.Show("Nu există rezultate pentru nicio cursă.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActualizeazaClasament_Click(object sender, EventArgs e)
        {
            clasamentService.ActualizeazaClasament();
            LoadDashboardData();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            clasamentService.BackupDatabase();
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme();
            ThemeManager.ApplyTheme(this);
            ThemeManager.ApplyThemeToAllOpenForms();
            btnToggleTheme.Text = ThemeManager.IsDarkMode ? "🌙" : "☀️";
        }

        private void btnUrmatoareaCursa_Click(object sender, EventArgs e)
        {
            var nextRace = clasamentService.GetUrmatoareaCursa();
            if (nextRace != null)
            {
                CurseForm form = new CurseForm(readOnly: true);
                form.Show();
            }
            else
            {
                MessageBox.Show("Nu există curse programate.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFinalizeazaCursa_Click(object sender, EventArgs e)
        {
            var nextRace = clasamentService.GetUrmatoareaCursa();
            if (nextRace == null)
            {
                MessageBox.Show("Nu există curse programate de finalizat.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Ești sigur că vrei să finalizezi cursa '{nextRace.NumeCursa}'?\nSe vor genera automat rezultate randomizate pentru toți piloții.",
                "Confirmă finalizarea cursei",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var cursaFinalizata = clasamentService.FinalizeazaUrmatoareaCursa();
                if (cursaFinalizata != null)
                {
                    MessageBox.Show(
                        $"Cursa '{cursaFinalizata.NumeCursa}' a fost finalizată cu succes!\nRezultatele au fost generate și clasamentul a fost actualizat.",
                        "Cursă Finalizată",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Refresh dashboard data
                    LoadDashboardData();
                }
                else
                {
                    MessageBox.Show("Eroare la finalizarea cursei.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== Search Pilot Methods ==========

        private void buttonSearchPilot_Click(object sender, EventArgs e)
        {
            EfectueazaCautare();
        }

        private void textBoxSearchPilot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EfectueazaCautare();
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxSearchPilot_Enter(object sender, EventArgs e)
        {
            if (textBoxSearchPilot.Text == "Introdu numele pilotului...")
            {
                textBoxSearchPilot.Text = "";
                textBoxSearchPilot.ForeColor = Color.Black;
            }
        }

        private void textBoxSearchPilot_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearchPilot.Text))
            {
                textBoxSearchPilot.Text = "Introdu numele pilotului...";
                textBoxSearchPilot.ForeColor = Color.Gray;
            }
        }

        private void EfectueazaCautare()
        {
            try
            {
                string numeCautat = textBoxSearchPilot.Text.Trim();
                if (string.IsNullOrWhiteSpace(numeCautat) || numeCautat == "Introdu numele pilotului...")
                {
                    MessageBox.Show("Introdu numele unui pilot pentru căutare.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var rezultat = clasamentService.CautaPilot(numeCautat);

                // Ascunde toate label-ele de rezultat
                labelSearchResultNume.Visible = false;
                labelSearchResultEchipa.Visible = false;
                labelSearchResultPozitie.Visible = false;
                labelSearchResultPuncte.Visible = false;
                labelSearchNoResult.Visible = false;

                if (rezultat != null)
                {
                    labelSearchResultNume.Text = $"🏎️ {rezultat.NumePilot}";
                    labelSearchResultNume.Visible = true;

                    labelSearchResultEchipa.Text = $"🏁 Echipa: {rezultat.NumeEchipa ?? "N/A"}";
                    labelSearchResultEchipa.Visible = true;

                    labelSearchResultPozitie.Text = $"📍 Poziția în clasament: #{rezultat.PozitieFinala}";
                    labelSearchResultPozitie.Visible = true;

                    labelSearchResultPuncte.Text = $"⭐ Puncte totale: {rezultat.Puncte}";
                    labelSearchResultPuncte.Visible = true;
                }
                else
                {
                    labelSearchNoResult.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la căutare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}