namespace F1ManagerAvalonia.Modele
{
    public class Clasament
    {
        public int ClasamentID { get; set; }
        public int PilotID { get; set; }
        public int CursaID { get; set; }
        public int PozitieFinala { get; set; }
        public int Puncte { get; set; }
        public string? NumePilot { get; set; }
        public string? NumeEchipa { get; set; }
        public string? NumeCursa { get; set; }
    }
}