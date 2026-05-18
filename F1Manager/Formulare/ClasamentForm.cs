using System;
using System.Drawing;
using System.Windows.Forms;
using F1Manager.Servicii;
using iTextSharp.text.pdf;
using System.IO;

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
            ThemeManager.ApplyTheme(this);
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
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;

            // Row styling - alternating colors
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(46, 46, 46);
        }

        private void ApplyHoverStyle()
        {
            btnExportPDF.MouseEnter += (s, ev) =>
            {
                btnExportPDF.BackColor = Color.FromArgb(255, 60, 60);
            };
            btnExportPDF.MouseLeave += (s, ev) =>
            {
                btnExportPDF.BackColor = Color.FromArgb(230, 28, 43);
            };
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Columns.Clear();
                labelSubTitle.Text = "";

                if (tipClasament == "Rezultate" && cursaId.HasValue)
                {
                    var rezultate = clasamentService.GetRezultateByCursa(cursaId.Value);
                    if (rezultate.Count > 0)
                    {
                        labelTitle.Text = $"🏁 Rezultate: {rezultate[0].NumeCursa}";
                    }

                
                    var cursa = clasamentService.GetCursaById(cursaId.Value);
                    if (cursa != null)
                    {
                        labelSubTitle.Text = $"📍 {cursa.Locatie ?? "Necunoscută"}  |  📅 {cursa.DataFormatted}  |  🏎️ {cursa.NumarTure ?? 0} ture";
                    }

                    dataGridView.Columns.Add("Pozitie", "Poziție");
                    dataGridView.Columns.Add("Pilot", "Pilot");
                    dataGridView.Columns.Add("Echipa", "Echipă");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    foreach (var r in rezultate)
                    {
                        dataGridView.Rows.Add(r.PozitieFinala, r.NumePilot, r.NumeEchipa, r.Puncte);
                    }

                    // Gold/Silver/Bronze coloring for top 3
                    ColorTop3Rows();
                }
                else if (tipClasament == "Echipe")
                {
                    labelTitle.Text = "🏢 Clasament Echipe";
                    dataGridView.Columns.Add("Pozitie", "Poziție");
                    dataGridView.Columns.Add("Echipa", "Echipă");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    var echipe = clasamentService.GetClasamentEchipe();
                    int poz = 1;
                    foreach (var e in echipe)
                    {
                        dataGridView.Rows.Add(poz++, e.NumeEchipa, e.Puncte);
                    }

                    ColorTop3Rows();
                }
                else // Piloti
                {
                    labelTitle.Text = "🏆 Clasament Piloti";
                    dataGridView.Columns.Add("Pozitie", "Poziție");
                    dataGridView.Columns.Add("Pilot", "Pilot");
                    dataGridView.Columns.Add("Echipa", "Echipă");
                    dataGridView.Columns.Add("Puncte", "Puncte");

                    var piloti = clasamentService.GetClasamentGeneral();
                    int poz = 1;
                    foreach (var p in piloti)
                    {
                        dataGridView.Rows.Add(poz++, p.NumePilot, p.NumeEchipa, p.Puncte);
                    }

                    ColorTop3Rows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorTop3Rows()
        {
            try
            {
                if (dataGridView.Rows.Count >= 1)
                {
                    dataGridView.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(60, 50, 20);
                    dataGridView.Rows[0].DefaultCellStyle.ForeColor = Color.Gold;
                    dataGridView.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
                }
                if (dataGridView.Rows.Count >= 2)
                {
                    dataGridView.Rows[1].DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 50);
                    dataGridView.Rows[1].DefaultCellStyle.ForeColor = Color.Silver;
                }
                if (dataGridView.Rows.Count >= 3)
                {
                    dataGridView.Rows[2].DefaultCellStyle.BackColor = Color.FromArgb(50, 40, 35);
                    dataGridView.Rows[2].DefaultCellStyle.ForeColor = Color.FromArgb(205, 127, 50);
                }
            }
            catch { /* Ignore styling errors */ }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Nu există date de exportat.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = $"{labelTitle.Text.Replace("🏆 ", "").Replace("🏢 ", "").Replace("🏁 ", "").Replace(" ", "_")}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Title
                        iTextSharp.text.Font titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 18, 1, iTextSharp.text.BaseColor.BLACK);
                        doc.Add(new iTextSharp.text.Paragraph(labelTitle.Text.Replace("🏆 ", "").Replace("🏢 ", "").Replace("🏁 ", ""), titleFont));
                        doc.Add(new iTextSharp.text.Paragraph(" "));

                        // Subtitle
                        if (!string.IsNullOrEmpty(labelSubTitle.Text))
                        {
                            iTextSharp.text.Font subFont = iTextSharp.text.FontFactory.GetFont("Arial", 11, 0, iTextSharp.text.BaseColor.DARK_GRAY);
                            doc.Add(new iTextSharp.text.Paragraph(labelSubTitle.Text, subFont));
                            doc.Add(new iTextSharp.text.Paragraph(" "));
                        }

                        // PDF Table
                        PdfPTable pdfTable = new PdfPTable(dataGridView.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        // Header style
                        iTextSharp.text.Font headerFont = iTextSharp.text.FontFactory.GetFont("Arial", 10, 1, iTextSharp.text.BaseColor.WHITE);
                        iTextSharp.text.BaseColor headerBg = new iTextSharp.text.BaseColor(200, 30, 30);

                        // Headers
                        foreach (DataGridViewColumn col in dataGridView.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase(col.HeaderText, headerFont));
                            cell.BackgroundColor = headerBg;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Padding = 6;
                            pdfTable.AddCell(cell);
                        }

                        // Data rows
                        iTextSharp.text.Font rowFont = iTextSharp.text.FontFactory.GetFont("Arial", 10, 0, iTextSharp.text.BaseColor.BLACK);
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (row.IsNewRow) continue;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string text = cell.Value?.ToString() ?? "";
                                PdfPCell pdfCell = new PdfPCell(new iTextSharp.text.Phrase(text, rowFont));
                                pdfCell.Padding = 5;
                                pdfCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                pdfTable.AddCell(pdfCell);
                            }
                        }

                        doc.Add(pdfTable);
                        doc.Close();
                        writer.Close();
                    }

                    MessageBox.Show("PDF-ul a fost generat cu succes!\n" + saveDialog.FileName,
                        "Export Reușit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la exportul PDF: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}