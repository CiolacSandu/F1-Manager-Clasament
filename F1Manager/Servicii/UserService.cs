using MySql.Data.MySqlClient;
using F1Manager.BazaDeDate;
using F1Manager.Modele;

namespace F1Manager.Servicii
{
    public class UserService
    {
        public string? LastError { get; private set; }

        DbConnection db = new DbConnection();

        public bool Register(User user)
        {
            LastError = null;

            try
            {
                using var conn = db.GetConnection();
                conn.Open();

                string query = "INSERT INTO users(username,email,password,role) VALUES(@u,@e,@p,@r)";
                using var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.Password);
                cmd.Parameters.AddWithValue("@r", "User"); // Default role

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // ✔ LOGIN corect (returnează Role sau null)
        public string? Login(string username, string password)
        {
            LastError = null;

            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT Role 
                        FROM users 
                        WHERE username = @u AND password = @p
                    ";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@u", username.Trim());
                    cmd.Parameters.AddWithValue("@p", password.Trim());

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        return result.ToString();

                    return null;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }
    }
}