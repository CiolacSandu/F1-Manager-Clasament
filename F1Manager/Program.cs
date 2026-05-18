using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using F1Manager.Formulare;

namespace F1Manager
{
    internal static class Program
    {
        // Setează locale-ul thread-ului la nivel de API Windows (0x0418 = Romanian)
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint SetThreadLocale(uint localeId);

        [STAThread]
        static void Main()
        {
            // Forțează locale-ul Windows să fie română (afectează controale native precum DateTimePicker)
            SetThreadLocale(0x0418); // 0x0418 = ro-RO

            CultureInfo romanian = new CultureInfo("ro-RO");
            CultureInfo.DefaultThreadCurrentCulture = romanian;
            CultureInfo.DefaultThreadCurrentUICulture = romanian;
            Thread.CurrentThread.CurrentCulture = romanian;
            Thread.CurrentThread.CurrentUICulture = romanian;

            ApplicationConfiguration.Initialize();

            Application.Run(new LoginForm());
        }
    }
}
