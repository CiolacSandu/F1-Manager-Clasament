using MySql.Data.MySqlClient;

namespace F1Manager.BazaDeDate
{
    public class DbConnection
    {
        private readonly string server = "localhost";
        private readonly string user = "root";
        private readonly string password = "sandu2008";
        private readonly string database = "f1managerclasament";

        private string ConnectionString =>
            $"server={server};user={user};password={password};database={database};Allow User Variables=true;";

        private string RootConnectionString =>
            $"server={server};user={user};password={password};Allow User Variables=true;";

        public DbConnection()
        {
            EnsureDatabaseExists();
            EnsureTablesExist();
            EnsureTestDataExists();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // ✔ Creează baza de date dacă nu există
        private void EnsureDatabaseExists()
        {
            using var conn = new MySqlConnection(RootConnectionString);
            conn.Open();

            string query =
                $"CREATE DATABASE IF NOT EXISTS `{database}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";

            using var cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        // ✔ Creează tabelul users
        private void EnsureTablesExist()
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            string createTable = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username VARCHAR(100) NOT NULL UNIQUE,
                    email VARCHAR(255) NOT NULL UNIQUE,
                    password VARCHAR(255) NOT NULL,
                    role VARCHAR(50) NOT NULL,
                    DateCreate DATETIME DEFAULT CURRENT_TIMESTAMP
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";

            using var cmd = new MySqlCommand(createTable, conn);
            cmd.ExecuteNonQuery();
        }

        // ✔ Inserează admin + user automat
        private void EnsureTestDataExists()
        {
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();

                // verificăm dacă există admin
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username IN ('admin','user')";
                using var checkCmd = new MySqlCommand(checkQuery, conn);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    // ✔ ADMIN
                    string insertAdmin = @"
                        INSERT INTO users(username, email, password, Role)
                        VALUES (@u1, @e1, @p1, @r1);
                    ";

                    using var cmd1 = new MySqlCommand(insertAdmin, conn);
                    cmd1.Parameters.AddWithValue("@u1", "admin");
                    cmd1.Parameters.AddWithValue("@e1", "admin@f1.com");
                    cmd1.Parameters.AddWithValue("@p1", "admin123");
                    cmd1.Parameters.AddWithValue("@r1", "Admin");
                    cmd1.ExecuteNonQuery();

                    // ✔ USER
                    string insertUser = @"
                        INSERT INTO users(username, email, password, Role)
                        VALUES (@u2, @e2, @p2, @r2);
                    ";

                    using var cmd2 = new MySqlCommand(insertUser, conn);
                    cmd2.Parameters.AddWithValue("@u2", "user");
                    cmd2.Parameters.AddWithValue("@e2", "user@f1.com");
                    cmd2.Parameters.AddWithValue("@p2", "user123");
                    cmd2.Parameters.AddWithValue("@r2", "User");
                    cmd2.ExecuteNonQuery();
                }
            }
            catch
            {
                // ignorăm erorile
            }
        }
    }
}