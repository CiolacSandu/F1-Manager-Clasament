using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;
using F1Manager.Modele;

namespace F1Manager.Formulare
{
    public partial class DashboardAdminForm : Form
    {
        private ClasamentService clasamentService = new ClasamentService();
        private PilotService pilotService = new PilotService();
        private EchipaService echipaService = new EchipaService();

        public DashboardAdminForm()
        {
            InitializeComponent();
        }

        private void DashboardAdminForm_Load(object sender, EventArgs e)
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

                // Top 3 piloti
                var racesWithResults = clasamentService.GetCurseCuRezultate();
                if (racesWithResults.Count > 0)
                {
                    var lastRace = racesWithResults[0]; 
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
            PilotiForm form = new PilotiForm();
            form.Show();
        }

        private void buttonEchipe_Click(object sender, EventArgs e)
        {
            EchipeForm form = new EchipeForm();
            form.Show();
        }

        private void buttonCurse_Click(object sender, EventArgs e)
        {
            CurseForm form = new CurseForm();
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

        private void btnAdaugaPilot_Click(object sender, EventArgs e)
        {
            PilotiForm form = new PilotiForm();
            form.Show();
        }

        private void btnFinalizeazaCursa_Click(object sender, EventArgs e)
        {
            var nextRace = clasamentService.GetUrmatoareaCursa();
            if (nextRace == null)
            {
                MessageBox.Show("Nu există curse programate de finalizat.\nAdaugă mai întâi o cursă nouă.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Ești sigur că vrei să finalizezi cursa \"{nextRace.NumeCursa}\" din data de {nextRace.DataFormatted}?\nSe va genera un clasament automat pentru toți piloții.",
                "Finalizează Cursa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var cursaFinalizata = clasamentService.FinalizeazaUrmatoareaCursa();
                if (cursaFinalizata != null)
                {
                    MessageBox.Show(
                        $"Cursa \"{cursaFinalizata.NumeCursa}\" a fost finalizată cu succes!\nClasamentul a fost generat automat.\n\nVezi rezultatele în secțiunea de clasament.",
                        "Cursă Finalizată",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Reîncarcă dashboard-ul
                    LoadDashboardData();
                }
                else
                {
                    MessageBox.Show("Nu s-a putut finaliza cursa. Verifică să existe piloți înregistrați.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                MessageBox.Show("Nu există rezultate pentru nicio cursă. Finalizează mai întâi o cursă.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUrmatoareaCursa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ești sigur că vrei să adaugi următoarea cursă în calendar?",
                "Adaugă Următoarea Cursă",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var cursaAdaugata = clasamentService.AdaugaUrmatoareaCursa();
                if (cursaAdaugata != null)
                {
                    MessageBox.Show(
                        $"Cursa \"{cursaAdaugata.NumeCursa}\" a fost adăugată pentru data de {cursaAdaugata.DataFormatted}.",
                        "Cursă Adăugată",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    
                    LoadDashboardData();
                }
                else
                {
                    MessageBox.Show("Nu s-a putut adăuga cursa.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVeziClasamentComplet_Click(object sender, EventArgs e)
        {
            ClasamentForm form = new ClasamentForm("Piloti");
            form.Show();
        }

        private void btnVeziCalendarComplet_Click(object sender, EventArgs e)
        {
            CurseForm form = new CurseForm();
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
    }
}
