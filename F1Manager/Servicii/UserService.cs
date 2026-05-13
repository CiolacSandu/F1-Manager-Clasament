using MySql.Data.MySqlClient;
using F1Manager.BazaDeDate;
using F1Manager.Modele;

namespace F1Manager.Servicii
{
    public class UserService
    {
        public string? LastError { get; private set; }

        DbConnection db = new DbConnection();

        // REGISTER
        public bool Register(User user)
        {
            LastError = null;

            try
            {
                using var conn = db.GetConnection();

                conn.Open();

                string query =
                    @"INSERT INTO users
                    (username,email,password,role)
                    VALUES(@u,@e,@p,@r)";

                using var cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.Password);

                // implicit USER
                cmd.Parameters.AddWithValue("@r", "User");

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // LOGIN
        public string? Login(string username, string password)
        {
            LastError = null;

            try
            {
                using var conn = db.GetConnection();

                conn.Open();

                string query =
                    @"SELECT role
                    FROM users
                    WHERE username=@u
                    AND password=@p";

                using var cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@u", username.Trim());
                cmd.Parameters.AddWithValue("@p", password.Trim());

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();
                }

                LastError = "Username sau parola incorectă!";
                return null;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }
    }
}