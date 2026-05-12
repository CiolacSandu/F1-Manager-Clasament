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
                    role VARCHAR(50) NOT NULL,
                    DateCreate DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";

            using var cmd = new MySqlCommand(createTable, conn);
            cmd.ExecuteNonQuery();

            if (!ColumnExists(conn, "users", "password"))
            {
                string addPasswordColumn = @"
                    ALTER TABLE users
                    ADD COLUMN password VARCHAR(255) NOT NULL DEFAULT '';
                ";

                using var alterCmd = new MySqlCommand(addPasswordColumn, conn);
                alterCmd.ExecuteNonQuery();
            }

            if (ColumnExists(conn, "users", "PasswordHash"))
            {
                string copyOldPasswords = @"
                    UPDATE users
                    SET password = PasswordHash
                    WHERE password = '' AND PasswordHash IS NOT NULL;
                ";

                using var copyCmd = new MySqlCommand(copyOldPasswords, conn);
                copyCmd.ExecuteNonQuery();
            }
        }

        private bool ColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            string query = @"
                SELECT COUNT(*)
                FROM information_schema.columns
                WHERE table_schema = @db
                  AND table_name = @table
                  AND column_name = @column;
            ";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@db", database);
            cmd.Parameters.AddWithValue("@table", tableName);
            cmd.Parameters.AddWithValue("@column", columnName);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
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