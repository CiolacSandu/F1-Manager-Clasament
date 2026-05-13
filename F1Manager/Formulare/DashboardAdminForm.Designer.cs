namespace F1Manager.Formulare
{
    partial class DashboardAdminForm
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
            panelSidebar = new Panel();
            buttonLogout = new Button();
            buttonClasamentEchipe = new Button();
            buttonClasament = new Button();
            buttonCurse = new Button();
            buttonEchipe = new Button();
            buttonPiloti = new Button();
            buttonDashboard = new Button();
            labelBrand = new Label();
            panelHeader = new Panel();
            labelSubHeader = new Label();
            labelHeader = new Label();
            panelContent = new Panel();
            btnBackup = new Button();
            btnActualizeazaClasament = new Button();
            btnVeziClasamentComplet = new Button();
            btnAdaugaPilot = new Button();
            panelTopPiloti = new Panel();
            labelTopPilotiTitle = new Label();
            panelTop3 = new Panel();
            panelTop3_1 = new Panel();
            labelTop1Puncte = new Label();
            labelTop1Echipa = new Label();
            labelTop1Nume = new Label();
            labelTop1Poz = new Label();
            panelTop3_2 = new Panel();
            labelTop2Puncte = new Label();
            labelTop2Echipa = new Label();
            labelTop2Nume = new Label();
            labelTop2Poz = new Label();
            panelTop3_3 = new Panel();
            labelTop3Puncte = new Label();
            labelTop3Echipa = new Label();
            labelTop3Nume = new Label();
            labelTop3Poz = new Label();
            panelStatsTop = new Panel();
            labelStatsTitle = new Label();
            labelPilotiInregistrati = new Label();
            labelTotalPuncte = new Label();
            labelUrmatoareaCursa = new Label();
            panelCalendarPreview = new Panel();
            labelCalendarTitle = new Label();
            btnVeziCalendarComplet = new Button();
            labelCursa1 = new Label();
            labelCursa2 = new Label();
            labelCursa3 = new Label();
            panelResultsPreview = new Panel();
            labelResultsTitle = new Label();
            btnVeziRezultateComplet = new Button();
            labelRezultat1 = new Label();
            labelRezultat2 = new Label();
            labelRezultat3 = new Label();
            labelRezultatCursa = new Label();
            panelSidebar.SuspendLayout();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelTopPiloti.SuspendLayout();
            panelTop3.SuspendLayout();
            panelTop3_1.SuspendLayout();
            panelTop3_2.SuspendLayout();
            panelTop3_3.SuspendLayout();
            panelStatsTop.SuspendLayout();
            panelCalendarPreview.SuspendLayout();
            panelResultsPreview.SuspendLayout();
            SuspendLayout();

            // panelSidebar
            panelSidebar.BackColor = Color.FromArgb(18, 18, 18);
            panelSidebar.Controls.Add(buttonLogout);
            panelSidebar.Controls.Add(buttonClasamentEchipe);
            panelSidebar.Controls.Add(buttonClasament);
            panelSidebar.Controls.Add(buttonCurse);
            panelSidebar.Controls.Add(buttonEchipe);
            panelSidebar.Controls.Add(buttonPiloti);
            panelSidebar.Controls.Add(buttonDashboard);
            panelSidebar.Controls.Add(labelBrand);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(250, 800);
            panelSidebar.TabIndex = 0;

            // labelBrand
            labelBrand.AutoSize = true;
            labelBrand.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            labelBrand.ForeColor = Color.FromArgb(230, 28, 43);
            labelBrand.Location = new Point(24, 24);
            labelBrand.Name = "labelBrand";
            labelBrand.Size = new Size(165, 37);
            labelBrand.TabIndex = 0;
            labelBrand.Text = "F1 Manager";

            // buttonDashboard
            buttonDashboard.FlatAppearance.BorderSize = 0;
            buttonDashboard.FlatStyle = FlatStyle.Flat;
            buttonDashboard.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonDashboard.ForeColor = Color.WhiteSmoke;
            buttonDashboard.Location = new Point(12, 90);
            buttonDashboard.Name = "buttonDashboard";
            buttonDashboard.Size = new Size(226, 46);
            buttonDashboard.TabIndex = 1;
            buttonDashboard.Text = "   📊 Dashboard";
            buttonDashboard.TextAlign = ContentAlignment.MiddleLeft;
            buttonDashboard.UseVisualStyleBackColor = true;
            buttonDashboard.Click += new EventHandler(buttonDashboard_Click);
            buttonDashboard.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonDashboard.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonPiloti
            buttonPiloti.FlatAppearance.BorderSize = 0;
            buttonPiloti.FlatStyle = FlatStyle.Flat;
            buttonPiloti.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPiloti.ForeColor = Color.WhiteSmoke;
            buttonPiloti.Location = new Point(12, 146);
            buttonPiloti.Name = "buttonPiloti";
            buttonPiloti.Size = new Size(226, 46);
            buttonPiloti.TabIndex = 2;
            buttonPiloti.Text = "   🏎️ Piloti";
            buttonPiloti.TextAlign = ContentAlignment.MiddleLeft;
            buttonPiloti.UseVisualStyleBackColor = true;
            buttonPiloti.Click += new EventHandler(buttonPiloti_Click);
            buttonPiloti.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonPiloti.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonEchipe
            buttonEchipe.FlatAppearance.BorderSize = 0;
            buttonEchipe.FlatStyle = FlatStyle.Flat;
            buttonEchipe.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonEchipe.ForeColor = Color.WhiteSmoke;
            buttonEchipe.Location = new Point(12, 202);
            buttonEchipe.Name = "buttonEchipe";
            buttonEchipe.Size = new Size(226, 46);
            buttonEchipe.TabIndex = 3;
            buttonEchipe.Text = "   🏁 Echipe";
            buttonEchipe.TextAlign = ContentAlignment.MiddleLeft;
            buttonEchipe.UseVisualStyleBackColor = true;
            buttonEchipe.Click += new EventHandler(buttonEchipe_Click);
            buttonEchipe.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonEchipe.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonCurse
            buttonCurse.FlatAppearance.BorderSize = 0;
            buttonCurse.FlatStyle = FlatStyle.Flat;
            buttonCurse.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCurse.ForeColor = Color.WhiteSmoke;
            buttonCurse.Location = new Point(12, 258);
            buttonCurse.Name = "buttonCurse";
            buttonCurse.Size = new Size(226, 46);
            buttonCurse.TabIndex = 4;
            buttonCurse.Text = "   📅 Curse";
            buttonCurse.TextAlign = ContentAlignment.MiddleLeft;
            buttonCurse.UseVisualStyleBackColor = true;
            buttonCurse.Click += new EventHandler(buttonCurse_Click);
            buttonCurse.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonCurse.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonClasament
            buttonClasament.FlatAppearance.BorderSize = 0;
            buttonClasament.FlatStyle = FlatStyle.Flat;
            buttonClasament.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonClasament.ForeColor = Color.WhiteSmoke;
            buttonClasament.Location = new Point(12, 314);
            buttonClasament.Name = "buttonClasament";
            buttonClasament.Size = new Size(226, 46);
            buttonClasament.TabIndex = 5;
            buttonClasament.Text = "   🏆 Clasament Piloti";
            buttonClasament.TextAlign = ContentAlignment.MiddleLeft;
            buttonClasament.UseVisualStyleBackColor = true;
            buttonClasament.Click += new EventHandler(buttonClasament_Click);
            buttonClasament.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonClasament.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonClasamentEchipe
            buttonClasamentEchipe.FlatAppearance.BorderSize = 0;
            buttonClasamentEchipe.FlatStyle = FlatStyle.Flat;
            buttonClasamentEchipe.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonClasamentEchipe.ForeColor = Color.WhiteSmoke;
            buttonClasamentEchipe.Location = new Point(12, 370);
            buttonClasamentEchipe.Name = "buttonClasamentEchipe";
            buttonClasamentEchipe.Size = new Size(226, 46);
            buttonClasamentEchipe.TabIndex = 6;
            buttonClasamentEchipe.Text = "   🏢 Clasament Echipe";
            buttonClasamentEchipe.TextAlign = ContentAlignment.MiddleLeft;
            buttonClasamentEchipe.UseVisualStyleBackColor = true;
            buttonClasamentEchipe.Click += new EventHandler(buttonClasamentEchipe_Click);
            buttonClasamentEchipe.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonClasamentEchipe.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // buttonLogout
            buttonLogout.FlatAppearance.BorderSize = 0;
            buttonLogout.FlatStyle = FlatStyle.Flat;
            buttonLogout.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLogout.ForeColor = Color.FromArgb(200, 80, 80);
            buttonLogout.Location = new Point(12, 740);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(226, 46);
            buttonLogout.TabIndex = 7;
            buttonLogout.Text = "   🚪 Logout";
            buttonLogout.TextAlign = ContentAlignment.MiddleLeft;
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += new EventHandler(buttonLogout_Click);
            buttonLogout.MouseEnter += new EventHandler(NavButton_MouseEnter);
            buttonLogout.MouseLeave += new EventHandler(NavButton_MouseLeave);

            // panelHeader
            panelHeader.BackColor = Color.FromArgb(24, 24, 24);
            panelHeader.Controls.Add(labelSubHeader);
            panelHeader.Controls.Add(labelHeader);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(250, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(950, 100);
            panelHeader.TabIndex = 1;

            // labelHeader
            labelHeader.AutoSize = true;
            labelHeader.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold, GraphicsUnit.Point);
            labelHeader.ForeColor = Color.White;
            labelHeader.Location = new Point(24, 18);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(322, 50);
            labelHeader.TabIndex = 0;
            labelHeader.Text = "Dashboard Admin";

            // labelSubHeader
            labelSubHeader.AutoSize = true;
            labelSubHeader.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubHeader.ForeColor = Color.LightGray;
            labelSubHeader.Location = new Point(24, 68);
            labelSubHeader.Name = "labelSubHeader";
            labelSubHeader.Size = new Size(500, 23);
            labelSubHeader.TabIndex = 1;
            labelSubHeader.Text = "Gestionează piloții, echipele, cursele și clasamentele sezonului.";

            // panelContent
            panelContent.AutoScroll = true;
            panelContent.BackColor = Color.FromArgb(27, 27, 27);
            panelContent.Controls.Add(btnBackup);
            panelContent.Controls.Add(btnActualizeazaClasament);
            panelContent.Controls.Add(btnVeziClasamentComplet);
            panelContent.Controls.Add(btnAdaugaPilot);
            panelContent.Controls.Add(panelTopPiloti);
            panelContent.Controls.Add(panelStatsTop);
            panelContent.Controls.Add(panelCalendarPreview);
            panelContent.Controls.Add(panelResultsPreview);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(250, 100);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(950, 700);
            panelContent.TabIndex = 2;

            // panelStatsTop
            panelStatsTop.BackColor = Color.FromArgb(36, 36, 36);
            panelStatsTop.Controls.Add(labelStatsTitle);
            panelStatsTop.Controls.Add(labelPilotiInregistrati);
            panelStatsTop.Controls.Add(labelTotalPuncte);
            panelStatsTop.Controls.Add(labelUrmatoareaCursa);
            panelStatsTop.Location = new Point(20, 20);
            panelStatsTop.Name = "panelStatsTop";
            panelStatsTop.Padding = new Padding(16);
            panelStatsTop.Size = new Size(890, 100);
            panelStatsTop.TabIndex = 0;

            // labelStatsTitle
            labelStatsTitle.AutoSize = true;
            labelStatsTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelStatsTitle.ForeColor = Color.White;
            labelStatsTitle.Location = new Point(16, 8);
            labelStatsTitle.Name = "labelStatsTitle";
            labelStatsTitle.Size = new Size(90, 32);
            labelStatsTitle.TabIndex = 0;
            labelStatsTitle.Text = "Statistici";

            // labelPilotiInregistrati
            labelPilotiInregistrati.AutoSize = true;
            labelPilotiInregistrati.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPilotiInregistrati.ForeColor = Color.LightGray;
            labelPilotiInregistrati.Location = new Point(16, 52);
            labelPilotiInregistrati.Name = "labelPilotiInregistrati";
            labelPilotiInregistrati.Size = new Size(200, 25);
            labelPilotiInregistrati.TabIndex = 1;
            labelPilotiInregistrati.Text = "Piloți înregistrați: 0";

            // labelTotalPuncte
            labelTotalPuncte.AutoSize = true;
            labelTotalPuncte.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelTotalPuncte.ForeColor = Color.LightGray;
            labelTotalPuncte.Location = new Point(320, 52);
            labelTotalPuncte.Name = "labelTotalPuncte";
            labelTotalPuncte.Size = new Size(180, 25);
            labelTotalPuncte.TabIndex = 2;
            labelTotalPuncte.Text = "Puncte acumulate: 0";

            // labelUrmatoareaCursa
            labelUrmatoareaCursa.AutoSize = true;
            labelUrmatoareaCursa.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelUrmatoareaCursa.ForeColor = Color.FromArgb(230, 28, 43);
            labelUrmatoareaCursa.Location = new Point(610, 52);
            labelUrmatoareaCursa.Name = "labelUrmatoareaCursa";
            labelUrmatoareaCursa.Size = new Size(220, 25);
            labelUrmatoareaCursa.TabIndex = 3;
            labelUrmatoareaCursa.Text = "Următoarea cursă: -";

            // panelTopPiloti
            panelTopPiloti.BackColor = Color.FromArgb(30, 30, 30);
            panelTopPiloti.Controls.Add(labelTopPilotiTitle);
            panelTopPiloti.Controls.Add(panelTop3);
            panelTopPiloti.Location = new Point(20, 136);
            panelTopPiloti.Name = "panelTopPiloti";
            panelTopPiloti.Padding = new Padding(16);
            panelTopPiloti.Size = new Size(890, 200);
            panelTopPiloti.TabIndex = 1;

            // labelTopPilotiTitle
            labelTopPilotiTitle.AutoSize = true;
            labelTopPilotiTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTopPilotiTitle.ForeColor = Color.White;
            labelTopPilotiTitle.Location = new Point(16, 8);
            labelTopPilotiTitle.Name = "labelTopPilotiTitle";
            labelTopPilotiTitle.Size = new Size(170, 32);
            labelTopPilotiTitle.TabIndex = 0;
            labelTopPilotiTitle.Text = "Top 3 Piloți";

            // panelTop3
            panelTop3.Controls.Add(panelTop3_1);
            panelTop3.Controls.Add(panelTop3_2);
            panelTop3.Controls.Add(panelTop3_3);
            panelTop3.Location = new Point(16, 48);
            panelTop3.Name = "panelTop3";
            panelTop3.Size = new Size(858, 140);
            panelTop3.TabIndex = 1;

            // panelTop3_1 - POZITIA 1
            panelTop3_1.BackColor = Color.FromArgb(50, 40, 20);
            panelTop3_1.BorderStyle = BorderStyle.FixedSingle;
            panelTop3_1.Controls.Add(labelTop1Puncte);
            panelTop3_1.Controls.Add(labelTop1Echipa);
            panelTop3_1.Controls.Add(labelTop1Nume);
            panelTop3_1.Controls.Add(labelTop1Poz);
            panelTop3_1.Location = new Point(0, 0);
            panelTop3_1.Name = "panelTop3_1";
            panelTop3_1.Size = new Size(280, 130);
            panelTop3_1.TabIndex = 0;

            labelTop1Poz.AutoSize = true;
            labelTop1Poz.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop1Poz.ForeColor = Color.Gold;
            labelTop1Poz.Location = new Point(12, 8);
            labelTop1Poz.Name = "labelTop1Poz";
            labelTop1Poz.Size = new Size(57, 46);
            labelTop1Poz.TabIndex = 0;
            labelTop1Poz.Text = "#1";

            labelTop1Nume.AutoSize = true;
            labelTop1Nume.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop1Nume.ForeColor = Color.White;
            labelTop1Nume.Location = new Point(12, 56);
            labelTop1Nume.Name = "labelTop1Nume";
            labelTop1Nume.Size = new Size(100, 32);
            labelTop1Nume.TabIndex = 1;
            labelTop1Nume.Text = "Nume";

            labelTop1Echipa.AutoSize = true;
            labelTop1Echipa.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelTop1Echipa.ForeColor = Color.LightGray;
            labelTop1Echipa.Location = new Point(12, 90);
            labelTop1Echipa.Name = "labelTop1Echipa";
            labelTop1Echipa.Size = new Size(60, 23);
            labelTop1Echipa.TabIndex = 2;
            labelTop1Echipa.Text = "Echipa";

            labelTop1Puncte.AutoSize = true;
            labelTop1Puncte.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop1Puncte.ForeColor = Color.Gold;
            labelTop1Puncte.Location = new Point(180, 44);
            labelTop1Puncte.Name = "labelTop1Puncte";
            labelTop1Puncte.Size = new Size(80, 41);
            labelTop1Puncte.TabIndex = 3;
            labelTop1Puncte.Text = "0 P";

            // panelTop3_2 - POZITIA 2
            panelTop3_2.BackColor = Color.FromArgb(36, 36, 40);
            panelTop3_2.BorderStyle = BorderStyle.FixedSingle;
            panelTop3_2.Controls.Add(labelTop2Puncte);
            panelTop3_2.Controls.Add(labelTop2Echipa);
            panelTop3_2.Controls.Add(labelTop2Nume);
            panelTop3_2.Controls.Add(labelTop2Poz);
            panelTop3_2.Location = new Point(290, 0);
            panelTop3_2.Name = "panelTop3_2";
            panelTop3_2.Size = new Size(280, 130);
            panelTop3_2.TabIndex = 1;

            labelTop2Poz.AutoSize = true;
            labelTop2Poz.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop2Poz.ForeColor = Color.Silver;
            labelTop2Poz.Location = new Point(12, 8);
            labelTop2Poz.Name = "labelTop2Poz";
            labelTop2Poz.Size = new Size(57, 46);
            labelTop2Poz.TabIndex = 0;
            labelTop2Poz.Text = "#2";

            labelTop2Nume.AutoSize = true;
            labelTop2Nume.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop2Nume.ForeColor = Color.White;
            labelTop2Nume.Location = new Point(12, 56);
            labelTop2Nume.Name = "labelTop2Nume";
            labelTop2Nume.Size = new Size(100, 32);
            labelTop2Nume.TabIndex = 1;
            labelTop2Nume.Text = "Nume";

            labelTop2Echipa.AutoSize = true;
            labelTop2Echipa.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelTop2Echipa.ForeColor = Color.LightGray;
            labelTop2Echipa.Location = new Point(12, 90);
            labelTop2Echipa.Name = "labelTop2Echipa";
            labelTop2Echipa.Size = new Size(60, 23);
            labelTop2Echipa.TabIndex = 2;
            labelTop2Echipa.Text = "Echipa";

            labelTop2Puncte.AutoSize = true;
            labelTop2Puncte.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop2Puncte.ForeColor = Color.Silver;
            labelTop2Puncte.Location = new Point(180, 44);
            labelTop2Puncte.Name = "labelTop2Puncte";
            labelTop2Puncte.Size = new Size(80, 41);
            labelTop2Puncte.TabIndex = 3;
            labelTop2Puncte.Text = "0 P";

            // panelTop3_3 - POZITIA 3
            panelTop3_3.BackColor = Color.FromArgb(36, 30, 28);
            panelTop3_3.BorderStyle = BorderStyle.FixedSingle;
            panelTop3_3.Controls.Add(labelTop3Puncte);
            panelTop3_3.Controls.Add(labelTop3Echipa);
            panelTop3_3.Controls.Add(labelTop3Nume);
            panelTop3_3.Controls.Add(labelTop3Poz);
            panelTop3_3.Location = new Point(580, 0);
            panelTop3_3.Name = "panelTop3_3";
            panelTop3_3.Size = new Size(280, 130);
            panelTop3_3.TabIndex = 2;

            labelTop3Poz.AutoSize = true;
            labelTop3Poz.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop3Poz.ForeColor = Color.FromArgb(205, 127, 50);
            labelTop3Poz.Location = new Point(12, 8);
            labelTop3Poz.Name = "labelTop3Poz";
            labelTop3Poz.Size = new Size(57, 46);
            labelTop3Poz.TabIndex = 0;
            labelTop3Poz.Text = "#3";

            labelTop3Nume.AutoSize = true;
            labelTop3Nume.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop3Nume.ForeColor = Color.White;
            labelTop3Nume.Location = new Point(12, 56);
            labelTop3Nume.Name = "labelTop3Nume";
            labelTop3Nume.Size = new Size(100, 32);
            labelTop3Nume.TabIndex = 1;
            labelTop3Nume.Text = "Nume";

            labelTop3Echipa.AutoSize = true;
            labelTop3Echipa.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelTop3Echipa.ForeColor = Color.LightGray;
            labelTop3Echipa.Location = new Point(12, 90);
            labelTop3Echipa.Name = "labelTop3Echipa";
            labelTop3Echipa.Size = new Size(60, 23);
            labelTop3Echipa.TabIndex = 2;
            labelTop3Echipa.Text = "Echipa";

            labelTop3Puncte.AutoSize = true;
            labelTop3Puncte.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTop3Puncte.ForeColor = Color.FromArgb(205, 127, 50);
            labelTop3Puncte.Location = new Point(180, 44);
            labelTop3Puncte.Name = "labelTop3Puncte";
            labelTop3Puncte.Size = new Size(80, 41);
            labelTop3Puncte.TabIndex = 3;
            labelTop3Puncte.Text = "0 P";

            // Buttons row
            btnAdaugaPilot.BackColor = Color.FromArgb(230, 28, 43);
            btnAdaugaPilot.FlatAppearance.BorderSize = 0;
            btnAdaugaPilot.FlatStyle = FlatStyle.Flat;
            btnAdaugaPilot.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdaugaPilot.ForeColor = Color.White;
            btnAdaugaPilot.Location = new Point(20, 352);
            btnAdaugaPilot.Name = "btnAdaugaPilot";
            btnAdaugaPilot.Size = new Size(180, 40);
            btnAdaugaPilot.TabIndex = 2;
            btnAdaugaPilot.Text = "+ Adaugă Pilot";
            btnAdaugaPilot.UseVisualStyleBackColor = false;
            btnAdaugaPilot.Click += new EventHandler(btnAdaugaPilot_Click);
            btnAdaugaPilot.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnAdaugaPilot.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            btnVeziClasamentComplet.BackColor = Color.FromArgb(32, 32, 32);
            btnVeziClasamentComplet.FlatAppearance.BorderSize = 0;
            btnVeziClasamentComplet.FlatStyle = FlatStyle.Flat;
            btnVeziClasamentComplet.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnVeziClasamentComplet.ForeColor = Color.WhiteSmoke;
            btnVeziClasamentComplet.Location = new Point(210, 352);
            btnVeziClasamentComplet.Name = "btnVeziClasamentComplet";
            btnVeziClasamentComplet.Size = new Size(200, 40);
            btnVeziClasamentComplet.TabIndex = 3;
            btnVeziClasamentComplet.Text = "📋 Vezi Clasament Complet";
            btnVeziClasamentComplet.UseVisualStyleBackColor = false;
            btnVeziClasamentComplet.Click += new EventHandler(btnVeziClasamentComplet_Click);
            btnVeziClasamentComplet.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnVeziClasamentComplet.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            // panelCalendarPreview
            panelCalendarPreview.BackColor = Color.FromArgb(30, 30, 30);
            panelCalendarPreview.Controls.Add(btnVeziCalendarComplet);
            panelCalendarPreview.Controls.Add(labelCursa3);
            panelCalendarPreview.Controls.Add(labelCursa2);
            panelCalendarPreview.Controls.Add(labelCursa1);
            panelCalendarPreview.Controls.Add(labelCalendarTitle);
            panelCalendarPreview.Location = new Point(20, 408);
            panelCalendarPreview.Name = "panelCalendarPreview";
            panelCalendarPreview.Padding = new Padding(16);
            panelCalendarPreview.Size = new Size(435, 210);
            panelCalendarPreview.TabIndex = 4;

            // labelCalendarTitle
            labelCalendarTitle.AutoSize = true;
            labelCalendarTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelCalendarTitle.ForeColor = Color.White;
            labelCalendarTitle.Location = new Point(16, 8);
            labelCalendarTitle.Name = "labelCalendarTitle";
            labelCalendarTitle.Size = new Size(193, 32);
            labelCalendarTitle.TabIndex = 0;
            labelCalendarTitle.Text = "Calendar Curse";

            // labelCursa1
            labelCursa1.AutoSize = true;
            labelCursa1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCursa1.ForeColor = Color.LightGray;
            labelCursa1.Location = new Point(16, 56);
            labelCursa1.Name = "labelCursa1";
            labelCursa1.Size = new Size(150, 25);
            labelCursa1.TabIndex = 1;
            labelCursa1.Text = "Cursa 1 - Data";

            // labelCursa2
            labelCursa2.AutoSize = true;
            labelCursa2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCursa2.ForeColor = Color.LightGray;
            labelCursa2.Location = new Point(16, 90);
            labelCursa2.Name = "labelCursa2";
            labelCursa2.Size = new Size(150, 25);
            labelCursa2.TabIndex = 2;
            labelCursa2.Text = "Cursa 2 - Data";

            // labelCursa3
            labelCursa3.AutoSize = true;
            labelCursa3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCursa3.ForeColor = Color.LightGray;
            labelCursa3.Location = new Point(16, 124);
            labelCursa3.Name = "labelCursa3";
            labelCursa3.Size = new Size(150, 25);
            labelCursa3.TabIndex = 3;
            labelCursa3.Text = "Cursa 3 - Data";

            // btnVeziCalendarComplet
            btnVeziCalendarComplet.BackColor = Color.FromArgb(32, 32, 32);
            btnVeziCalendarComplet.FlatAppearance.BorderSize = 0;
            btnVeziCalendarComplet.FlatStyle = FlatStyle.Flat;
            btnVeziCalendarComplet.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnVeziCalendarComplet.ForeColor = Color.WhiteSmoke;
            btnVeziCalendarComplet.Location = new Point(240, 158);
            btnVeziCalendarComplet.Name = "btnVeziCalendarComplet";
            btnVeziCalendarComplet.Size = new Size(180, 36);
            btnVeziCalendarComplet.TabIndex = 4;
            btnVeziCalendarComplet.Text = "📅 Vezi Calendar Complet";
            btnVeziCalendarComplet.UseVisualStyleBackColor = false;
            btnVeziCalendarComplet.Click += new EventHandler(btnVeziCalendarComplet_Click);
            btnVeziCalendarComplet.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnVeziCalendarComplet.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            // panelResultsPreview
            panelResultsPreview.BackColor = Color.FromArgb(30, 30, 30);
            panelResultsPreview.Controls.Add(btnVeziRezultateComplet);
            panelResultsPreview.Controls.Add(labelRezultatCursa);
            panelResultsPreview.Controls.Add(labelRezultat3);
            panelResultsPreview.Controls.Add(labelRezultat2);
            panelResultsPreview.Controls.Add(labelRezultat1);
            panelResultsPreview.Controls.Add(labelResultsTitle);
            panelResultsPreview.Location = new Point(475, 408);
            panelResultsPreview.Name = "panelResultsPreview";
            panelResultsPreview.Padding = new Padding(16);
            panelResultsPreview.Size = new Size(435, 210);
            panelResultsPreview.TabIndex = 5;

            // labelResultsTitle
            labelResultsTitle.AutoSize = true;
            labelResultsTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelResultsTitle.ForeColor = Color.White;
            labelResultsTitle.Location = new Point(16, 8);
            labelResultsTitle.Name = "labelResultsTitle";
            labelResultsTitle.Size = new Size(184, 32);
            labelResultsTitle.TabIndex = 0;
            labelResultsTitle.Text = "Ultime Rezultate";

            // labelRezultatCursa
            labelRezultatCursa.AutoSize = true;
            labelRezultatCursa.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point);
            labelRezultatCursa.ForeColor = Color.FromArgb(230, 28, 43);
            labelRezultatCursa.Location = new Point(16, 44);
            labelRezultatCursa.Name = "labelRezultatCursa";
            labelRezultatCursa.Size = new Size(100, 23);
            labelRezultatCursa.TabIndex = 1;
            labelRezultatCursa.Text = "Nume Curse";

            // labelRezultat1
            labelRezultat1.AutoSize = true;
            labelRezultat1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRezultat1.ForeColor = Color.LightGray;
            labelRezultat1.Location = new Point(16, 76);
            labelRezultat1.Name = "labelRezultat1";
            labelRezultat1.Size = new Size(100, 25);
            labelRezultat1.TabIndex = 2;
            labelRezultat1.Text = "1. Pilot";

            // labelRezultat2
            labelRezultat2.AutoSize = true;
            labelRezultat2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRezultat2.ForeColor = Color.LightGray;
            labelRezultat2.Location = new Point(16, 106);
            labelRezultat2.Name = "labelRezultat2";
            labelRezultat2.Size = new Size(100, 25);
            labelRezultat2.TabIndex = 3;
            labelRezultat2.Text = "2. Pilot";

            // labelRezultat3
            labelRezultat3.AutoSize = true;
            labelRezultat3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRezultat3.ForeColor = Color.LightGray;
            labelRezultat3.Location = new Point(16, 136);
            labelRezultat3.Name = "labelRezultat3";
            labelRezultat3.Size = new Size(100, 25);
            labelRezultat3.TabIndex = 4;
            labelRezultat3.Text = "3. Pilot";

            // btnVeziRezultateComplet
            btnVeziRezultateComplet.BackColor = Color.FromArgb(32, 32, 32);
            btnVeziRezultateComplet.FlatAppearance.BorderSize = 0;
            btnVeziRezultateComplet.FlatStyle = FlatStyle.Flat;
            btnVeziRezultateComplet.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnVeziRezultateComplet.ForeColor = Color.WhiteSmoke;
            btnVeziRezultateComplet.Location = new Point(240, 158);
            btnVeziRezultateComplet.Name = "btnVeziRezultateComplet";
            btnVeziRezultateComplet.Size = new Size(180, 36);
            btnVeziRezultateComplet.TabIndex = 5;
            btnVeziRezultateComplet.Text = "📊 Vezi Rezultate Complet";
            btnVeziRezultateComplet.UseVisualStyleBackColor = false;
            btnVeziRezultateComplet.Click += new EventHandler(btnVeziRezultateComplet_Click);
            btnVeziRezultateComplet.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnVeziRezultateComplet.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            // Bottom buttons
            btnActualizeazaClasament.BackColor = Color.FromArgb(230, 28, 43);
            btnActualizeazaClasament.FlatAppearance.BorderSize = 0;
            btnActualizeazaClasament.FlatStyle = FlatStyle.Flat;
            btnActualizeazaClasament.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnActualizeazaClasament.ForeColor = Color.White;
            btnActualizeazaClasament.Location = new Point(20, 634);
            btnActualizeazaClasament.Name = "btnActualizeazaClasament";
            btnActualizeazaClasament.Size = new Size(280, 44);
            btnActualizeazaClasament.TabIndex = 6;
            btnActualizeazaClasament.Text = "🔄 Actualizează Clasament";
            btnActualizeazaClasament.UseVisualStyleBackColor = false;
            btnActualizeazaClasament.Click += new EventHandler(btnActualizeazaClasament_Click);
            btnActualizeazaClasament.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnActualizeazaClasament.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            btnBackup.BackColor = Color.FromArgb(32, 32, 32);
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.FlatStyle = FlatStyle.Flat;
            btnBackup.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackup.ForeColor = Color.WhiteSmoke;
            btnBackup.Location = new Point(320, 634);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(200, 44);
            btnBackup.TabIndex = 7;
            btnBackup.Text = "💾 Backup Date";
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += new EventHandler(btnBackup_Click);
            btnBackup.MouseEnter += new EventHandler(InteractiveButton_MouseEnter);
            btnBackup.MouseLeave += new EventHandler(InteractiveButton_MouseLeave);

            // DashboardAdminForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(1200, 800);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimumSize = new Size(1200, 800);
            Name = "DashboardAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "F1 Manager - Dashboard Admin";
            Load += new EventHandler(DashboardAdminForm_Load);
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelTopPiloti.ResumeLayout(false);
            panelTopPiloti.PerformLayout();
            panelTop3.ResumeLayout(false);
            panelTop3_1.ResumeLayout(false);
            panelTop3_1.PerformLayout();
            panelTop3_2.ResumeLayout(false);
            panelTop3_2.PerformLayout();
            panelTop3_3.ResumeLayout(false);
            panelTop3_3.PerformLayout();
            panelStatsTop.ResumeLayout(false);
            panelStatsTop.PerformLayout();
            panelCalendarPreview.ResumeLayout(false);
            panelCalendarPreview.PerformLayout();
            panelResultsPreview.ResumeLayout(false);
            panelResultsPreview.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelSidebar;
        private Label labelBrand;
        private Button buttonDashboard;
        private Button buttonPiloti;
        private Button buttonEchipe;
        private Button buttonCurse;
        private Button buttonClasament;
        private Button buttonClasamentEchipe;
        private Button buttonLogout;
        private Panel panelHeader;
        private Label labelHeader;
        private Label labelSubHeader;
        private Panel panelContent;
        
        private Panel panelStatsTop;
        private Label labelStatsTitle;
        private Label labelPilotiInregistrati;
        private Label labelTotalPuncte;
        private Label labelUrmatoareaCursa;

        private Panel panelTopPiloti;
        private Label labelTopPilotiTitle;
        private Panel panelTop3;
        private Panel panelTop3_1;
        private Label labelTop1Poz;
        private Label labelTop1Nume;
        private Label labelTop1Echipa;
        private Label labelTop1Puncte;
        private Panel panelTop3_2;
        private Label labelTop2Poz;
        private Label labelTop2Nume;
        private Label labelTop2Echipa;
        private Label labelTop2Puncte;
        private Panel panelTop3_3;
        private Label labelTop3Poz;
        private Label labelTop3Nume;
        private Label labelTop3Echipa;
        private Label labelTop3Puncte;

        private Button btnAdaugaPilot;
        private Button btnVeziClasamentComplet;

        private Panel panelCalendarPreview;
        private Label labelCalendarTitle;
        private Label labelCursa1;
        private Label labelCursa2;
        private Label labelCursa3;
        private Button btnVeziCalendarComplet;

        private Panel panelResultsPreview;
        private Label labelResultsTitle;
        private Label labelRezultatCursa;
        private Label labelRezultat1;
        private Label labelRezultat2;
        private Label labelRezultat3;
        private Button btnVeziRezultateComplet;

        private Button btnActualizeazaClasament;
        private Button btnBackup;
    }
}