namespace GestionMaterielIntExt
{
    partial class ViewGestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewGestion));
            tlpFormGestion = new TableLayoutPanel();
            tbcGestion = new TabControl();
            tabMateriel = new TabPage();
            tlpGMateriel = new TableLayoutPanel();
            dgvTabMateriel = new DataGridView();
            lbxContextCategoriesTM = new ListBox();
            tabCMaintenance = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            dgvTabCMaintenance = new DataGridView();
            tabEntreprise = new TabPage();
            tableLayoutPanel6 = new TableLayoutPanel();
            dgvTabEntreprise = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            lbGestionMatériels = new Label();
            lbSI = new Label();
            flpFiltreCategorie = new FlowLayoutPanel();
            btRecherche = new Button();
            cbxCatRecherche = new ComboBox();
            flpButton = new FlowLayoutPanel();
            btAjouter = new Button();
            btModifier = new Button();
            btRafraichir = new Button();
            btAnnuler = new Button();
            btArchiver = new Button();
            btDetruire = new Button();
            btClose = new Button();
            tlpContextTabMateriel = new TableLayoutPanel();
            tbxContextNomMatTB = new TextBox();
            lbMaitemance = new Label();
            lbRefMat = new Label();
            lbContxDFGarantie = new Label();
            lbUser = new Label();
            blContxDtMService = new Label();
            lbContxNom = new Label();
            dtpContextDFGarantieTM = new DateTimePicker();
            dtpContextDMServiceTM = new DateTimePicker();
            cbxContextUserTM = new ComboBox();
            tbxContextIdMatTM = new TextBox();
            cbxContextIdMContratTM = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbError = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            bdsTabMateriel = new BindingSource(components);
            bdsTabCMaintenance = new BindingSource(components);
            bdsTabEntreprise = new BindingSource(components);
            toolTipGestion = new ToolTip(components);
            tlpFormGestion.SuspendLayout();
            tbcGestion.SuspendLayout();
            tabMateriel.SuspendLayout();
            tlpGMateriel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTabMateriel).BeginInit();
            tabCMaintenance.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTabCMaintenance).BeginInit();
            tabEntreprise.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTabEntreprise).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            flpFiltreCategorie.SuspendLayout();
            flpButton.SuspendLayout();
            tlpContextTabMateriel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bdsTabMateriel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bdsTabCMaintenance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bdsTabEntreprise).BeginInit();
            SuspendLayout();
            // 
            // tlpFormGestion
            // 
            tlpFormGestion.AutoSize = true;
            tlpFormGestion.ColumnCount = 1;
            tlpFormGestion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormGestion.Controls.Add(tbcGestion, 0, 0);
            tlpFormGestion.Controls.Add(tableLayoutPanel2, 0, 0);
            tlpFormGestion.Controls.Add(flpButton, 0, 3);
            tlpFormGestion.Controls.Add(tlpContextTabMateriel, 0, 2);
            tlpFormGestion.Controls.Add(tableLayoutPanel1, 0, 4);
            tlpFormGestion.Dock = DockStyle.Fill;
            tlpFormGestion.Location = new Point(0, 0);
            tlpFormGestion.Margin = new Padding(2, 3, 2, 3);
            tlpFormGestion.Name = "tlpFormGestion";
            tlpFormGestion.RowCount = 5;
            tlpFormGestion.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tlpFormGestion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFormGestion.RowStyles.Add(new RowStyle());
            tlpFormGestion.RowStyles.Add(new RowStyle());
            tlpFormGestion.RowStyles.Add(new RowStyle());
            tlpFormGestion.Size = new Size(1845, 775);
            tlpFormGestion.TabIndex = 0;
            // 
            // tbcGestion
            // 
            tbcGestion.Appearance = TabAppearance.Buttons;
            tbcGestion.Controls.Add(tabMateriel);
            tbcGestion.Controls.Add(tabCMaintenance);
            tbcGestion.Controls.Add(tabEntreprise);
            tbcGestion.Cursor = Cursors.Hand;
            tbcGestion.Dock = DockStyle.Fill;
            tbcGestion.Font = new Font("Tahoma", 9.857143F);
            tbcGestion.HotTrack = true;
            tbcGestion.Location = new Point(0, 115);
            tbcGestion.Margin = new Padding(0, 15, 0, 0);
            tbcGestion.Name = "tbcGestion";
            tbcGestion.Padding = new Point(23, 6);
            tbcGestion.SelectedIndex = 0;
            tbcGestion.Size = new Size(1845, 482);
            tbcGestion.TabIndex = 1;
            tbcGestion.SelectedIndexChanged += tbcGestion_SelectedIndexChanged;
            // 
            // tabMateriel
            // 
            tabMateriel.BackColor = SystemColors.ActiveBorder;
            tabMateriel.Controls.Add(tlpGMateriel);
            tabMateriel.Font = new Font("Tahoma", 9.857143F, FontStyle.Bold);
            tabMateriel.Location = new Point(4, 34);
            tabMateriel.Margin = new Padding(0);
            tabMateriel.Name = "tabMateriel";
            tabMateriel.Size = new Size(1837, 444);
            tabMateriel.TabIndex = 0;
            tabMateriel.Text = "Matériel";
            // 
            // tlpGMateriel
            // 
            tlpGMateriel.AutoSize = true;
            tlpGMateriel.ColumnCount = 2;
            tlpGMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlpGMateriel.Controls.Add(dgvTabMateriel, 0, 0);
            tlpGMateriel.Controls.Add(lbxContextCategoriesTM, 1, 0);
            tlpGMateriel.Dock = DockStyle.Fill;
            tlpGMateriel.Location = new Point(0, 0);
            tlpGMateriel.Margin = new Padding(2, 3, 2, 3);
            tlpGMateriel.Name = "tlpGMateriel";
            tlpGMateriel.RowCount = 1;
            tlpGMateriel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGMateriel.RowStyles.Add(new RowStyle(SizeType.Absolute, 399F));
            tlpGMateriel.Size = new Size(1837, 444);
            tlpGMateriel.TabIndex = 0;
            // 
            // dgvTabMateriel
            // 
            dgvTabMateriel.AllowUserToAddRows = false;
            dgvTabMateriel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabMateriel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTabMateriel.Dock = DockStyle.Fill;
            dgvTabMateriel.Location = new Point(2, 3);
            dgvTabMateriel.Margin = new Padding(2, 3, 2, 3);
            dgvTabMateriel.MultiSelect = false;
            dgvTabMateriel.Name = "dgvTabMateriel";
            dgvTabMateriel.ReadOnly = true;
            dgvTabMateriel.RowHeadersVisible = false;
            dgvTabMateriel.RowHeadersWidth = 72;
            dgvTabMateriel.RowTemplate.Height = 37;
            dgvTabMateriel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabMateriel.Size = new Size(1633, 438);
            dgvTabMateriel.TabIndex = 5;
            dgvTabMateriel.SelectionChanged += dgvTabMateriel_SelectionChanged;
            // 
            // lbxContextCategoriesTM
            // 
            lbxContextCategoriesTM.Dock = DockStyle.Fill;
            lbxContextCategoriesTM.Font = new Font("Tahoma", 9.857143F);
            lbxContextCategoriesTM.FormattingEnabled = true;
            lbxContextCategoriesTM.ItemHeight = 16;
            lbxContextCategoriesTM.Location = new Point(1639, 3);
            lbxContextCategoriesTM.Margin = new Padding(2, 3, 2, 3);
            lbxContextCategoriesTM.Name = "lbxContextCategoriesTM";
            lbxContextCategoriesTM.SelectionMode = SelectionMode.MultiExtended;
            lbxContextCategoriesTM.Size = new Size(196, 438);
            lbxContextCategoriesTM.TabIndex = 7;
            // 
            // tabCMaintenance
            // 
            tabCMaintenance.BackColor = SystemColors.ActiveBorder;
            tabCMaintenance.Controls.Add(tableLayoutPanel3);
            tabCMaintenance.Location = new Point(4, 34);
            tabCMaintenance.Margin = new Padding(0);
            tabCMaintenance.Name = "tabCMaintenance";
            tabCMaintenance.Size = new Size(1837, 444);
            tabCMaintenance.TabIndex = 1;
            tabCMaintenance.Text = "Contrat de maintenance";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(dgvTabCMaintenance, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(1837, 444);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // dgvTabCMaintenance
            // 
            dgvTabCMaintenance.AllowUserToAddRows = false;
            dgvTabCMaintenance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabCMaintenance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTabCMaintenance.Dock = DockStyle.Fill;
            dgvTabCMaintenance.Location = new Point(2, 3);
            dgvTabCMaintenance.Margin = new Padding(2, 3, 2, 3);
            dgvTabCMaintenance.MultiSelect = false;
            dgvTabCMaintenance.Name = "dgvTabCMaintenance";
            dgvTabCMaintenance.ReadOnly = true;
            dgvTabCMaintenance.RowHeadersVisible = false;
            dgvTabCMaintenance.RowHeadersWidth = 72;
            dgvTabCMaintenance.RowTemplate.Height = 37;
            dgvTabCMaintenance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabCMaintenance.Size = new Size(1833, 438);
            dgvTabCMaintenance.TabIndex = 5;
            // 
            // tabEntreprise
            // 
            tabEntreprise.BackColor = SystemColors.ActiveBorder;
            tabEntreprise.Controls.Add(tableLayoutPanel6);
            tabEntreprise.Location = new Point(4, 34);
            tabEntreprise.Margin = new Padding(0);
            tabEntreprise.Name = "tabEntreprise";
            tabEntreprise.Size = new Size(1837, 444);
            tabEntreprise.TabIndex = 2;
            tabEntreprise.Text = "Entreprise de maintenance";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(dgvTabEntreprise, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(1837, 444);
            tableLayoutPanel6.TabIndex = 1;
            // 
            // dgvTabEntreprise
            // 
            dgvTabEntreprise.AllowUserToAddRows = false;
            dgvTabEntreprise.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabEntreprise.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTabEntreprise.Dock = DockStyle.Fill;
            dgvTabEntreprise.Location = new Point(2, 3);
            dgvTabEntreprise.Margin = new Padding(2, 3, 2, 3);
            dgvTabEntreprise.MultiSelect = false;
            dgvTabEntreprise.Name = "dgvTabEntreprise";
            dgvTabEntreprise.ReadOnly = true;
            dgvTabEntreprise.RowHeadersVisible = false;
            dgvTabEntreprise.RowHeadersWidth = 72;
            dgvTabEntreprise.RowTemplate.Height = 37;
            dgvTabEntreprise.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabEntreprise.Size = new Size(1833, 438);
            dgvTabEntreprise.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13F));
            tableLayoutPanel2.Controls.Add(lbGestionMatériels, 1, 0);
            tableLayoutPanel2.Controls.Add(lbSI, 0, 0);
            tableLayoutPanel2.Controls.Add(flpFiltreCategorie, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(2, 3);
            tableLayoutPanel2.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1841, 94);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // lbGestionMatériels
            // 
            lbGestionMatériels.AutoSize = true;
            lbGestionMatériels.Dock = DockStyle.Fill;
            lbGestionMatériels.Font = new Font("Tahoma", 11.25F);
            lbGestionMatériels.Location = new Point(462, 15);
            lbGestionMatériels.Margin = new Padding(2, 15, 2, 0);
            lbGestionMatériels.Name = "lbGestionMatériels";
            lbGestionMatériels.Size = new Size(686, 79);
            lbGestionMatériels.TabIndex = 3;
            lbGestionMatériels.Text = "Gestion matériel";
            lbGestionMatériels.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbSI
            // 
            lbSI.Anchor = AnchorStyles.Right;
            lbSI.AutoSize = true;
            lbSI.Font = new Font("Tahoma", 12F);
            lbSI.Location = new Point(263, 45);
            lbSI.Margin = new Padding(2, 15, 2, 0);
            lbSI.Name = "lbSI";
            lbSI.Size = new Size(195, 19);
            lbSI.TabIndex = 2;
            lbSI.Text = "SERVICE INFORMATIQUE";
            // 
            // flpFiltreCategorie
            // 
            flpFiltreCategorie.Anchor = AnchorStyles.None;
            flpFiltreCategorie.AutoSize = true;
            flpFiltreCategorie.Controls.Add(btRecherche);
            flpFiltreCategorie.Controls.Add(cbxCatRecherche);
            flpFiltreCategorie.Location = new Point(1320, 3);
            flpFiltreCategorie.Margin = new Padding(2, 3, 2, 3);
            flpFiltreCategorie.Name = "flpFiltreCategorie";
            flpFiltreCategorie.Padding = new Padding(0, 13, 0, 0);
            flpFiltreCategorie.RightToLeft = RightToLeft.Yes;
            flpFiltreCategorie.Size = new Size(350, 88);
            flpFiltreCategorie.TabIndex = 4;
            // 
            // btRecherche
            // 
            btRecherche.Anchor = AnchorStyles.Left;
            btRecherche.Cursor = Cursors.Hand;
            btRecherche.Font = new Font("Tahoma", 9.857143F);
            btRecherche.Location = new Point(162, 28);
            btRecherche.Margin = new Padding(15);
            btRecherche.Name = "btRecherche";
            btRecherche.Size = new Size(173, 47);
            btRecherche.TabIndex = 8;
            btRecherche.Text = "&Filtrer";
            btRecherche.UseVisualStyleBackColor = true;
            btRecherche.Click += btRecherche_Click;
            // 
            // cbxCatRecherche
            // 
            cbxCatRecherche.Anchor = AnchorStyles.None;
            cbxCatRecherche.FormattingEnabled = true;
            cbxCatRecherche.Location = new Point(2, 37);
            cbxCatRecherche.Margin = new Padding(2, 3, 2, 3);
            cbxCatRecherche.Name = "cbxCatRecherche";
            cbxCatRecherche.RightToLeft = RightToLeft.No;
            cbxCatRecherche.Size = new Size(143, 28);
            cbxCatRecherche.TabIndex = 1;
            cbxCatRecherche.Text = "Catégorie";
            // 
            // flpButton
            // 
            flpButton.Anchor = AnchorStyles.None;
            flpButton.AutoSize = true;
            flpButton.Controls.Add(btAjouter);
            flpButton.Controls.Add(btModifier);
            flpButton.Controls.Add(btRafraichir);
            flpButton.Controls.Add(btAnnuler);
            flpButton.Controls.Add(btArchiver);
            flpButton.Controls.Add(btDetruire);
            flpButton.Controls.Add(btClose);
            flpButton.Location = new Point(212, 669);
            flpButton.Margin = new Padding(2, 3, 2, 3);
            flpButton.Name = "flpButton";
            flpButton.Size = new Size(1421, 77);
            flpButton.TabIndex = 8;
            // 
            // btAjouter
            // 
            btAjouter.Anchor = AnchorStyles.Left;
            btAjouter.Cursor = Cursors.Hand;
            btAjouter.FlatStyle = FlatStyle.Flat;
            btAjouter.Font = new Font("Tahoma", 9.857143F);
            btAjouter.Location = new Point(15, 15);
            btAjouter.Margin = new Padding(15);
            btAjouter.Name = "btAjouter";
            btAjouter.Size = new Size(173, 47);
            btAjouter.TabIndex = 2;
            btAjouter.Text = "&Nouveau";
            btAjouter.UseVisualStyleBackColor = true;
            btAjouter.Click += btAjouter_Click;
            // 
            // btModifier
            // 
            btModifier.Anchor = AnchorStyles.Left;
            btModifier.Cursor = Cursors.Hand;
            btModifier.FlatStyle = FlatStyle.Flat;
            btModifier.Font = new Font("Tahoma", 9.857143F);
            btModifier.Location = new Point(218, 15);
            btModifier.Margin = new Padding(15);
            btModifier.Name = "btModifier";
            btModifier.Size = new Size(173, 47);
            btModifier.TabIndex = 3;
            btModifier.Text = "&Modifier";
            btModifier.UseVisualStyleBackColor = true;
            btModifier.Click += btModifier_Click;
            // 
            // btRafraichir
            // 
            btRafraichir.Anchor = AnchorStyles.Left;
            btRafraichir.Cursor = Cursors.Hand;
            btRafraichir.FlatStyle = FlatStyle.Flat;
            btRafraichir.Font = new Font("Tahoma", 9.857143F);
            btRafraichir.ImageAlign = ContentAlignment.MiddleRight;
            btRafraichir.Location = new Point(421, 15);
            btRafraichir.Margin = new Padding(15);
            btRafraichir.Name = "btRafraichir";
            btRafraichir.Size = new Size(173, 47);
            btRafraichir.TabIndex = 4;
            btRafraichir.Text = "&Rafraichir";
            btRafraichir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btRafraichir.UseVisualStyleBackColor = true;
            btRafraichir.Click += btRafraichir_Click;
            // 
            // btAnnuler
            // 
            btAnnuler.Anchor = AnchorStyles.Left;
            btAnnuler.Cursor = Cursors.Hand;
            btAnnuler.FlatStyle = FlatStyle.Flat;
            btAnnuler.Font = new Font("Tahoma", 9.857143F);
            btAnnuler.ImageAlign = ContentAlignment.MiddleRight;
            btAnnuler.Location = new Point(624, 15);
            btAnnuler.Margin = new Padding(15);
            btAnnuler.Name = "btAnnuler";
            btAnnuler.Size = new Size(173, 47);
            btAnnuler.TabIndex = 7;
            btAnnuler.Text = "Annuler";
            btAnnuler.TextImageRelation = TextImageRelation.ImageBeforeText;
            btAnnuler.UseVisualStyleBackColor = true;
            btAnnuler.Click += btAnnuler_Click;
            // 
            // btArchiver
            // 
            btArchiver.Anchor = AnchorStyles.Left;
            btArchiver.Cursor = Cursors.Hand;
            btArchiver.FlatStyle = FlatStyle.Flat;
            btArchiver.Font = new Font("Tahoma", 9.857143F);
            btArchiver.Location = new Point(827, 15);
            btArchiver.Margin = new Padding(15);
            btArchiver.Name = "btArchiver";
            btArchiver.Size = new Size(173, 47);
            btArchiver.TabIndex = 5;
            btArchiver.Text = "&Archiver";
            btArchiver.UseVisualStyleBackColor = true;
            btArchiver.Click += btArchiver_Click;
            // 
            // btDetruire
            // 
            btDetruire.Anchor = AnchorStyles.Left;
            btDetruire.Cursor = Cursors.Hand;
            btDetruire.FlatStyle = FlatStyle.Flat;
            btDetruire.Font = new Font("Tahoma", 9.857143F);
            btDetruire.Location = new Point(1030, 15);
            btDetruire.Margin = new Padding(15);
            btDetruire.Name = "btDetruire";
            btDetruire.Size = new Size(173, 47);
            btDetruire.TabIndex = 6;
            btDetruire.Text = "&Détruire";
            btDetruire.UseVisualStyleBackColor = true;
            btDetruire.Click += btDetruire_Click;
            // 
            // btClose
            // 
            btClose.Anchor = AnchorStyles.Left;
            btClose.Cursor = Cursors.Hand;
            btClose.Font = new Font("Tahoma", 9.857143F);
            btClose.ImageAlign = ContentAlignment.MiddleRight;
            btClose.Location = new Point(1233, 15);
            btClose.Margin = new Padding(15);
            btClose.Name = "btClose";
            btClose.Size = new Size(173, 47);
            btClose.TabIndex = 8;
            btClose.Text = "Fermer";
            btClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btClose.UseVisualStyleBackColor = true;
            btClose.Click += btClose_Click;
            // 
            // tlpContextTabMateriel
            // 
            tlpContextTabMateriel.ColumnCount = 6;
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tlpContextTabMateriel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tlpContextTabMateriel.Controls.Add(tbxContextNomMatTB, 0, 1);
            tlpContextTabMateriel.Controls.Add(lbMaitemance, 5, 0);
            tlpContextTabMateriel.Controls.Add(lbRefMat, 4, 0);
            tlpContextTabMateriel.Controls.Add(lbContxDFGarantie, 2, 0);
            tlpContextTabMateriel.Controls.Add(lbUser, 3, 0);
            tlpContextTabMateriel.Controls.Add(blContxDtMService, 1, 0);
            tlpContextTabMateriel.Controls.Add(lbContxNom, 0, 0);
            tlpContextTabMateriel.Controls.Add(dtpContextDFGarantieTM, 2, 1);
            tlpContextTabMateriel.Controls.Add(dtpContextDMServiceTM, 1, 1);
            tlpContextTabMateriel.Controls.Add(cbxContextUserTM, 3, 1);
            tlpContextTabMateriel.Controls.Add(tbxContextIdMatTM, 4, 1);
            tlpContextTabMateriel.Controls.Add(cbxContextIdMContratTM, 5, 1);
            tlpContextTabMateriel.Dock = DockStyle.Fill;
            tlpContextTabMateriel.Location = new Point(2, 600);
            tlpContextTabMateriel.Margin = new Padding(2, 3, 2, 3);
            tlpContextTabMateriel.Name = "tlpContextTabMateriel";
            tlpContextTabMateriel.RowCount = 2;
            tlpContextTabMateriel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpContextTabMateriel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpContextTabMateriel.Size = new Size(1841, 63);
            tlpContextTabMateriel.TabIndex = 9;
            // 
            // tbxContextNomMatTB
            // 
            tbxContextNomMatTB.Dock = DockStyle.Fill;
            tbxContextNomMatTB.Enabled = false;
            tbxContextNomMatTB.Font = new Font("Tahoma", 9.857143F);
            tbxContextNomMatTB.Location = new Point(8, 34);
            tbxContextNomMatTB.Margin = new Padding(8, 3, 8, 3);
            tbxContextNomMatTB.Name = "tbxContextNomMatTB";
            tbxContextNomMatTB.Size = new Size(290, 23);
            tbxContextNomMatTB.TabIndex = 2;
            tbxContextNomMatTB.Text = "nom matériel";
            tbxContextNomMatTB.TextAlign = HorizontalAlignment.Center;
            tbxContextNomMatTB.TextChanged += tbxContextNomMatTB_TextChanged;
            // 
            // lbMaitemance
            // 
            lbMaitemance.AutoSize = true;
            lbMaitemance.Dock = DockStyle.Fill;
            lbMaitemance.Font = new Font("Tahoma", 9.857143F);
            lbMaitemance.ForeColor = SystemColors.Control;
            lbMaitemance.Location = new Point(1532, 0);
            lbMaitemance.Margin = new Padding(2, 0, 2, 0);
            lbMaitemance.Name = "lbMaitemance";
            lbMaitemance.Size = new Size(307, 31);
            lbMaitemance.TabIndex = 6;
            lbMaitemance.Text = "Contrat";
            lbMaitemance.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbRefMat
            // 
            lbRefMat.AutoSize = true;
            lbRefMat.Dock = DockStyle.Fill;
            lbRefMat.Font = new Font("Tahoma", 9.857143F);
            lbRefMat.ForeColor = SystemColors.Control;
            lbRefMat.Location = new Point(1226, 0);
            lbRefMat.Margin = new Padding(2, 0, 2, 0);
            lbRefMat.Name = "lbRefMat";
            lbRefMat.Size = new Size(302, 31);
            lbRefMat.TabIndex = 5;
            lbRefMat.Text = "Ref marquage";
            lbRefMat.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbContxDFGarantie
            // 
            lbContxDFGarantie.AutoSize = true;
            lbContxDFGarantie.Dock = DockStyle.Fill;
            lbContxDFGarantie.Font = new Font("Tahoma", 9.857143F);
            lbContxDFGarantie.ForeColor = SystemColors.Control;
            lbContxDFGarantie.Location = new Point(614, 0);
            lbContxDFGarantie.Margin = new Padding(2, 0, 2, 0);
            lbContxDFGarantie.Name = "lbContxDFGarantie";
            lbContxDFGarantie.Size = new Size(302, 31);
            lbContxDFGarantie.TabIndex = 2;
            lbContxDFGarantie.Text = "Date fin garantie";
            lbContxDFGarantie.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Dock = DockStyle.Fill;
            lbUser.Font = new Font("Tahoma", 9.857143F);
            lbUser.ForeColor = SystemColors.Control;
            lbUser.Location = new Point(920, 0);
            lbUser.Margin = new Padding(2, 0, 2, 0);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(302, 31);
            lbUser.TabIndex = 4;
            lbUser.Text = "Propriétaire";
            lbUser.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // blContxDtMService
            // 
            blContxDtMService.AutoSize = true;
            blContxDtMService.Dock = DockStyle.Fill;
            blContxDtMService.Font = new Font("Tahoma", 9.857143F);
            blContxDtMService.ForeColor = SystemColors.Control;
            blContxDtMService.Location = new Point(308, 0);
            blContxDtMService.Margin = new Padding(2, 0, 2, 0);
            blContxDtMService.Name = "blContxDtMService";
            blContxDtMService.Size = new Size(302, 31);
            blContxDtMService.TabIndex = 1;
            blContxDtMService.Text = "Date mise en service";
            blContxDtMService.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbContxNom
            // 
            lbContxNom.AutoSize = true;
            lbContxNom.Dock = DockStyle.Fill;
            lbContxNom.Font = new Font("Tahoma", 9.857143F);
            lbContxNom.ForeColor = SystemColors.Control;
            lbContxNom.Location = new Point(2, 0);
            lbContxNom.Margin = new Padding(2, 0, 2, 0);
            lbContxNom.Name = "lbContxNom";
            lbContxNom.Size = new Size(302, 31);
            lbContxNom.TabIndex = 0;
            lbContxNom.Text = "Nom";
            lbContxNom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpContextDFGarantieTM
            // 
            dtpContextDFGarantieTM.Dock = DockStyle.Fill;
            dtpContextDFGarantieTM.Enabled = false;
            dtpContextDFGarantieTM.Font = new Font("Tahoma", 9.857143F);
            dtpContextDFGarantieTM.Location = new Point(620, 34);
            dtpContextDFGarantieTM.Margin = new Padding(8, 3, 8, 3);
            dtpContextDFGarantieTM.Name = "dtpContextDFGarantieTM";
            dtpContextDFGarantieTM.ShowCheckBox = true;
            dtpContextDFGarantieTM.Size = new Size(290, 23);
            dtpContextDFGarantieTM.TabIndex = 4;
            // 
            // dtpContextDMServiceTM
            // 
            dtpContextDMServiceTM.Dock = DockStyle.Fill;
            dtpContextDMServiceTM.Enabled = false;
            dtpContextDMServiceTM.Font = new Font("Tahoma", 9.857143F);
            dtpContextDMServiceTM.Location = new Point(314, 34);
            dtpContextDMServiceTM.Margin = new Padding(8, 3, 8, 3);
            dtpContextDMServiceTM.Name = "dtpContextDMServiceTM";
            dtpContextDMServiceTM.ShowCheckBox = true;
            dtpContextDMServiceTM.Size = new Size(290, 23);
            dtpContextDMServiceTM.TabIndex = 3;
            // 
            // cbxContextUserTM
            // 
            cbxContextUserTM.Dock = DockStyle.Fill;
            cbxContextUserTM.Enabled = false;
            cbxContextUserTM.FlatStyle = FlatStyle.Popup;
            cbxContextUserTM.FormattingEnabled = true;
            cbxContextUserTM.Location = new Point(926, 34);
            cbxContextUserTM.Margin = new Padding(8, 3, 8, 3);
            cbxContextUserTM.Name = "cbxContextUserTM";
            cbxContextUserTM.Size = new Size(290, 28);
            cbxContextUserTM.TabIndex = 13;
            cbxContextUserTM.Text = "nom du propriétaire";
            // 
            // tbxContextIdMatTM
            // 
            tbxContextIdMatTM.Dock = DockStyle.Fill;
            tbxContextIdMatTM.Enabled = false;
            tbxContextIdMatTM.Font = new Font("Tahoma", 9.857143F);
            tbxContextIdMatTM.Location = new Point(1232, 34);
            tbxContextIdMatTM.Margin = new Padding(8, 3, 8, 3);
            tbxContextIdMatTM.Name = "tbxContextIdMatTM";
            tbxContextIdMatTM.Size = new Size(290, 23);
            tbxContextIdMatTM.TabIndex = 7;
            tbxContextIdMatTM.Text = "référence de marquage";
            tbxContextIdMatTM.TextAlign = HorizontalAlignment.Center;
            // 
            // cbxContextIdMContratTM
            // 
            cbxContextIdMContratTM.Dock = DockStyle.Fill;
            cbxContextIdMContratTM.Enabled = false;
            cbxContextIdMContratTM.FlatStyle = FlatStyle.Popup;
            cbxContextIdMContratTM.FormattingEnabled = true;
            cbxContextIdMContratTM.Location = new Point(1538, 34);
            cbxContextIdMContratTM.Margin = new Padding(8, 3, 8, 3);
            cbxContextIdMContratTM.Name = "cbxContextIdMContratTM";
            cbxContextIdMContratTM.Size = new Size(295, 28);
            cbxContextIdMContratTM.TabIndex = 15;
            cbxContextIdMContratTM.Text = "contrat de maintenance";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13F));
            tableLayoutPanel1.Controls.Add(lbError, 0, 0);
            tableLayoutPanel1.Location = new Point(893, 752);
            tableLayoutPanel1.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel1.Size = new Size(59, 20);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // lbError
            // 
            lbError.Anchor = AnchorStyles.None;
            lbError.AutoSize = true;
            lbError.Location = new Point(2, 0);
            lbError.Margin = new Padding(2, 0, 2, 0);
            lbError.Name = "lbError";
            lbError.Size = new Size(55, 20);
            lbError.TabIndex = 1;
            lbError.Text = "ERROR";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.ColumnCount = 7;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.Controls.Add(label1, 0, 0);
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(200, 100);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Tahoma", 9.857143F);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(37, 100);
            label1.TabIndex = 0;
            label1.Text = "Nom";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Tahoma", 9.857143F);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(71, 0);
            label2.Name = "label2";
            label2.Size = new Size(236, 100);
            label2.TabIndex = 1;
            label2.Text = "Date mise en service";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewGestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1845, 775);
            Controls.Add(tlpFormGestion);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            Name = "ViewGestion";
            Text = "Gestions";
            FormClosing += ViewGestion_FormClosing;
            Load += Gestions_Load;
            tlpFormGestion.ResumeLayout(false);
            tlpFormGestion.PerformLayout();
            tbcGestion.ResumeLayout(false);
            tabMateriel.ResumeLayout(false);
            tabMateriel.PerformLayout();
            tlpGMateriel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTabMateriel).EndInit();
            tabCMaintenance.ResumeLayout(false);
            tabCMaintenance.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTabCMaintenance).EndInit();
            tabEntreprise.ResumeLayout(false);
            tabEntreprise.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTabEntreprise).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flpFiltreCategorie.ResumeLayout(false);
            flpButton.ResumeLayout(false);
            tlpContextTabMateriel.ResumeLayout(false);
            tlpContextTabMateriel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bdsTabMateriel).EndInit();
            ((System.ComponentModel.ISupportInitialize)bdsTabCMaintenance).EndInit();
            ((System.ComponentModel.ISupportInitialize)bdsTabEntreprise).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpFormGestion;
        private Button btAjouter;
        private Button btModifier;
        private Button btRafraichir;
        private Button btArchiver;
        private Button btDetruire;
        private TabControl tbcGestion;
        private TabPage tabMateriel;
        private TabPage tabCMaintenance;
        private TabPage tabEntreprise;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lbGestionMatériels;
        private Label lbSI;
        private TableLayoutPanel tlpGMateriel;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TableLayoutPanel tableLayoutPanel6;
        private DataGridView dgvTabMateriel;
        private DataGridView dgvTabCMaintenance;
        private DataGridView dgvTabEntreprise;
        private FlowLayoutPanel flpFiltreCategorie;
        private ComboBox cbxCatRecherche;
        private Button btRecherche;
        private BindingSource bdsTabMateriel;
        private DateTimePicker dtpContextDFGarantieTM;
        private DateTimePicker dtpContextDMServiceTM;
        private Label lbContxNom;
        private Label blContxDtMService;
        private Label lbContxDFGarantie;
        private Label lbRefMat;
        private Label lbUser;
        private Label lbMaitemance;
        private TextBox tbxContextNomMatTB;
        private TextBox tbxContextIdMatTM;
        private ComboBox cbxContextUserTM;
        private ListBox lbxContextCategoriesTM;
        private FlowLayoutPanel flpButton;
        private TableLayoutPanel tlpContextTabMateriel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbError;
        private Button btAnnuler;
        private ComboBox cbxContextIdMContratTM;
        private BindingSource bdsTabCMaintenance;
        private BindingSource bdsTabEntreprise;
        private ToolTip toolTipGestion;
        private Button btClose;
    }
}