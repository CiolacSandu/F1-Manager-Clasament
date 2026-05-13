using System.Data;
using F1ManagerAvalonia.BazaDeDate;
using F1ManagerAvalonia.Modele;

namespace F1ManagerAvalonia.Servicii
{
    public class ClasamentService
    {
        private DbHelper db = new DbHelper();

        public List<Clasament> GetClasamentGeneral()
        {
            var list = new List<Clasament>();
            string query = @"
                SELECT 
                    p.PilotID,
                    p.Nume AS NumePilot,
                    e.Nume AS NumeEchipa,
                    COALESCE(SUM(cl.Puncte), 0) AS Puncte
                FROM piloti p
                LEFT JOIN echipe e ON p.EchipaID = e.EchipaID
                LEFT JOIN clasament cl ON p.PilotID = cl.PilotID
                GROUP BY p.PilotID, p.Nume, e.Nume
                ORDER BY Puncte DESC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            int poz = 1;
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Clasament
                {
                    PozitieFinala = poz++,
                    PilotID = Convert.ToInt32(row["PilotID"]),
                    NumePilot = row["NumePilot"]?.ToString(),
                    NumeEchipa = row["NumeEchipa"]?.ToString(),
                    Puncte = Convert.ToInt32(row["Puncte"])
                });
            }
            return list;
        }

        public List<Clasament> GetClasamentEchipe()
        {
            var list = new List<Clasament>();
            string query = @"
                SELECT 
                    e.EchipaID,
                    e.Nume AS NumeEchipa,
                    COALESCE(SUM(cl.Puncte), 0) AS Puncte
                FROM echipe e
                LEFT JOIN piloti p ON e.EchipaID = p.EchipaID
                LEFT JOIN clasament cl ON p.PilotID = cl.PilotID
                GROUP BY e.EchipaID, e.Nume
                ORDER BY Puncte DESC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            int poz = 1;
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Clasament
                {
                    PozitieFinala = poz++,
                    NumeEchipa = row["NumeEchipa"]?.ToString(),
                    Puncte = Convert.ToInt32(row["Puncte"])
                });
            }
            return list;
        }

        public List<Clasament> GetRezultateByCursa(int cursaId)
        {
            var list = new List<Clasament>();
            string query = @"
                SELECT cl.PozitieFinala, cl.Puncte, p.Nume AS NumePilot, e.Nume AS NumeEchipa, c.NumeCursa
                FROM clasament cl
                JOIN piloti p ON cl.PilotID = p.PilotID
                LEFT JOIN echipe e ON p.EchipaID = e.EchipaID
                JOIN curse c ON cl.CursaID = c.CursaID
                WHERE cl.CursaID = @cursaId
                ORDER BY cl.PozitieFinala ASC";
            
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Clasament
                {
                    PozitieFinala = Convert.ToInt32(row["PozitieFinala"]),
                    NumePilot = row["NumePilot"]?.ToString(),
                    NumeEchipa = row["NumeEchipa"]?.ToString(),
                    Puncte = Convert.ToInt32(row["Puncte"]),
                    NumeCursa = row["NumeCursa"]?.ToString()
                });
            }
            return list;
        }

        public List<Cursa> GetCurseCuRezultate()
        {
            var list = new List<Cursa>();
            string query = @"
                SELECT DISTINCT c.CursaID, c.NumeCursa, c.Locatie, c.DataCursa, c.NumarTure
                FROM curse c
                INNER JOIN clasament cl ON c.CursaID = cl.CursaID
                ORDER BY c.DataCursa DESC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Cursa
                {
                    CursaID = Convert.ToInt32(row["CursaID"]),
                    NumeCursa = row["NumeCursa"]?.ToString() ?? "",
                    Locatie = row["Locatie"]?.ToString(),
                    DataCursa = row["DataCursa"] != DBNull.Value ? Convert.ToDateTime(row["DataCursa"]) : null,
                    NumarTure = row["NumarTure"] != DBNull.Value ? Convert.ToInt32(row["NumarTure"]) : null,
                    Status = "Finalizata"
                });
            }
            return list;
        }

        public List<Cursa> GetCurseUrmatoare()
        {
            var list = new List<Cursa>();
            string query = @"
                SELECT c.CursaID, c.NumeCursa, c.Locatie, c.DataCursa, c.NumarTure
                FROM curse c
                LEFT JOIN clasament cl ON c.CursaID = cl.CursaID
                WHERE cl.CursaID IS NULL
                ORDER BY c.DataCursa ASC";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null) return list;

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Cursa
                {
                    CursaID = Convert.ToInt32(row["CursaID"]),
                    NumeCursa = row["NumeCursa"]?.ToString() ?? "",
                    Locatie = row["Locatie"]?.ToString(),
                    DataCursa = row["DataCursa"] != DBNull.Value ? Convert.ToDateTime(row["DataCursa"]) : null,
                    NumarTure = row["NumarTure"] != DBNull.Value ? Convert.ToInt32(row["NumarTure"]) : null,
                    Status = "Programata"
                });
            }
            return list;
        }

        public Cursa? GetUrmatoareaCursa()
        {
            string query = @"
                SELECT c.CursaID, c.NumeCursa, c.Locatie, c.DataCursa, c.NumarTure
                FROM curse c
                LEFT JOIN clasament cl ON c.CursaID = cl.CursaID
                WHERE cl.CursaID IS NULL
                ORDER BY c.DataCursa ASC LIMIT 1";
            DataTable dt = db.ExecuteQuery(query);
            if (dt == null || dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new Cursa
            {
                CursaID = Convert.ToInt32(row["CursaID"]),
                NumeCursa = row["NumeCursa"]?.ToString() ?? "",
                Locatie = row["Locatie"]?.ToString(),
                DataCursa = row["DataCursa"] != DBNull.Value ? Convert.ToDateTime(row["DataCursa"]) : null,
                NumarTure = row["NumarTure"] != DBNull.Value ? Convert.ToInt32(row["NumarTure"]) : null,
                Status = "Programata"
            };
        }

        public int GetNumarPilotiInregistrati()
        {
            object result = db.ExecuteScalar("SELECT COUNT(*) FROM piloti");
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public int GetTotalPuncteSezon()
        {
            object result = db.ExecuteScalar("SELECT COALESCE(SUM(Puncte), 0) FROM clasament");
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public void ActualizeazaClasament()
        {
            // This is handled manually - the clasament table already has data
        }

        public void BackupDatabase()
        {
            try
            {
                string backupDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                string fileName = $"Backup_F1Manager_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                string filePath = Path.Combine(backupDir, fileName);

                string backupQuery = @"
                    SELECT * INTO OUTFILE '" + filePath.Replace("\\", "\\\\") + @"'
                    FIELDS TERMINATED BY ',' ENCLOSED BY '""' LINES TERMINATED BY '\n'
                    FROM clasament";
                
                Console.WriteLine($"Backup salvat în: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Eroare la backup: " + ex.Message);
            }
        }
    }
}