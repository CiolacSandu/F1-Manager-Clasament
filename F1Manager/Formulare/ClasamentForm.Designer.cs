namespace F1Manager.Formulare
{
    partial class ClasamentForm
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
            dataGridView = new DataGridView();
            labelTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();

            // labelTitle
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(24, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(250, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Clasament";

            // dataGridView
            dataGridView.Location = new Point(24, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(940, 480);
            dataGridView.TabIndex = 1;

            // ClasamentForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 27, 27);
            ClientSize = new Size(1000, 600);
            Controls.Add(labelTitle);
            Controls.Add(dataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ClasamentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clasament - F1 Manager";
            Load += new EventHandler(ClasamentForm_Load);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dataGridView;
        private Label labelTitle;
    }
}