using System.Data;
using F1Manager.BazaDeDate;
using F1Manager.Modele;
using MySql.Data.MySqlClient;

namespace F1Manager.Servicii
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
            string query = $@"
                SELECT cl.PozitieFinala, cl.Puncte, p.Nume AS NumePilot, e.Nume AS NumeEchipa, c.NumeCursa
                FROM clasament cl
                JOIN piloti p ON cl.PilotID = p.PilotID
                LEFT JOIN echipe e ON p.EchipaID = e.EchipaID
                JOIN curse c ON cl.CursaID = c.CursaID
                WHERE cl.CursaID = {cursaId}
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

        public Cursa? GetCursaById(int cursaId)
        {
            string query = $@"
                SELECT c.CursaID, c.NumeCursa, c.Locatie, c.DataCursa, c.NumarTure
                FROM curse c
                WHERE c.CursaID = {cursaId}";
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
                Status = "Finalizata"
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
            System.Windows.Forms.MessageBox.Show("Clasamentul este deja actualizat.", "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public void BackupDatabase()
        {
            try
            {
                string backupDir = Path.Combine(Application.StartupPath, "Backup");
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                string fileName = $"Backup_F1Manager_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                string filePath = Path.Combine(backupDir, fileName);

                System.Windows.Forms.MessageBox.Show($"Backup salvat în: {filePath}", "Backup", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Eroare la backup: " + ex.Message, "Eroare", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Finalizează următoarea cursă programată, generând automat clasament randomizat pentru toți piloții.
        /// </summary>
        /// <returns>Cursa finalizată, sau null dacă nu există curse programate.</returns>
        public Cursa? FinalizeazaUrmatoareaCursa()
        {
            // Get the next upcoming race
            var nextRace = GetUrmatoareaCursa();
            if (nextRace == null)
                return null;

            // Get all pilots with random ordering
            string getPilotsQuery = @"
                SELECT p.PilotID 
                FROM piloti p 
                ORDER BY RAND()";
            DataTable dtPiloti = db.ExecuteQuery(getPilotsQuery);
            if (dtPiloti == null || dtPiloti.Rows.Count == 0)
                return null;

            // F1 Point system: 25, 18, 15, 12, 10, 8, 6, 4, 2, 1, then 0 for the rest
            int[] points = { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };

            // Insert results for each pilot
            for (int i = 0; i < dtPiloti.Rows.Count; i++)
            {
                int pilotId = Convert.ToInt32(dtPiloti.Rows[i]["PilotID"]);
                int position = i + 1;
                int puncte = i < points.Length ? points[i] : 0;

                string insertQuery = @"
                    INSERT INTO clasament (PilotID, CursaID, PozitieFinala, Puncte)
                    VALUES (@pilotId, @cursaId, @pozitie, @puncte)";

                db.ExecuteNonQuery(insertQuery, new MySqlParameter[]
                {
                    new MySqlParameter("@pilotId", pilotId),
                    new MySqlParameter("@cursaId", nextRace.CursaID),
                    new MySqlParameter("@pozitie", position),
                    new MySqlParameter("@puncte", puncte)
                });
            }

            nextRace.Status = "Finalizata";
            return nextRace;
        }

        /// <summary>
        /// Adaugă următoarea cursă în calendar (cu 7 zile după ultima cursă din calendar).
        /// </summary>
        /// <returns>Cursa adăugată, sau null dacă nu s-a putut adăuga.</returns>
        public Cursa? AdaugaUrmatoareaCursa()
        {
            // Get the last race date to determine next race date
            string getLastDateQuery = "SELECT MAX(DataCursa) FROM curse";
            object lastDateObj = db.ExecuteScalar(getLastDateQuery);
            
            DateTime nextDate;
            if (lastDateObj != null && lastDateObj != DBNull.Value)
            {
                nextDate = Convert.ToDateTime(lastDateObj).AddDays(7);
            }
            else
            {
                nextDate = DateTime.Now.AddDays(7);
            }

            // Determine the next race number
            string countQuery = "SELECT COUNT(*) FROM curse";
            object countObj = db.ExecuteScalar(countQuery);
            int raceCount = countObj != null ? Convert.ToInt32(countObj) : 0;
            int nextRaceNumber = raceCount + 1;

            string numeCursa = $"Grand Prix {nextRaceNumber}";
            string locatie = "Circuit";
            string dataFormatted = nextDate.ToString("yyyy-MM-dd");

            string insertQuery = @"
                INSERT INTO curse (NumeCursa, Locatie, DataCursa, NumarTure)
                VALUES (@nume, @locatie, @data, @ture)";

            int numarTure = 50; // default number of laps

            db.ExecuteNonQuery(insertQuery, new MySqlParameter[]
            {
                new MySqlParameter("@nume", numeCursa),
                new MySqlParameter("@locatie", locatie),
                new MySqlParameter("@data", nextDate),
                new MySqlParameter("@ture", numarTure)
            });

            // Return the newly created race
            return GetUrmatoareaCursa();
        }
    }
}