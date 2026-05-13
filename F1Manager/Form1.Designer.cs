namespace F1Manager;

partial class Form1
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1920, 1080);
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        Text = "F1 Manager";
        DoubleBuffered = true;
    }

    #endregion
}