using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;

namespace F1Manager.Formulare
{
    public partial class ClasamentForm : Form
    {
        private ClasamentService clasamentService = new ClasamentService();
        private string tipClasament;
        private int? cursaId;

        public ClasamentForm(string tip)
        {
            InitializeComponent();
            tipClasament = tip;
            cursaId = null;
            this.Text = tip == "Echipe" ? "Clasament Echipe - F1 Manager" : "Clasament Piloti - F1 Manager";
        }

        public ClasamentForm(string tip, int cursaIdParam)
        {
            InitializeComponent();
            tipClasament = tip;
            cursaId = cursaIdParam;
            this.Text = "Rezultate Cursă - F1 Manager";
        }

        private void ClasamentForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadData();
        }

        private void SetupGrid()
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 28, 43);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Columns.Clear();

                if (tipClasament == "Rezultate" && cursaId.HasValue)
                {
                    var rezultate = clasamentService.GetRezultateByCursa(cursaId.Value);
                    if (rezultate.Count > 0)
                    {
                        labelTitle.Text = $"Rezultate: {rezultate[0].NumeCursa}";
                    }

                    dataGridView.Columns.Add("Pozitie", "#");
                    dataGridView.Columns.Add("Pilot", "Pilot");
                    dataGridView.Columns.Add("Echipa", "Echipa");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    foreach (var r in rezultate)
                    {
                        dataGridView.Rows.Add(r.PozitieFinala, r.NumePilot, r.NumeEchipa, r.Puncte);
                    }
                }
                else if (tipClasament == "Echipe")
                {
                    labelTitle.Text = "Clasament Echipe";
                    dataGridView.Columns.Add("Pozitie", "#");
                    dataGridView.Columns.Add("Echipa", "Echipa");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    var echipe = clasamentService.GetClasamentEchipe();
                    int poz = 1;
                    foreach (var e in echipe)
                    {
                        dataGridView.Rows.Add(poz++, e.NumeEchipa, e.Puncte);
                    }
                }
                else // Piloti
                {
                    labelTitle.Text = "Clasament Piloti";
                    dataGridView.Columns.Add("Pozitie", "#");
                    dataGridView.Columns.Add("Pilot", "Pilot");
                    dataGridView.Columns.Add("Echipa", "Echipa");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    var piloti = clasamentService.GetClasamentGeneral();
                    int poz = 1;
                    foreach (var p in piloti)
                    {
                        dataGridView.Rows.Add(poz++, p.NumePilot, p.NumeEchipa, p.Puncte);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}