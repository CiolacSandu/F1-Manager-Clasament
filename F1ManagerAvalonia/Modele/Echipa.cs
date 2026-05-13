namespace F1ManagerAvalonia.Modele
{
    public class Echipa
    {
        public int EchipaID { get; set; }
        public string Nume { get; set; } = "";
        public string? Tara { get; set; }
        public string? DirectorEchipa { get; set; }
        public decimal? Buget { get; set; }
        public int Puncte { get; set; } // calculated from clasament
    }
}