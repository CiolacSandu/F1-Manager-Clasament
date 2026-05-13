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

        // ✔ Creează toate tabelele necesare
        private void EnsureTablesExist()
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            // Tabela users
            string createUsers = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username VARCHAR(100) NOT NULL UNIQUE,
                    email VARCHAR(255) NOT NULL UNIQUE,
                    password VARCHAR(255) NOT NULL,
                    role VARCHAR(50) NOT NULL DEFAULT 'User',
                    DateCreate DATETIME DEFAULT CURRENT_TIMESTAMP
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";
            using (var cmd = new MySqlCommand(createUsers, conn))
                cmd.ExecuteNonQuery();

            // Tabela echipe
            string createEchipe = @"
                CREATE TABLE IF NOT EXISTS echipe (
                    EchipaID INT AUTO_INCREMENT PRIMARY KEY,
                    Nume VARCHAR(200) NOT NULL,
                    Tara VARCHAR(100) DEFAULT NULL,
                    DirectorEchipa VARCHAR(200) DEFAULT NULL,
                    Buget DECIMAL(15,2) DEFAULT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";
            using (var cmd = new MySqlCommand(createEchipe, conn))
                cmd.ExecuteNonQuery();

            // Tabela piloti
            string createPiloti = @"
                CREATE TABLE IF NOT EXISTS piloti (
                    PilotID INT AUTO_INCREMENT PRIMARY KEY,
                    Nume VARCHAR(200) NOT NULL,
                    Nationalitate VARCHAR(100) DEFAULT NULL,
                    Varsta INT DEFAULT NULL,
                    NumarMasina INT DEFAULT NULL,
                    EchipaID INT DEFAULT NULL,
                    FOREIGN KEY (EchipaID) REFERENCES echipe(EchipaID) ON DELETE SET NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";
            using (var cmd = new MySqlCommand(createPiloti, conn))
                cmd.ExecuteNonQuery();

            // Tabela curse
            string createCurse = @"
                CREATE TABLE IF NOT EXISTS curse (
                    CursaID INT AUTO_INCREMENT PRIMARY KEY,
                    NumeCursa VARCHAR(200) NOT NULL,
                    Locatie VARCHAR(200) DEFAULT NULL,
                    DataCursa DATE DEFAULT NULL,
                    NumarTure INT DEFAULT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";
            using (var cmd = new MySqlCommand(createCurse, conn))
                cmd.ExecuteNonQuery();

            // Tabela clasament
            string createClasament = @"
                CREATE TABLE IF NOT EXISTS clasament (
                    ClasamentID INT AUTO_INCREMENT PRIMARY KEY,
                    PilotID INT NOT NULL,
                    CursaID INT NOT NULL,
                    PozitieFinala INT NOT NULL,
                    Puncte INT NOT NULL DEFAULT 0,
                    FOREIGN KEY (PilotID) REFERENCES piloti(PilotID) ON DELETE CASCADE,
                    FOREIGN KEY (CursaID) REFERENCES curse(CursaID) ON DELETE CASCADE
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
            ";
            using (var cmd = new MySqlCommand(createClasament, conn))
                cmd.ExecuteNonQuery();

            // Migrări vechi users - PasswordHash -> password
            if (ColumnExists(conn, "users", "PasswordHash"))
            {
                if (!ColumnExists(conn, "users", "password"))
                {
                    string addPasswordColumn = @"
                        ALTER TABLE users
                        ADD COLUMN password VARCHAR(255) NULL;
                    ";

                    using var addPasswordCmd = new MySqlCommand(addPasswordColumn, conn);
                    addPasswordCmd.ExecuteNonQuery();
                }

                string copyOldPasswords = @"
                    UPDATE users
                    SET password = PasswordHash
                    WHERE (password IS NULL OR password = '') AND PasswordHash IS NOT NULL;
                ";

                using var copyCmd = new MySqlCommand(copyOldPasswords, conn);
                copyCmd.ExecuteNonQuery();

                string alterPasswordColumn = @"
                    ALTER TABLE users
                    MODIFY COLUMN password VARCHAR(255) NOT NULL DEFAULT '';
                ";

                using var alterPasswordCmd = new MySqlCommand(alterPasswordColumn, conn);
                alterPasswordCmd.ExecuteNonQuery();

                string dropOldColumn = @"
                    ALTER TABLE users
                    DROP COLUMN PasswordHash;
                ";

                using var dropColumnCmd = new MySqlCommand(dropOldColumn, conn);
                dropColumnCmd.ExecuteNonQuery();
            }

            if (ColumnExists(conn, "users", "Rol"))
            {
                if (!ColumnExists(conn, "users", "role"))
                {
                    string addRoleColumn = @"
                        ALTER TABLE users
                        ADD COLUMN role VARCHAR(50) NULL;
                    ";

                    using var addRoleCmd = new MySqlCommand(addRoleColumn, conn);
                    addRoleCmd.ExecuteNonQuery();
                }

                string copyOldRoles = @"
                    UPDATE users
                    SET role = Rol
                    WHERE (role IS NULL OR role = '') AND Rol IS NOT NULL;
                ";

                using var copyRoleCmd = new MySqlCommand(copyOldRoles, conn);
                copyRoleCmd.ExecuteNonQuery();

                string alterRoleColumn = @"
                    ALTER TABLE users
                    MODIFY COLUMN role VARCHAR(50) NOT NULL DEFAULT 'User';
                ";

                using var alterRoleCmd = new MySqlCommand(alterRoleColumn, conn);
                alterRoleCmd.ExecuteNonQuery();

                string dropOldRoleColumn = @"
                    ALTER TABLE users
                    DROP COLUMN Rol;
                ";

                using var dropRoleCmd = new MySqlCommand(dropOldRoleColumn, conn);
                dropRoleCmd.ExecuteNonQuery();
            }

            if (!ColumnExists(conn, "users", "role"))
            {
                string addRoleColumn = @"
                    ALTER TABLE users
                    ADD COLUMN role VARCHAR(50) NOT NULL DEFAULT 'User';
                ";

                using var addRoleCmd = new MySqlCommand(addRoleColumn, conn);
                addRoleCmd.ExecuteNonQuery();
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