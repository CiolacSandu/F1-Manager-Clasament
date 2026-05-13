using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using F1ManagerAvalonia.Modele;
using F1ManagerAvalonia.Servicii;

namespace F1ManagerAvalonia.Formulare
{
    public partial class RegisterWindow : Window
    {
        UserService service = new UserService();

        public RegisterWindow()
        {
            InitializeComponent();
            LoadHeaderLogo();

            btnRegister.Click += BtnRegister_Click;
        }

        private void LoadHeaderLogo()
        {
            try
            {
                string logoPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Resources",
                    "Imagini",
                    "f1_logo.png"
                );

                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Source = new Bitmap(logoPath);
                }
            }
            catch
            {
            }
        }

        private async void BtnRegister_Click(object? sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text?.Trim() ?? "";
            string email = txtEmail.Text?.Trim() ?? "";
            string password = txtPassword.Text?.Trim() ?? "";
            string confirmPassword = txtConfirmPassword.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await MessageBoxHelper.ShowInfo("Completează toate câmpurile.", "Atenție");
                return;
            }

            if (password != confirmPassword)
            {
                await MessageBoxHelper.ShowInfo("Parolele nu coincid.", "Atenție");
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
                await MessageBoxHelper.ShowInfo("Cont creat cu succes!", "Succes");
                this.Close();
            }
            else if (!string.IsNullOrEmpty(service.LastError))
            {
                await MessageBoxHelper.ShowInfo("Eroare bază de date: " + service.LastError, "Eroare");
            }
            else
            {
                await MessageBoxHelper.ShowInfo("Nu s-a putut crea contul.", "Eroare");
            }
        }
    }
}