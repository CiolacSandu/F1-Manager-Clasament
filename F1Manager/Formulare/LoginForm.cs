using System;
using System.Drawing;
using System.IO;
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
            LoadHeaderLogo();
        }

        private void LoadHeaderLogo()
        {
            try
            {
                string logoPath = Path.Combine(
                    Application.StartupPath,
                    "Resources",
                    "Imagini",
                    "f1_logo.png"
                );

                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                    pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch
            {
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string? role = service.Login(username, password);

            if (role != null)
            {
                MessageBox.Show("Login reușit!");

                this.Hide();

                // ADMIN
                if (role.ToLower() == "admin")
                {
                    DashboardAdminForm adminForm =
                        new DashboardAdminForm();

                    adminForm.Show();
                }

                // USER
                else
                {
                    DashboardUserForm userForm =
                        new DashboardUserForm();

                    userForm.Show();
                }
            }
            else
            {
                MessageBox.Show(
                    service.LastError ?? "Date greșite!"
                );
            }
        }

        private void btnRegisterLink_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm =
                new RegisterForm();

            registerForm.ShowDialog();
        }
    }
}