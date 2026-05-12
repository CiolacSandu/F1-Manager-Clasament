using System;
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
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Parolele nu coincid!");
                return;
            }

            User user = new User()
            {
                Username = txtUsername.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text
            };

            if (service.Register(user))
            {
                MessageBox.Show("Cont creat!");
            }
            else if (!string.IsNullOrEmpty(service.LastError))
            {
                MessageBox.Show("Eroare bază de date: " + service.LastError, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Eroare la crearea contului.");
            }
        }
    }
}