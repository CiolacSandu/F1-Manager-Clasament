using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace F1Manager.BazaDeDate
{
    public class DbHelper
    {
        private string _connectionString;

        public DbHelper()
        {
            _connectionString =
                "server=localhost;database=f1managerclasament;uid=root;pwd=sandu2008;";
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Eroare conexiune: " + ex.Message);

                return null;
            }
        }

        public object ExecuteScalar(string query)
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command =
                        new MySqlCommand(query, connection);

                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Eroare: " + ex.Message);

                return null;
            }
        }

        public void ExecuteNonQuery(
            string query,
            MySqlParameter[]? parameters = null)
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command =
                        new MySqlCommand(query, connection);

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Eroare: " + ex.Message);
            }
        }
    }
}