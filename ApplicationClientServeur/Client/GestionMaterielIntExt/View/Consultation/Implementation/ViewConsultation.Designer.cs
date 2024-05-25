namespace GestionMaterielIntExt.View.Consultation.Implementation;

partial class ViewConsultation
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewConsultation));
        tableLayoutPanel1 = new TableLayoutPanel();
        flowLayoutPanel3 = new FlowLayoutPanel();
        btRafraichir = new Button();
        btClose = new Button();
        tableLayoutPanel2 = new TableLayoutPanel();
        lbGestionMatériels = new Label();
        lbSI = new Label();
        flowLayoutPanel2 = new FlowLayoutPanel();
        btRecherche = new Button();
        cbxCatRecherche = new ComboBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        txbArchive = new TextBox();
        label1 = new Label();
        dtpDFGarantie = new DateTimePicker();
        dtpDMService = new DateTimePicker();
        lbContxNom = new Label();
        blContxDtMService = new Label();
        lbContxDFGarantie = new Label();
        lbref = new Label();
        lbUser = new Label();
        lbMaitemance = new Label();
        txbNom = new TextBox();
        txbUser = new TextBox();
        txbId = new TextBox();
        txbCMaitemance = new TextBox();
        tableLayoutPanel6 = new TableLayoutPanel();
        dgvCMateriel = new DataGridView();
        lbxCategoriesCM = new ListBox();
        tableLayoutPanel5 = new TableLayoutPanel();
        lbError = new Label();
        bdsCMateriel = new BindingSource(components);
        tableLayoutPanel7 = new TableLayoutPanel();
        lbxCategories = new ListBox();
        label11 = new Label();
        listBox1 = new ListBox();
        label2 = new Label();
        tableLayoutPanel4 = new TableLayoutPanel();
        toolTipConsultation = new ToolTip(components);
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel3.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        flowLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel6.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCMateriel).BeginInit();
        tableLayoutPanel5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)bdsCMateriel).BeginInit();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.AutoSize = true;
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 0, 3);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 1);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 0, 4);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(2);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 5;
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.Size = new Size(1284, 639);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // flowLayoutPanel3
        // 
        flowLayoutPanel3.Anchor = AnchorStyles.None;
        flowLayoutPanel3.AutoSize = true;
        flowLayoutPanel3.Controls.Add(btRafraichir);
        flowLayoutPanel3.Controls.Add(btClose);
        flowLayoutPanel3.Location = new Point(439, 536);
        flowLayoutPanel3.Margin = new Padding(2);
        flowLayoutPanel3.Name = "flowLayoutPanel3";
        flowLayoutPanel3.Size = new Size(406, 77);
        flowLayoutPanel3.TabIndex = 8;
        // 
        // btRafraichir
        // 
        btRafraichir.Anchor = AnchorStyles.Left;
        btRafraichir.Cursor = Cursors.Hand;
        btRafraichir.Font = new Font("Tahoma", 9.857143F);
        btRafraichir.ImageAlign = ContentAlignment.MiddleRight;
        btRafraichir.Location = new Point(15, 15);
        btRafraichir.Margin = new Padding(15);
        btRafraichir.Name = "btRafraichir";
        btRafraichir.Size = new Size(173, 47);
        btRafraichir.TabIndex = 1;
        btRafraichir.Text = "&Rafraichir";
        btRafraichir.TextImageRelation = TextImageRelation.ImageBeforeText;
        btRafraichir.UseVisualStyleBackColor = true;
        btRafraichir.Click += btRafraichir_Click;
        // 
        // btClose
        // 
        btClose.Anchor = AnchorStyles.Left;
        btClose.Cursor = Cursors.Hand;
        btClose.Font = new Font("Tahoma", 9.857143F);
        btClose.ImageAlign = ContentAlignment.MiddleRight;
        btClose.Location = new Point(218, 15);
        btClose.Margin = new Padding(15);
        btClose.Name = "btClose";
        btClose.Size = new Size(173, 47);
        btClose.TabIndex = 2;
        btClose.Text = "&Fermer";
        btClose.TextImageRelation = TextImageRelation.ImageBeforeText;
        btClose.UseVisualStyleBackColor = true;
        btClose.Click += btClose_Click;
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
        tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 2, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(2, 2);
        tableLayoutPanel2.Margin = new Padding(2);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Size = new Size(1280, 133);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // lbGestionMatériels
        // 
        lbGestionMatériels.Anchor = AnchorStyles.Left;
        lbGestionMatériels.AutoSize = true;
        lbGestionMatériels.Font = new Font("Tahoma", 11.25F);
        lbGestionMatériels.Location = new Point(322, 57);
        lbGestionMatériels.Margin = new Padding(2, 0, 2, 0);
        lbGestionMatériels.Name = "lbGestionMatériels";
        lbGestionMatériels.Size = new Size(142, 18);
        lbGestionMatériels.TabIndex = 3;
        lbGestionMatériels.Text = "Consultation matériel";
        // 
        // lbSI
        // 
        lbSI.Anchor = AnchorStyles.Right;
        lbSI.AutoSize = true;
        lbSI.Font = new Font("Tahoma", 12F);
        lbSI.Location = new Point(123, 57);
        lbSI.Margin = new Padding(2, 0, 2, 0);
        lbSI.Name = "lbSI";
        lbSI.Size = new Size(195, 19);
        lbSI.TabIndex = 1;
        lbSI.Text = "SERVICE INFORMATIQUE";
        // 
        // flowLayoutPanel2
        // 
        flowLayoutPanel2.Anchor = AnchorStyles.None;
        flowLayoutPanel2.AutoSize = true;
        flowLayoutPanel2.Controls.Add(btRecherche);
        flowLayoutPanel2.Controls.Add(cbxCatRecherche);
        flowLayoutPanel2.Location = new Point(865, 18);
        flowLayoutPanel2.Margin = new Padding(2);
        flowLayoutPanel2.Name = "flowLayoutPanel2";
        flowLayoutPanel2.Padding = new Padding(0, 20, 0, 0);
        flowLayoutPanel2.RightToLeft = RightToLeft.Yes;
        flowLayoutPanel2.Size = new Size(350, 97);
        flowLayoutPanel2.TabIndex = 5;
        // 
        // btRecherche
        // 
        btRecherche.Anchor = AnchorStyles.Left;
        btRecherche.Cursor = Cursors.Hand;
        btRecherche.Font = new Font("Tahoma", 9.857143F);
        btRecherche.Location = new Point(162, 35);
        btRecherche.Margin = new Padding(15);
        btRecherche.Name = "btRecherche";
        btRecherche.Size = new Size(173, 47);
        btRecherche.TabIndex = 2;
        btRecherche.Text = "&Filtrer";
        btRecherche.UseVisualStyleBackColor = true;
        btRecherche.Click += btRecherche_Click;
        // 
        // cbxCatRecherche
        // 
        cbxCatRecherche.Anchor = AnchorStyles.None;
        cbxCatRecherche.FormattingEnabled = true;
        cbxCatRecherche.Location = new Point(2, 44);
        cbxCatRecherche.Margin = new Padding(2);
        cbxCatRecherche.Name = "cbxCatRecherche";
        cbxCatRecherche.RightToLeft = RightToLeft.No;
        cbxCatRecherche.Size = new Size(143, 28);
        cbxCatRecherche.Sorted = true;
        cbxCatRecherche.TabIndex = 3;
        cbxCatRecherche.Text = "Catégories";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.AutoSize = true;
        tableLayoutPanel3.ColumnCount = 7;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13F));
        tableLayoutPanel3.Controls.Add(txbArchive, 6, 1);
        tableLayoutPanel3.Controls.Add(label1, 6, 0);
        tableLayoutPanel3.Controls.Add(dtpDFGarantie, 2, 1);
        tableLayoutPanel3.Controls.Add(dtpDMService, 1, 1);
        tableLayoutPanel3.Controls.Add(lbContxNom, 0, 0);
        tableLayoutPanel3.Controls.Add(blContxDtMService, 1, 0);
        tableLayoutPanel3.Controls.Add(lbContxDFGarantie, 2, 0);
        tableLayoutPanel3.Controls.Add(lbref, 4, 0);
        tableLayoutPanel3.Controls.Add(lbUser, 3, 0);
        tableLayoutPanel3.Controls.Add(lbMaitemance, 5, 0);
        tableLayoutPanel3.Controls.Add(txbNom, 0, 1);
        tableLayoutPanel3.Controls.Add(txbUser, 3, 1);
        tableLayoutPanel3.Controls.Add(txbId, 4, 1);
        tableLayoutPanel3.Controls.Add(txbCMaitemance, 5, 1);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
        tableLayoutPanel3.Location = new Point(2, 478);
        tableLayoutPanel3.Margin = new Padding(2);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(1280, 54);
        tableLayoutPanel3.TabIndex = 1;
        // 
        // txbArchive
        // 
        txbArchive.Anchor = AnchorStyles.None;
        txbArchive.Font = new Font("Tahoma", 9.857143F);
        txbArchive.Location = new Point(1108, 29);
        txbArchive.Margin = new Padding(2);
        txbArchive.Name = "txbArchive";
        txbArchive.ReadOnly = true;
        txbArchive.Size = new Size(156, 23);
        txbArchive.TabIndex = 10;
        txbArchive.Text = "X";
        txbArchive.TextAlign = HorizontalAlignment.Center;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = DockStyle.Fill;
        label1.Font = new Font("Tahoma", 9.857143F);
        label1.ForeColor = SystemColors.Control;
        label1.Location = new Point(1094, 0);
        label1.Margin = new Padding(2, 0, 2, 0);
        label1.Name = "label1";
        label1.Size = new Size(184, 27);
        label1.TabIndex = 10;
        label1.Text = "Archive";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dtpDFGarantie
        // 
        dtpDFGarantie.Anchor = AnchorStyles.None;
        dtpDFGarantie.Font = new Font("Tahoma", 9.857143F);
        dtpDFGarantie.Format = DateTimePickerFormat.Short;
        dtpDFGarantie.Location = new Point(377, 29);
        dtpDFGarantie.Margin = new Padding(2);
        dtpDFGarantie.Name = "dtpDFGarantie";
        dtpDFGarantie.Size = new Size(156, 23);
        dtpDFGarantie.TabIndex = 6;
        // 
        // dtpDMService
        // 
        dtpDMService.Anchor = AnchorStyles.None;
        dtpDMService.Font = new Font("Tahoma", 9.857143F);
        dtpDMService.Format = DateTimePickerFormat.Short;
        dtpDMService.Location = new Point(195, 29);
        dtpDMService.Margin = new Padding(2);
        dtpDMService.Name = "dtpDMService";
        dtpDMService.Size = new Size(156, 23);
        dtpDMService.TabIndex = 5;
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
        lbContxNom.Size = new Size(178, 27);
        lbContxNom.TabIndex = 0;
        lbContxNom.Text = "Nom";
        lbContxNom.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // blContxDtMService
        // 
        blContxDtMService.AutoSize = true;
        blContxDtMService.Dock = DockStyle.Fill;
        blContxDtMService.Font = new Font("Tahoma", 9.857143F);
        blContxDtMService.ForeColor = SystemColors.Control;
        blContxDtMService.Location = new Point(184, 0);
        blContxDtMService.Margin = new Padding(2, 0, 2, 0);
        blContxDtMService.Name = "blContxDtMService";
        blContxDtMService.Size = new Size(178, 27);
        blContxDtMService.TabIndex = 1;
        blContxDtMService.Text = "Date mise en service";
        blContxDtMService.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbContxDFGarantie
        // 
        lbContxDFGarantie.AutoSize = true;
        lbContxDFGarantie.Dock = DockStyle.Fill;
        lbContxDFGarantie.Font = new Font("Tahoma", 9.857143F);
        lbContxDFGarantie.ForeColor = SystemColors.Control;
        lbContxDFGarantie.Location = new Point(366, 0);
        lbContxDFGarantie.Margin = new Padding(2, 0, 2, 0);
        lbContxDFGarantie.Name = "lbContxDFGarantie";
        lbContxDFGarantie.Size = new Size(178, 27);
        lbContxDFGarantie.TabIndex = 2;
        lbContxDFGarantie.Text = "Date fin garantie";
        lbContxDFGarantie.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbref
        // 
        lbref.AutoSize = true;
        lbref.Dock = DockStyle.Fill;
        lbref.Font = new Font("Tahoma", 9.857143F);
        lbref.ForeColor = SystemColors.Control;
        lbref.Location = new Point(730, 0);
        lbref.Margin = new Padding(2, 0, 2, 0);
        lbref.Name = "lbref";
        lbref.Size = new Size(178, 27);
        lbref.TabIndex = 5;
        lbref.Text = "Ref marquage";
        lbref.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbUser
        // 
        lbUser.AutoSize = true;
        lbUser.Dock = DockStyle.Fill;
        lbUser.Font = new Font("Tahoma", 9.857143F);
        lbUser.ForeColor = SystemColors.Control;
        lbUser.Location = new Point(548, 0);
        lbUser.Margin = new Padding(2, 0, 2, 0);
        lbUser.Name = "lbUser";
        lbUser.Size = new Size(178, 27);
        lbUser.TabIndex = 4;
        lbUser.Text = "Propriétaire";
        lbUser.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbMaitemance
        // 
        lbMaitemance.AutoSize = true;
        lbMaitemance.Dock = DockStyle.Fill;
        lbMaitemance.Font = new Font("Tahoma", 9.857143F);
        lbMaitemance.ForeColor = SystemColors.Control;
        lbMaitemance.Location = new Point(912, 0);
        lbMaitemance.Margin = new Padding(2, 0, 2, 0);
        lbMaitemance.Name = "lbMaitemance";
        lbMaitemance.Size = new Size(178, 27);
        lbMaitemance.TabIndex = 6;
        lbMaitemance.Text = "Contrat";
        lbMaitemance.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txbNom
        // 
        txbNom.Anchor = AnchorStyles.None;
        txbNom.Font = new Font("Tahoma", 9.857143F);
        txbNom.Location = new Point(13, 29);
        txbNom.Margin = new Padding(2);
        txbNom.Name = "txbNom";
        txbNom.ReadOnly = true;
        txbNom.Size = new Size(156, 23);
        txbNom.TabIndex = 4;
        txbNom.Text = "AOC 27 LED - C27G2ZE";
        txbNom.TextAlign = HorizontalAlignment.Center;
        // 
        // txbUser
        // 
        txbUser.Anchor = AnchorStyles.None;
        txbUser.Font = new Font("Tahoma", 9.857143F);
        txbUser.Location = new Point(559, 29);
        txbUser.Margin = new Padding(2);
        txbUser.Name = "txbUser";
        txbUser.ReadOnly = true;
        txbUser.Size = new Size(156, 23);
        txbUser.TabIndex = 7;
        txbUser.Text = "J.Petit";
        txbUser.TextAlign = HorizontalAlignment.Center;
        // 
        // txbId
        // 
        txbId.Anchor = AnchorStyles.None;
        txbId.Font = new Font("Tahoma", 9.857143F);
        txbId.Location = new Point(741, 29);
        txbId.Margin = new Padding(2);
        txbId.Name = "txbId";
        txbId.ReadOnly = true;
        txbId.Size = new Size(156, 23);
        txbId.TabIndex = 8;
        txbId.Text = "1234";
        txbId.TextAlign = HorizontalAlignment.Center;
        // 
        // txbCMaitemance
        // 
        txbCMaitemance.Anchor = AnchorStyles.None;
        txbCMaitemance.Font = new Font("Tahoma", 9.857143F);
        txbCMaitemance.Location = new Point(923, 29);
        txbCMaitemance.Margin = new Padding(2);
        txbCMaitemance.Name = "txbCMaitemance";
        txbCMaitemance.ReadOnly = true;
        txbCMaitemance.Size = new Size(156, 23);
        txbCMaitemance.TabIndex = 9;
        txbCMaitemance.Text = "Maintenance";
        txbCMaitemance.TextAlign = HorizontalAlignment.Center;
        // 
        // tableLayoutPanel6
        // 
        tableLayoutPanel6.ColumnCount = 2;
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
        tableLayoutPanel6.Controls.Add(dgvCMateriel, 0, 0);
        tableLayoutPanel6.Controls.Add(lbxCategoriesCM, 1, 0);
        tableLayoutPanel6.Dock = DockStyle.Fill;
        tableLayoutPanel6.Location = new Point(2, 139);
        tableLayoutPanel6.Margin = new Padding(2);
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.RowCount = 1;
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel6.Size = new Size(1280, 335);
        tableLayoutPanel6.TabIndex = 9;
        // 
        // dgvCMateriel
        // 
        dgvCMateriel.AllowUserToAddRows = false;
        dgvCMateriel.AllowUserToDeleteRows = false;
        dgvCMateriel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCMateriel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCMateriel.Dock = DockStyle.Fill;
        dgvCMateriel.Location = new Point(2, 2);
        dgvCMateriel.Margin = new Padding(2);
        dgvCMateriel.Name = "dgvCMateriel";
        dgvCMateriel.ReadOnly = true;
        dgvCMateriel.RowHeadersVisible = false;
        dgvCMateriel.RowHeadersWidth = 72;
        dgvCMateriel.RowTemplate.Height = 37;
        dgvCMateriel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCMateriel.Size = new Size(1076, 331);
        dgvCMateriel.TabIndex = 0;
        dgvCMateriel.SelectionChanged += dgvCMateriel_SelectionChanged;
        // 
        // lbxCategoriesCM
        // 
        lbxCategoriesCM.Dock = DockStyle.Fill;
        lbxCategoriesCM.FormattingEnabled = true;
        lbxCategoriesCM.ImeMode = ImeMode.NoControl;
        lbxCategoriesCM.Location = new Point(1082, 2);
        lbxCategoriesCM.Margin = new Padding(2);
        lbxCategoriesCM.Name = "lbxCategoriesCM";
        lbxCategoriesCM.SelectionMode = SelectionMode.MultiExtended;
        lbxCategoriesCM.Size = new Size(196, 331);
        lbxCategoriesCM.Sorted = true;
        lbxCategoriesCM.TabIndex = 11;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.Anchor = AnchorStyles.None;
        tableLayoutPanel5.AutoSize = true;
        tableLayoutPanel5.ColumnCount = 1;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.Controls.Add(lbError, 0, 0);
        tableLayoutPanel5.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
        tableLayoutPanel5.Location = new Point(612, 617);
        tableLayoutPanel5.Margin = new Padding(2);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 1;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
        tableLayoutPanel5.Size = new Size(59, 20);
        tableLayoutPanel5.TabIndex = 10;
        // 
        // lbError
        // 
        lbError.Anchor = AnchorStyles.None;
        lbError.AutoSize = true;
        lbError.Location = new Point(2, 0);
        lbError.Margin = new Padding(2, 0, 2, 0);
        lbError.Name = "lbError";
        lbError.Size = new Size(55, 20);
        lbError.TabIndex = 0;
        lbError.Text = "ERROR";
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.Controls.Add(lbxCategories, 0, 1);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Location = new Point(0, 0);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 2;
        tableLayoutPanel7.Size = new Size(200, 100);
        tableLayoutPanel7.TabIndex = 0;
        // 
        // lbxCategories
        // 
        lbxCategories.Dock = DockStyle.Fill;
        lbxCategories.FormattingEnabled = true;
        lbxCategories.ItemHeight = 30;
        lbxCategories.Location = new Point(3, 3);
        lbxCategories.Name = "lbxCategories";
        lbxCategories.Size = new Size(194, 149);
        lbxCategories.TabIndex = 0;
        // 
        // label11
        // 
        label11.Anchor = AnchorStyles.None;
        label11.AutoSize = true;
        label11.Location = new Point(49, 0);
        label11.Name = "label11";
        label11.Size = new Size(102, 30);
        label11.TabIndex = 1;
        label11.Text = "Catégorie";
        // 
        // listBox1
        // 
        listBox1.Dock = DockStyle.Fill;
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 30;
        listBox1.Location = new Point(3, 23);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(194, 74);
        listBox1.TabIndex = 0;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.None;
        label2.AutoSize = true;
        label2.Location = new Point(49, 0);
        label2.Name = "label2";
        label2.Size = new Size(102, 30);
        label2.TabIndex = 1;
        label2.Text = "Catégorie";
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.ColumnCount = 1;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Controls.Add(listBox1, 0, 1);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Location = new Point(0, 0);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 2;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel4.Size = new Size(200, 100);
        tableLayoutPanel4.TabIndex = 0;
        // 
        // ViewConsultation
        // 
        AcceptButton = btRafraichir;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        ClientSize = new Size(1284, 639);
        Controls.Add(tableLayoutPanel1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(2);
        Name = "ViewConsultation";
        Text = "CMateriel";
        FormClosing += ViewConsultation_FormClosing;
        Load += CMateriel_Load;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        flowLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        tableLayoutPanel6.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCMateriel).EndInit();
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)bdsCMateriel).EndInit();
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private Label lbGestionMatériels;
    private Label lbSI;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbContxNom;
    private Label blContxDtMService;
    private Label lbContxDFGarantie;
    private Label lbUser;
    private Label lbref;
    private Label lbMaitemance;
    private TextBox txbNom;
    private TextBox txbUser;
    private TextBox txbId;
    private TextBox txbCMaitemance;
    private DateTimePicker dtpDFGarantie;
    private DateTimePicker dtpDMService;
    private Button btRafraichir;
    private BindingSource bdsCMateriel;
    private TextBox txbArchive;
    private Label label1;
    private FlowLayoutPanel flowLayoutPanel2;
    private ComboBox cbxCatRecherche;
    private Button btRecherche;
    private FlowLayoutPanel flowLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel7;
    private ListBox lbxCategories;
    private Label label11;
    private ListBox listBox1;
    private Label label2;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel6;
    private DataGridView dgvCMateriel;
    private ListBox lbxCategoriesCM;
    private TableLayoutPanel tableLayoutPanel5;
    private Label lbError;
    private ToolTip toolTipConsultation;
    private Button btClose;
}
