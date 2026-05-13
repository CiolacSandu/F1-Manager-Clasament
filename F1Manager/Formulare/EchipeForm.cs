using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;
using F1Manager.Modele;

namespace F1Manager.Formulare
{
    public partial class EchipeForm : Form
    {
        private EchipaService echipaService = new EchipaService();
        private int? editId = null;

        public EchipeForm()
        {
            InitializeComponent();
        }

        private void EchipeForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Columns.Clear();
                dataGridView.Columns.Add("EchipaID", "ID");
                dataGridView.Columns.Add("Nume", "Nume");
                dataGridView.Columns.Add("Tara", "Țara");
                dataGridView.Columns.Add("Director", "Director");
                dataGridView.Columns[0].Visible = false;

                var echipe = echipaService.GetAll();
                foreach (var e in echipe)
                {
                    dataGridView.Rows.Add(e.EchipaID, e.Nume, e.Tara, e.DirectorEchipa);
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
            labelFormTitle.Text = "Adaugă Echipa";
            txtNume.Text = "";
            txtTara.Text = "";
            txtDirector.Text = "";
            txtBuget.Text = "";
            panelForm.Visible = true;
        }

        private void btnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o echipă!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            editId = Convert.ToInt32(row.Cells[0].Value);
            labelFormTitle.Text = "Editează Echipa";
            txtNume.Text = row.Cells[1].Value?.ToString() ?? "";
            txtTara.Text = row.Cells[2].Value?.ToString() ?? "";
            txtDirector.Text = row.Cells[3].Value?.ToString() ?? "";
            panelForm.Visible = true;
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o echipă!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            int id = Convert.ToInt32(row.Cells[0].Value);
            string nume = row.Cells[1].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show($"Sigur ștergi echipa {nume}?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                echipaService.Delete(id);
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

            decimal? buget = null;
            if (!string.IsNullOrEmpty(txtBuget.Text))
            {
                if (decimal.TryParse(txtBuget.Text, out decimal b))
                    buget = b;
                else
                {
                    MessageBox.Show("Bugetul trebuie să fie un număr!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Echipa echipaNoua = new Echipa
            {
                Nume = nume,
                Tara = txtTara.Text.Trim(),
                DirectorEchipa = txtDirector.Text.Trim(),
                Buget = buget
            };

            if (editId.HasValue)
            {
                echipaNoua.EchipaID = editId.Value;
                echipaService.Update(echipaNoua);
            }
            else
            {
                echipaService.Add(echipaNoua);
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