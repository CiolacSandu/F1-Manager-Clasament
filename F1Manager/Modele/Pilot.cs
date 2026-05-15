namespace F1Manager.Modele
{
    public class Pilot
    {
        public int PilotID { get; set; }
        public string Nume { get; set; } = "";
        public string? Nationalitate { get; set; }
        public int? Varsta { get; set; }
        public int? NumarMasina { get; set; }
        public int? EchipaID { get; set; }
        public string? NumeEchipa { get; set; }
        public int Puncte { get; set; } 
    }
}