using System;
using System.IO;
using System.Windows.Forms;
using F1Manager.Modele;
using F1Manager.Servicii;

namespace F1Manager.Formulare
{
    public partial class RegisterForm : Form
    {
        UserService service = new UserService();

        public RegisterForm()
        {
            InitializeComponent();
            LoadHeaderLogo();
        }

        private void LoadHeaderLogo()
        {
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "Imagini", "f1_logo.png");
                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                }
            }
            catch { /* Ignore if logo loading fails */ }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Completează toate câmpurile.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Parolele nu coincid.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            if (service.Register(user))
            {
                MessageBox.Show("Cont creat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (!string.IsNullOrEmpty(service.LastError))
            {
                MessageBox.Show("Eroare bază de date: " + service.LastError, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Nu s-a putut crea contul.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}