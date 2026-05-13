using System.Data;
using F1Manager.BazaDeDate;
using F1Manager.Modele;

namespace F1Manager.Servicii
{
    public class PilotService
    {
        private DbHelper db = new DbHelper();

        public List<Pilot> GetAll()
        {
            var list = new List<Pilot>();
            string query = @"
                SELECT p.PilotID, p.Nume, p.Nationalitate, p.Varsta, p.NumarMasina, p.EchipaID, e.Nume AS NumeEchipa,
                    (SELECT COALESCE(SUM(cl.Puncte), 0) FROM clasament cl WHERE cl.PilotID = p.PilotID) AS Puncte
                FROM piloti p
                LEFT JOIN echipe e ON p.EchipaID = e.EchipaID
                ORDER BY Puncte DESC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Pilot
                {
                    PilotID = Convert.ToInt32(row["PilotID"]),
                    Nume = row["Nume"].ToString() ?? "",
                    Nationalitate = row["Nationalitate"]?.ToString(),
                    Varsta = row["Varsta"] != DBNull.Value ? Convert.ToInt32(row["Varsta"]) : null,
                    NumarMasina = row["NumarMasina"] != DBNull.Value ? Convert.ToInt32(row["NumarMasina"]) : null,
                    EchipaID = row["EchipaID"] != DBNull.Value ? Convert.ToInt32(row["EchipaID"]) : null,
                    NumeEchipa = row["NumeEchipa"]?.ToString(),
                    Puncte = row["Puncte"] != DBNull.Value ? Convert.ToInt32(row["Puncte"]) : 0
                });
            }
            return list;
        }

        public void Add(Pilot p)
        {
            string query = "INSERT INTO piloti (Nume, Nationalitate, Varsta, NumarMasina, EchipaID) VALUES (@nume, @nat, @varsta, @nr, @echipaid)";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@nume", p.Nume),
                new MySql.Data.MySqlClient.MySqlParameter("@nat", p.Nationalitate ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@varsta", p.Varsta ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@nr", p.NumarMasina ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@echipaid", p.EchipaID ?? (object)DBNull.Value)
            });
        }

        public void Update(Pilot p)
        {
            string query = "UPDATE piloti SET Nume=@nume, Nationalitate=@nat, Varsta=@varsta, NumarMasina=@nr, EchipaID=@echipaid WHERE PilotID=@id";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@nume", p.Nume),
                new MySql.Data.MySqlClient.MySqlParameter("@nat", p.Nationalitate ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@varsta", p.Varsta ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@nr", p.NumarMasina ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@echipaid", p.EchipaID ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@id", p.PilotID)
            });
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM piloti WHERE PilotID=@id";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@id", id)
            });
        }
    }
}