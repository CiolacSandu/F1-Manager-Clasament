using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.BazaDeDate;
using F1Manager.Modele;
using F1Manager.Servicii;

namespace F1Manager.Formulare
{
    public partial class CurseForm : Form
    {
        private DbHelper db = new DbHelper();
        private int? editId = null;
        private bool readOnly = false;

        public CurseForm(bool readOnly = false)
        {
            InitializeComponent();
            this.readOnly = readOnly;
        }

        private void CurseForm_Load(object sender, EventArgs e)
        {
            LoadData();

            if (readOnly)
            {
                btnAdauga.Visible = false;
                btnEditeaza.Visible = false;
                btnSterge.Visible = false;
                panelForm.Visible = false;
                dataGridView.Width = 920;
            }
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Columns.Clear();
                dataGridView.Columns.Add("CursaID", "ID");
                dataGridView.Columns.Add("NumeCursa", "Nume Cursa");
                dataGridView.Columns.Add("Locatie", "Locație");
                dataGridView.Columns.Add("DataCursa", "Data");
                dataGridView.Columns.Add("NumarTure", "Nr Ture");
                dataGridView.Columns.Add("Status", "Status");
                dataGridView.Columns[0].Visible = false;

                DataTable dt = db.ExecuteQuery(@"
                    SELECT c.*, 
                        CASE WHEN EXISTS(SELECT 1 FROM clasament cl WHERE cl.CursaID = c.CursaID) THEN 'Finalizata' ELSE 'Programata' END AS Status
                    FROM curse c ORDER BY c.DataCursa ASC");
                
                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    DateTime? data = row["DataCursa"] != DBNull.Value ? Convert.ToDateTime(row["DataCursa"]) : null;
                    string dataStr = data.HasValue ? data.Value.ToString("dd.MM.yyyy") : "-";
                    dataGridView.Rows.Add(
                        Convert.ToInt32(row["CursaID"]),
                        row["NumeCursa"]?.ToString() ?? "",
                        row["Locatie"]?.ToString() ?? "",
                        dataStr,
                        row["NumarTure"]?.ToString() ?? "-",
                        row["Status"]?.ToString() ?? ""
                    );
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
            labelFormTitle.Text = "Adaugă Cursa";
            txtNume.Text = "";
            txtLocatie.Text = "";
            txtTure.Text = "";
            dateTimePicker.Value = DateTime.Now;
            panelForm.Visible = true;
        }

        private void btnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o cursă!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            editId = Convert.ToInt32(row.Cells[0].Value);
            labelFormTitle.Text = "Editează Cursa";
            txtNume.Text = row.Cells[1].Value?.ToString() ?? "";
            txtLocatie.Text = row.Cells[2].Value?.ToString() ?? "";

            if (DateTime.TryParse(row.Cells[3].Value?.ToString(), out DateTime data))
                dateTimePicker.Value = data;

            txtTure.Text = row.Cells[4].Value?.ToString() ?? "";
            panelForm.Visible = true;
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o cursă!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView.SelectedRows[0];
            if (row.Cells[0].Value == null) return;

            int id = Convert.ToInt32(row.Cells[0].Value);
            string nume = row.Cells[1].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show($"Sigur ștergi cursa {nume}?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                db.ExecuteNonQuery("DELETE FROM curse WHERE CursaID=@id", new MySql.Data.MySqlClient.MySqlParameter[]
                {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", id)
                });
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

            int? ture = null;
            if (!string.IsNullOrEmpty(txtTure.Text))
            {
                if (int.TryParse(txtTure.Text, out int t))
                    ture = t;
                else
                {
                    MessageBox.Show("Numărul de ture trebuie să fie un număr!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string dataStr = dateTimePicker.Value.ToString("yyyy-MM-dd");

            if (editId.HasValue)
            {
                string query = "UPDATE curse SET NumeCursa=@nume, Locatie=@loc, DataCursa=@data, NumarTure=@ture WHERE CursaID=@id";
                db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
                {
                    new MySql.Data.MySqlClient.MySqlParameter("@nume", nume),
                    new MySql.Data.MySqlClient.MySqlParameter("@loc", txtLocatie.Text.Trim()),
                    new MySql.Data.MySqlClient.MySqlParameter("@data", dataStr),
                    new MySql.Data.MySqlClient.MySqlParameter("@ture", ture ?? (object)DBNull.Value),
                    new MySql.Data.MySqlClient.MySqlParameter("@id", editId.Value)
                });
            }
            else
            {
                string query = "INSERT INTO curse (NumeCursa, Locatie, DataCursa, NumarTure) VALUES (@nume, @loc, @data, @ture)";
                db.ExecuteNonQuery(query, new MySql.Data.MySqlClient.MySqlParameter[]
                {
                    new MySql.Data.MySqlClient.MySqlParameter("@nume", nume),
                    new MySql.Data.MySqlClient.MySqlParameter("@loc", txtLocatie.Text.Trim()),
                    new MySql.Data.MySqlClient.MySqlParameter("@data", dataStr),
                    new MySql.Data.MySqlClient.MySqlParameter("@ture", ture ?? (object)DBNull.Value)
                });
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