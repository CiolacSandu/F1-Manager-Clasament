namespace F1Manager.Formulare
{
    partial class CurseForm
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
            dateTimePicker = new DateTimePicker();
            label4 = new Label();
            txtTure = new TextBox();
            label3 = new Label();
            txtLocatie = new TextBox();
            label2 = new Label();
            txtNume = new TextBox();
            label1 = new Label();
            labelFormTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            panelForm.SuspendLayout();
            SuspendLayout();

            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(24, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(250, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Calendar Curse";

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

            panelForm.BackColor = Color.FromArgb(36, 36, 36);
            panelForm.Controls.Add(btnSalveaza);
            panelForm.Controls.Add(btnAnuleaza);
            panelForm.Controls.Add(dateTimePicker);
            panelForm.Controls.Add(label4);
            panelForm.Controls.Add(txtTure);
            panelForm.Controls.Add(label3);
            panelForm.Controls.Add(txtLocatie);
            panelForm.Controls.Add(label2);
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
            labelFormTitle.Size = new Size(170, 32);
            labelFormTitle.TabIndex = 0;
            labelFormTitle.Text = "Adaugă Cursa";

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

            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.LightGray;
            label2.Location = new Point(16, 130);
            label2.Name = "label2";
            label2.Size = new Size(63, 23);
            label2.TabIndex = 3;
            label2.Text = "Locație:";
            txtLocatie.BackColor = Color.FromArgb(50, 50, 50);
            txtLocatie.BorderStyle = BorderStyle.FixedSingle;
            txtLocatie.ForeColor = Color.White;
            txtLocatie.Location = new Point(16, 156);
            txtLocatie.Name = "txtLocatie";
            txtLocatie.Size = new Size(208, 27);
            txtLocatie.TabIndex = 4;

            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.LightGray;
            label3.Location = new Point(16, 196);
            label3.Name = "label3";
            label3.Size = new Size(83, 23);
            label3.TabIndex = 5;
            label3.Text = "Nr. Ture:";
            txtTure.BackColor = Color.FromArgb(50, 50, 50);
            txtTure.BorderStyle = BorderStyle.FixedSingle;
            txtTure.ForeColor = Color.White;
            txtTure.Location = new Point(16, 222);
            txtTure.Name = "txtTure";
            txtTure.Size = new Size(208, 27);
            txtTure.TabIndex = 6;

            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.LightGray;
            label4.Location = new Point(16, 262);
            label4.Name = "label4";
            label4.Size = new Size(45, 23);
            label4.TabIndex = 7;
            label4.Text = "Data:";
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(16, 288);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(208, 27);
            dateTimePicker.TabIndex = 8;

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
            Name = "CurseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Curse - F1 Manager";
            Load += new EventHandler(CurseForm_Load);
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
        private Label label2;
        private TextBox txtLocatie;
        private Label label3;
        private TextBox txtTure;
        private Label label4;
        private DateTimePicker dateTimePicker;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}