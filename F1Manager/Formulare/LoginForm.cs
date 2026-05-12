using System;
using System.Windows.Forms;
using F1Manager.Servicii;

namespace F1Manager.Formulare
{
    public partial class LoginForm : Form
    {
        UserService service = new UserService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // ✔ DEBUG (dacă vrei să vezi ce trimiți)
            // MessageBox.Show($"U: '{username}' P: '{password}'");

            string? role = service.Login(username, password);

            if (role != null)
            {
                MessageBox.Show("Login reușit!");

                this.Hide();

                if (role == "Admin")
                {
                    DashboardAdminForm f = new DashboardAdminForm();
                    f.Show();
                }
                else
                {
                    DashboardUserForm f = new DashboardUserForm();
                    f.Show();
                }
            }
            else
            {
                MessageBox.Show("Date greșite!");
            }
        }
    }
}