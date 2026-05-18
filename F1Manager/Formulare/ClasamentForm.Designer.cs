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
            labelSubTitle = new Label();
            btnExportPDF = new Button();
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

            // labelSubTitle
            labelSubTitle.AutoSize = true;
            labelSubTitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubTitle.ForeColor = Color.LightGray;
            labelSubTitle.Location = new Point(24, 72);
            labelSubTitle.Name = "labelSubTitle";
            labelSubTitle.Size = new Size(0, 23);
            labelSubTitle.TabIndex = 1;

            // dataGridView
            dataGridView.Location = new Point(24, 110);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(940, 430);
            dataGridView.TabIndex = 2;

            // btnExportPDF
            btnExportPDF.BackColor = Color.FromArgb(230, 28, 43);
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.Location = new Point(24, 550);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(250, 46);
            btnExportPDF.TabIndex = 3;
            btnExportPDF.Text = "📄 Exportă PDF";
            btnExportPDF.UseVisualStyleBackColor = false;
            btnExportPDF.Click += new EventHandler(btnExportPDF_Click);
            btnExportPDF.MouseEnter += new EventHandler(btnExportPDF_MouseEnter);
            btnExportPDF.MouseLeave += new EventHandler(btnExportPDF_MouseLeave);

            // ClasamentForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 27, 27);
            ClientSize = new Size(1000, 620);
            Controls.Add(btnExportPDF);
            Controls.Add(labelTitle);
            Controls.Add(labelSubTitle);
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
        private Label labelSubTitle;
        private Button btnExportPDF;
    }
}