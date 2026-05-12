using System;
using System.Windows.Forms;
using F1Manager.Formulare;

namespace F1Manager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.ThreadException += (sender, e) =>
            {
                MessageBox.Show($"Eroare neprevăzută: {e.Exception.Message}\n\n{e.Exception.StackTrace}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"Eroare fatală: {ex.Message}\n\n{ex.StackTrace}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            try
            {
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la pornire: {ex.Message}\n\n{ex.StackTrace}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}