using Avalonia.Controls;
using Avalonia.Interactivity;
using F1ManagerAvalonia.Servicii;

namespace F1ManagerAvalonia.Formulare
{
    public partial class DashboardUserWindow : Window
    {
        private ClasamentService clasamentService = new ClasamentService();

        public DashboardUserWindow()
        {
            InitializeComponent();
            LoadDashboardData();

            buttonDashboard.Click += (s, e) => LoadDashboardData();
            buttonPiloti.Click += (s, e) => ShowMessage("Navigare către Piloti");
            buttonEchipe.Click += (s, e) => ShowMessage("Navigare către Echipe");
            buttonCurse.Click += (s, e) => ShowMessage("Navigare către Curse");
            buttonClasament.Click += (s, e) => ShowMessage("Navigare către Clasament Piloti");
            buttonClasamentEchipe.Click += (s, e) => ShowMessage("Navigare către Clasament Echipe");
            buttonLogout.Click += BtnLogout_Click;

            btnVeziClasamentComplet.Click += (s, e) => ShowMessage("Navigare către Clasament Piloti");
            btnVeziCalendarComplet.Click += (s, e) => ShowMessage("Navigare către Curse");
            btnActualizeazaClasament.Click += BtnActualizeazaClasament_Click;
        }

        private async void ShowMessage(string message)
        {
            await MessageBoxHelper.ShowInfo(message);
        }

        private void LoadDashboardData()
        {
            try
            {
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
                for (int idx = 0; idx < 3; idx++)
                {
                    var textBlock = idx switch
                    {
                        0 => labelCursa1,
                        1 => labelCursa2,
                        2 => labelCursa3,
                        _ => null
                    };
                    if (textBlock == null) break;

                    if (idx < upcomingRaces.Count)
                    {
                        textBlock.Text = $"{upcomingRaces[idx].NumeCursa} - {upcomingRaces[idx].DataFormatted}";
                    }
                    else
                    {
                        textBlock.Text = idx == 0 ? "Nu sunt curse programate" : "";
                    }
                }

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
            catch (System.Exception ex)
            {
                _ = MessageBoxHelper.ShowInfo("Eroare la încărcarea datelor: " + ex.Message, "Eroare");
            }
        }

        private async void BtnLogout_Click(object? sender, RoutedEventArgs e)
        {
            var result = await MessageBoxHelper.ShowConfirm("Ești sigur că vrei să te deconectezi?", "Logout");
            if (result)
            {
                this.Close();
                var login = new LoginWindow();
                login.Show();
            }
        }

        private async void BtnActualizeazaClasament_Click(object? sender, RoutedEventArgs e)
        {
            clasamentService.ActualizeazaClasament();
            LoadDashboardData();
            await MessageBoxHelper.ShowInfo("Clasament actualizat!", "Succes");
        }
    }
}