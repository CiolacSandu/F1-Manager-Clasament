namespace F1Manager.Formulare
{
    partial class PilotiForm
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
            btnAdauga = new Button();
            btnEditeaza = new Button();
            btnSterge = new Button();
            panelForm = new Panel();
            btnSalveaza = new Button();
            btnAnuleaza = new Button();
            cmbEchipa = new ComboBox();
            label5 = new Label();
            txtVarsta = new TextBox();
            label4 = new Label();
            txtNationalitate = new TextBox();
            label3 = new Label();
            txtNume = new TextBox();
            label1 = new Label();
            labelFormTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            panelForm.SuspendLayout();
            SuspendLayout();

            // labelTitle
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(24, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(200, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Piloți";

            // dataGridView
            dataGridView.Location = new Point(24, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(700, 400);
            dataGridView.TabIndex = 1;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 28, 43);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // btnAdauga
            btnAdauga.BackColor = Color.FromArgb(230, 28, 43);
            btnAdauga.FlatAppearance.BorderSize = 0;
            btnAdauga.FlatStyle = FlatStyle.Flat;
            btnAdauga.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdauga.ForeColor = Color.White;
            btnAdauga.Location = new Point(24, 496);
            btnAdauga.Name = "btnAdauga";
            btnAdauga.Size = new Size(120, 40);
            btnAdauga.TabIndex = 2;
            btnAdauga.Text = "+ Adaugă";
            btnAdauga.UseVisualStyleBackColor = false;
            btnAdauga.Click += new EventHandler(btnAdauga_Click);

            // btnEditeaza
            btnEditeaza.BackColor = Color.FromArgb(50, 50, 50);
            btnEditeaza.FlatAppearance.BorderSize = 0;
            btnEditeaza.FlatStyle = FlatStyle.Flat;
            btnEditeaza.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnEditeaza.ForeColor = Color.White;
            btnEditeaza.Location = new Point(160, 496);
            btnEditeaza.Name = "btnEditeaza";
            btnEditeaza.Size = new Size(120, 40);
            btnEditeaza.TabIndex = 3;
            btnEditeaza.Text = "✏️ Editează";
            btnEditeaza.UseVisualStyleBackColor = false;
            btnEditeaza.Click += new EventHandler(btnEditeaza_Click);

            // btnSterge
            btnSterge.BackColor = Color.FromArgb(180, 40, 40);
            btnSterge.FlatAppearance.BorderSize = 0;
            btnSterge.FlatStyle = FlatStyle.Flat;
            btnSterge.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnSterge.ForeColor = Color.White;
            btnSterge.Location = new Point(296, 496);
            btnSterge.Name = "btnSterge";
            btnSterge.Size = new Size(120, 40);
            btnSterge.TabIndex = 4;
            btnSterge.Text = "🗑️ Șterge";
            btnSterge.UseVisualStyleBackColor = false;
            btnSterge.Click += new EventHandler(btnSterge_Click);

            // panelForm
            panelForm.BackColor = Color.FromArgb(36, 36, 36);
            panelForm.Controls.Add(btnSalveaza);
            panelForm.Controls.Add(btnAnuleaza);
            panelForm.Controls.Add(cmbEchipa);
            panelForm.Controls.Add(label5);
            panelForm.Controls.Add(txtVarsta);
            panelForm.Controls.Add(label4);
            panelForm.Controls.Add(txtNationalitate);
            panelForm.Controls.Add(label3);
            panelForm.Controls.Add(txtNume);
            panelForm.Controls.Add(label1);
            panelForm.Controls.Add(labelFormTitle);
            panelForm.Location = new Point(740, 80);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(240, 460);
            panelForm.TabIndex = 5;
            panelForm.Visible = false;

            labelFormTitle.AutoSize = true;
            labelFormTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelFormTitle.ForeColor = Color.White;
            labelFormTitle.Location = new Point(16, 16);
            labelFormTitle.Name = "labelFormTitle";
            labelFormTitle.Size = new Size(150, 32);
            labelFormTitle.TabIndex = 0;
            labelFormTitle.Text = "Adaugă Pilot";

            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.LightGray;
            label1.Location = new Point(16, 64);
            label1.Name = "label1";
            label1.Size = new Size(55, 23);
            label1.TabIndex = 1;
            label1.Text = "Nume:";

            txtNume.BackColor = Color.FromArgb(50, 50, 50);
            txtNume.BorderStyle = BorderStyle.FixedSingle;
            txtNume.ForeColor = Color.White;
            txtNume.Location = new Point(16, 90);
            txtNume.Name = "txtNume";
            txtNume.Size = new Size(208, 27);
            txtNume.TabIndex = 2;

            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.LightGray;
            label3.Location = new Point(16, 130);
            label3.Name = "label3";
            label3.Size = new Size(103, 23);
            label3.TabIndex = 3;
            label3.Text = "Naționalitate:";

            txtNationalitate.BackColor = Color.FromArgb(50, 50, 50);
            txtNationalitate.BorderStyle = BorderStyle.FixedSingle;
            txtNationalitate.ForeColor = Color.White;
            txtNationalitate.Location = new Point(16, 156);
            txtNationalitate.Name = "txtNationalitate";
            txtNationalitate.Size = new Size(208, 27);
            txtNationalitate.TabIndex = 4;

            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.LightGray;
            label4.Location = new Point(16, 196);
            label4.Name = "label4";
            label4.Size = new Size(57, 23);
            label4.TabIndex = 5;
            label4.Text = "Vârsta:";

            txtVarsta.BackColor = Color.FromArgb(50, 50, 50);
            txtVarsta.BorderStyle = BorderStyle.FixedSingle;
            txtVarsta.ForeColor = Color.White;
            txtVarsta.Location = new Point(16, 222);
            txtVarsta.Name = "txtVarsta";
            txtVarsta.Size = new Size(208, 27);
            txtVarsta.TabIndex = 6;

            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.LightGray;
            label5.Location = new Point(16, 262);
            label5.Name = "label5";
            label5.Size = new Size(60, 23);
            label5.TabIndex = 7;
            label5.Text = "Echipa:";

            cmbEchipa.BackColor = Color.FromArgb(50, 50, 50);
            cmbEchipa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEchipa.FlatStyle = FlatStyle.Flat;
            cmbEchipa.ForeColor = Color.White;
            cmbEchipa.Location = new Point(16, 288);
            cmbEchipa.Name = "cmbEchipa";
            cmbEchipa.Size = new Size(208, 28);
            cmbEchipa.TabIndex = 8;

            btnSalveaza.BackColor = Color.FromArgb(230, 28, 43);
            btnSalveaza.FlatAppearance.BorderSize = 0;
            btnSalveaza.FlatStyle = FlatStyle.Flat;
            btnSalveaza.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnSalveaza.ForeColor = Color.White;
            btnSalveaza.Location = new Point(16, 340);
            btnSalveaza.Name = "btnSalveaza";
            btnSalveaza.Size = new Size(208, 40);
            btnSalveaza.TabIndex = 9;
            btnSalveaza.Text = "💾 Salvează";
            btnSalveaza.UseVisualStyleBackColor = false;
            btnSalveaza.Click += new EventHandler(btnSalveaza_Click);

            btnAnuleaza.BackColor = Color.FromArgb(50, 50, 50);
            btnAnuleaza.FlatAppearance.BorderSize = 0;
            btnAnuleaza.FlatStyle = FlatStyle.Flat;
            btnAnuleaza.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAnuleaza.ForeColor = Color.WhiteSmoke;
            btnAnuleaza.Location = new Point(16, 396);
            btnAnuleaza.Name = "btnAnuleaza";
            btnAnuleaza.Size = new Size(208, 40);
            btnAnuleaza.TabIndex = 10;
            btnAnuleaza.Text = "❌ Anulează";
            btnAnuleaza.UseVisualStyleBackColor = false;
            btnAnuleaza.Click += new EventHandler(btnAnuleaza_Click);

            // PilotiForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 27, 27);
            ClientSize = new Size(1000, 600);
            Controls.Add(panelForm);
            Controls.Add(btnSterge);
            Controls.Add(btnEditeaza);
            Controls.Add(btnAdauga);
            Controls.Add(dataGridView);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "PilotiForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Piloți - F1 Manager";
            Load += new EventHandler(PilotiForm_Load);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dataGridView;
        private Label labelTitle;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private Panel panelForm;
        private Label labelFormTitle;
        private Label label1;
        private TextBox txtNume;
        private Label label3;
        private TextBox txtNationalitate;
        private Label label4;
        private TextBox txtVarsta;
        private Label label5;
        private ComboBox cmbEchipa;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}