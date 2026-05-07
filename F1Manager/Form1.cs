using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace F1Manager
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> clasament = new Dictionary<string, int>();

        private ListBox listBox;
        private TextBox txtNume;
        private TextBox txtPuncte;

        public Form1()
        {
            InitializeComponent();

            this.Text = "F1 Manager + Clasament";
            this.Size = new System.Drawing.Size(500, 400);

            // input nume pilot
            txtNume = new TextBox();
            txtNume.PlaceholderText = "Nume pilot";
            txtNume.Location = new System.Drawing.Point(20, 20);

            // input puncte
            txtPuncte = new TextBox();
            txtPuncte.PlaceholderText = "Puncte";
            txtPuncte.Location = new System.Drawing.Point(200, 20);

            // buton adaugare
            Button btnAdd = new Button();
            btnAdd.Text = "Adaugă / Update";
            btnAdd.Location = new System.Drawing.Point(350, 18);

            btnAdd.Click += BtnAdd_Click;

            // listă clasament
            listBox = new ListBox();
            listBox.Location = new System.Drawing.Point(20, 70);
            listBox.Size = new System.Drawing.Size(430, 250);

            Controls.Add(txtNume);
            Controls.Add(txtPuncte);
            Controls.Add(btnAdd);
            Controls.Add(listBox);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string nume = txtNume.Text;

            if (!int.TryParse(txtPuncte.Text, out int puncte))
            {
                MessageBox.Show("Introdu puncte valide!");
                return;
            }

            if (clasament.ContainsKey(nume))
                clasament[nume] += puncte;
            else
                clasament[nume] = puncte;

            AfisareClasament();
        }

        private void AfisareClasament()
        {
            listBox.Items.Clear();

            var sorted = clasament
                .OrderByDescending(x => x.Value);

            foreach (var p in sorted)
            {
                listBox.Items.Add($"{p.Key} - {p.Value} puncte");
            }
        }
    }
}