using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;
using F1Manager.Modele;

namespace F1Manager.Formulare
{
    public partial class PilotiForm : Form
    {
        private PilotService pilotService = new PilotService();
        private EchipaService echipaService = new EchipaService();
        private int? editId = null;
        private bool readOnly = false;

        public PilotiForm(bool readOnly = false)
        {
            InitializeComponent();
            this.readOnly = readOnly;
        }

        private void PilotiForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            ApplyHoverStyles();
            LoadTeams();
            LoadData();

            if (readOnly)
            {
                btnAdauga.Visible = false;
                btnEditeaza.Visible = false;
                btnSterge.Visible = false;
                panelForm.Visible = false;
                // Extend DataGridView width to fill the space
                dataGridView.Width = 920;
            }
        }

        private void ApplyHoverStyles()
        {
            // Hover for action buttons
            btnAdauga.MouseEnter += (s, ev) => btnAdauga.BackColor = Color.FromArgb(255, 60, 60);
            btnAdauga.MouseLeave += (s, ev) => btnAdauga.BackColor = Color.FromArgb(230, 28, 43);

            btnEditeaza.MouseEnter += (s, ev) => btnEditeaza.BackColor = Color.FromArgb(70, 70, 70);
            btnEditeaza.MouseLeave += (s, ev) => btnEditeaza.BackColor = Color.FromArgb(50, 50, 50);

            btnSterge.MouseEnter += (s, ev) => btnSterge.BackColor = Color.FromArgb(220, 60, 60);
            btnSterge.MouseLeave += (s, ev) => btnSterge.BackColor = Color.FromArgb(180, 40, 40);

            btnSalveaza.MouseEnter += (s, ev) => btnSalveaza.BackColor = Color.FromArgb(255, 60, 60);
            btnSalveaza.MouseLeave += (s, ev) => btnSalveaza.BackColor = Color.FromArgb(230, 28, 43);

            btnAnuleaza.MouseEnter += (s, ev) => btnAnuleaza.BackColor = Color.FromArgb(70, 70, 70);
            btnAnuleaza.MouseLeave += (s, ev) => btnAnuleaza.BackColor = Color.FromArgb(50, 50, 50);
        }

        private void LoadTeams()
        {
            cmbEchipa.Items.Clear();
            cmbEchipa.Items.Add(new { Text = "Fără echipă", Value = 0 });
            var echipe = echipaService.GetAll();
            foreach (var echipa in echipe)
            {
                cmbEchipa.Items.Add(new { Text = echipa.Nume, Value = echipa.EchipaID });
            }
            cmbEchipa.DisplayMember = "Text";
            cmbEchipa.ValueMember = "Value";
            cmbEchipa.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Columns.Clear();
                dataGridView.Columns.Add("PilotID", "ID");
                dataGridView.Columns.Add("Nume", "Nume");
                dataGridView.Columns.Add("Nationalitate", "Naționalitate");
                dataGridView.Columns.Add("Varsta", "Vârsta");
                dataGridView.Columns[0].Visible = false;

                var piloti = pilotService.GetAll();
                foreach (var p in piloti)
                {
                    dataGridView.Rows.Add(p.PilotID, p.Nume, p.Nationalitate, p.Varsta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            editId = null;
            labelFormTitle.Text = "Adaugă Pilot";
            txtNume.Text = "";
            txtNationalitate.Text = "";
            txtVarsta.Text = "";
            cmbEchipa.SelectedIndex = 0;
            panelForm.Visible = true;
        }

        private void btnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează un pilot!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null)
            {
                MessageBox.Show("Selectează un pilot valid!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            editId = Convert.ToInt32(row.Cells[0].Value);
            labelFormTitle.Text = "Editează Pilot";
            txtNume.Text = row.Cells[1].Value?.ToString() ?? "";
            txtNationalitate.Text = row.Cells[2].Value?.ToString() ?? "";
            txtVarsta.Text = row.Cells[3].Value?.ToString() ?? "";
            panelForm.Visible = true;
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează un pilot!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            int id = Convert.ToInt32(row.Cells[0].Value);
            string nume = row.Cells[1].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show($"Sigur ștergi pilotul {nume}?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                pilotService.Delete(id);
                LoadData();
            }
        }

        private void btnSalveaza_Click(object sender, EventArgs e)
        {
            string nume = txtNume.Text.Trim();
            if (string.IsNullOrEmpty(nume))
            {
                MessageBox.Show("Numele este obligatoriu!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? varsta = null;
            if (!string.IsNullOrEmpty(txtVarsta.Text))
            {
                if (int.TryParse(txtVarsta.Text, out int v))
                    varsta = v;
                else
                {
                    MessageBox.Show("Vârsta trebuie să fie un număr!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var selectedEchipa = cmbEchipa.SelectedItem;
            int? echipaId = null;
            if (selectedEchipa != null)
            {
                dynamic item = selectedEchipa;
                if (item.Value != 0)
                    echipaId = item.Value;
            }

            Pilot p = new Pilot
            {
                Nume = nume,
                Nationalitate = txtNationalitate.Text.Trim(),
                Varsta = varsta,
                EchipaID = echipaId
            };

            if (editId.HasValue)
            {
                p.PilotID = editId.Value;
                pilotService.Update(p);
            }
            else
            {
                pilotService.Add(p);
            }

            panelForm.Visible = false;
            LoadData();
        }

        private void btnAnuleaza_Click(object sender, EventArgs e)
        {
            panelForm.Visible = false;
            editId = null;
        }
    }
}