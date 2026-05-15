using System.Globalization;

namespace F1Manager.Modele
{
    public class Cursa
    {
        public int CursaID { get; set; }
        public string NumeCursa { get; set; } = "";
        public string? Locatie { get; set; }
        public DateTime? DataCursa { get; set; }
        public int? NumarTure { get; set; }
        public string? Status { get; set; } 
        public string DataFormatted => DataCursa.HasValue 
            ? DataCursa.Value.ToString("dd MMM yyyy", new CultureInfo("ro-RO")) 
            : "N/A";
    }
}