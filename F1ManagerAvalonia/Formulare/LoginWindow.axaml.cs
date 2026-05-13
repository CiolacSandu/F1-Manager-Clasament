using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using F1ManagerAvalonia.Servicii;

namespace F1ManagerAvalonia.Formulare
{
    public partial class LoginWindow : Window
    {
        UserService service = new UserService();

        public LoginWindow()
        {
            InitializeComponent();
            LoadHeaderLogo();

            btnLogin.Click += BtnLogin_Click;
            btnRegisterLink.Click += BtnRegisterLink_Click;
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

        private async void BtnLogin_Click(object? sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text?.Trim() ?? "";
            string password = txtPassword.Text?.Trim() ?? "";

            string? role = service.Login(username, password);

            if (role != null)
            {
                await MessageBoxHelper.ShowInfo("Login reușit!");

                this.Hide();

                if (role.ToLower() == "admin")
                {
                    DashboardAdminWindow adminForm = new DashboardAdminWindow();
                    adminForm.Show();
                }
                else
                {
                    DashboardUserWindow userForm = new DashboardUserWindow();
                    userForm.Show();
                }
            }
            else
            {
                string errorMsg = service.LastError ?? "Date greșite!";
                await MessageBoxHelper.ShowInfo(errorMsg, "Eroare");
            }
        }

        private async void BtnRegisterLink_Click(object? sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            await registerWindow.ShowDialog(this);
        }
    }
}