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

                // Calendar - upcoming 3 races
                var upcomingRaces = clasamentService.GetCurseUrmatoare();
                int idx = 0;
                string[] labels = { labelCursa1.Text, labelCursa2.Text, labelCursa3.Text };
                foreach (var race in upcomingRaces)
                {
                    if (idx >= 3) break;
                    if (idx == 0) labelCursa1.Text = $"{race.NumeCursa} - {race.DataFormatted}";
                    else if (idx == 1) labelCursa2.Text = $"{race.NumeCursa} - {race.DataFormatted}";
                    else if (idx == 2) labelCursa3.Text = $"{race.NumeCursa} - {race.DataFormatted}";
                    idx++;
                }
                if (upcomingRaces.Count == 0)
                {
                    labelCursa1.Text = "Nu sunt curse programate";
                    labelCursa2.Text = "";
                    labelCursa3.Text = "";
                }
                else if (upcomingRaces.Count == 1)
                {
                    labelCursa2.Text = "";
                    labelCursa3.Text = "";
                }
                else if (upcomingRaces.Count == 2)
                {
                    labelCursa3.Text = "";
                }

                // Latest results
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
    }
}