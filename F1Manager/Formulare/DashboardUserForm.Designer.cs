namespace F1Manager.Formulare
{
    partial class DashboardUserForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1080);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Text = "F1 Manager - Dashboard User";
            BackColor = Color.FromArgb(20, 20, 20);
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}