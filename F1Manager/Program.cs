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

            Application.Run(new LoginForm());
        }
    }
}