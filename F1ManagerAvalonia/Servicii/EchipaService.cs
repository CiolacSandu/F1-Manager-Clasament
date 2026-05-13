using System.Data;
using F1ManagerAvalonia.BazaDeDate;
using F1ManagerAvalonia.Modele;

namespace F1ManagerAvalonia.Servicii
{
    public class EchipaService
    {
        private DbHelper db = new DbHelper();

        public List<Echipa> GetAll()
        {
            var list = new List<Echipa>();
            string query = "SELECT * FROM echipe ORDER BY Nume ASC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Echipa
                {
                    EchipaID = Convert.ToInt32(row["EchipaID"]),
                    Nume = row["Nume"].ToString() ?? "",
                    Tara = row["Tara"]?.ToString(),
                    DirectorEchipa = row["DirectorEchipa"]?.ToString(),
                    Buget = row["Buget"] != DBNull.Value ? Convert.ToDecimal(row["Buget"]) : null
                });
            }
            return list;
        }

        public void Add(Echipa e)
        {
            string query = "INSERT INTO echipe (Nume, Tara, DirectorEchipa, Buget) VALUES (@nume, @tara, @director, @buget)";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@nume", e.Nume),
                new MySql.Data.MySqlClient.MySqlParameter("@tara", e.Tara ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@director", e.DirectorEchipa ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@buget", e.Buget ?? (object)DBNull.Value)
            });
        }

        public void Update(Echipa e)
        {
            string query = "UPDATE echipe SET Nume=@nume, Tara=@tara, DirectorEchipa=@director, Buget=@buget WHERE EchipaID=@id";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@nume", e.Nume),
                new MySql.Data.MySqlClient.MySqlParameter("@tara", e.Tara ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@director", e.DirectorEchipa ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@buget", e.Buget ?? (object)DBNull.Value),
                new MySql.Data.MySqlClient.MySqlParameter("@id", e.EchipaID)
            });
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM echipe WHERE EchipaID=@id";
            db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@id", id)
            });
        }

        public List<Echipa> GetAllWithPoints()
        {
            var list = new List<Echipa>();
            string query = @"
                SELECT e.EchipaID, e.Nume, e.Tara, e.DirectorEchipa, e.Buget,
                    COALESCE(SUM(cl.Puncte), 0) AS Puncte
                FROM echipe e
                LEFT JOIN piloti p ON e.EchipaID = p.EchipaID
                LEFT JOIN clasament cl ON p.PilotID = cl.PilotID
                GROUP BY e.EchipaID, e.Nume, e.Tara, e.DirectorEchipa, e.Buget
                ORDER BY Puncte DESC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Echipa
                {
                    EchipaID = Convert.ToInt32(row["EchipaID"]),
                    Nume = row["Nume"].ToString() ?? "",
                    Tara = row["Tara"]?.ToString(),
                    DirectorEchipa = row["DirectorEchipa"]?.ToString(),
                    Buget = row["Buget"] != DBNull.Value ? Convert.ToDecimal(row["Buget"]) : null,
                    Puncte = row["Puncte"] != DBNull.Value ? Convert.ToInt32(row["Puncte"]) : 0
                });
            }
            return list;
        }
    }
}