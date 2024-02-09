namespace GestionMaterielIntExt.View.Connexion.Implementation;

partial class ViewConnexion: Form
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewConnexion));
        tableLayoutPanel1 = new TableLayoutPanel();
        tableLayoutPanel2 = new TableLayoutPanel();
        pictureBox1 = new PictureBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        lbSI = new Label();
        lbGestionMatériels = new Label();
        flpBtnConnexion = new FlowLayoutPanel();
        txtEmail = new TextBox();
        txtPassword = new TextBox();
        btConnexion = new Button();
        btClose = new Button();
        flpBtnGestion = new FlowLayoutPanel();
        btCMateriels = new Button();
        btGestions = new Button();
        btDeconction = new Button();
        btCloseWindows = new Button();
        flpMessageError = new FlowLayoutPanel();
        lbMessageError = new Label();
        toolTipConnexion = new ToolTip(components);
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        tableLayoutPanel3.SuspendLayout();
        flpBtnConnexion.SuspendLayout();
        flpBtnGestion.SuspendLayout();
        flpMessageError.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
        tableLayoutPanel1.Controls.Add(flpBtnConnexion, 0, 1);
        tableLayoutPanel1.Controls.Add(flpBtnGestion, 0, 2);
        tableLayoutPanel1.Controls.Add(flpMessageError, 0, 3);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 4;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 66.66666F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.Size = new Size(1062, 488);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Controls.Add(pictureBox1, 0, 0);
        tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(3, 4);
        tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 233F));
        tableLayoutPanel2.Size = new Size(1056, 222);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // pictureBox1
        // 
        pictureBox1.Anchor = AnchorStyles.None;
        pictureBox1.Image = GestionActifsMateriels.Properties.Resources.computer;
        pictureBox1.Location = new Point(135, 28);
        pictureBox1.Margin = new Padding(3, 4, 3, 4);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(258, 166);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
        tableLayoutPanel3.Controls.Add(lbSI, 0, 0);
        tableLayoutPanel3.Controls.Add(lbGestionMatériels, 0, 1);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new Point(531, 4);
        tableLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(522, 214);
        tableLayoutPanel3.TabIndex = 1;
        // 
        // lbSI
        // 
        lbSI.Anchor = AnchorStyles.Bottom;
        lbSI.AutoSize = true;
        lbSI.Font = new Font("Tahoma", 12F);
        lbSI.Location = new Point(145, 83);
        lbSI.Name = "lbSI";
        lbSI.Size = new Size(232, 24);
        lbSI.TabIndex = 0;
        lbSI.Text = "SERVICE INFORMATIQUE";
        // 
        // lbGestionMatériels
        // 
        lbGestionMatériels.Anchor = AnchorStyles.Top;
        lbGestionMatériels.AutoSize = true;
        lbGestionMatériels.Font = new Font("Tahoma", 11.25F);
        lbGestionMatériels.Location = new Point(184, 107);
        lbGestionMatériels.Name = "lbGestionMatériels";
        lbGestionMatériels.Size = new Size(153, 23);
        lbGestionMatériels.TabIndex = 1;
        lbGestionMatériels.Text = "Gestion matériels";
        // 
        // flpBtnConnexion
        // 
        flpBtnConnexion.Anchor = AnchorStyles.None;
        flpBtnConnexion.AutoSize = true;
        flpBtnConnexion.Controls.Add(txtEmail);
        flpBtnConnexion.Controls.Add(txtPassword);
        flpBtnConnexion.Controls.Add(btConnexion);
        flpBtnConnexion.Controls.Add(btClose);
        flpBtnConnexion.Location = new Point(19, 247);
        flpBtnConnexion.Name = "flpBtnConnexion";
        flpBtnConnexion.Size = new Size(1024, 81);
        flpBtnConnexion.TabIndex = 1;
        flpBtnConnexion.WrapContents = false;
        // 
        // txtEmail
        // 
        txtEmail.Anchor = AnchorStyles.Left;
        txtEmail.Location = new Point(23, 27);
        txtEmail.Margin = new Padding(23);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(260, 27);
        txtEmail.TabIndex = 0;
        txtEmail.Text = "votre courriel";
        txtEmail.TextAlign = HorizontalAlignment.Center;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = AnchorStyles.None;
        txtPassword.Location = new Point(329, 27);
        txtPassword.Margin = new Padding(23);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(260, 27);
        txtPassword.TabIndex = 1;
        txtPassword.Text = "password";
        txtPassword.TextAlign = HorizontalAlignment.Center;
        // 
        // btConnexion
        // 
        btConnexion.Anchor = AnchorStyles.Right;
        btConnexion.BackColor = Color.LightCyan;
        btConnexion.Cursor = Cursors.Hand;
        btConnexion.FlatStyle = FlatStyle.Popup;
        btConnexion.Location = new Point(635, 23);
        btConnexion.Margin = new Padding(23);
        btConnexion.Name = "btConnexion";
        btConnexion.Size = new Size(160, 35);
        btConnexion.TabIndex = 2;
        btConnexion.Text = "Connexion";
        btConnexion.UseVisualStyleBackColor = false;
        btConnexion.Click += btConnexion_Click;
        // 
        // btClose
        // 
        btClose.Anchor = AnchorStyles.Right;
        btClose.BackColor = Color.LightCyan;
        btClose.Cursor = Cursors.Hand;
        btClose.FlatStyle = FlatStyle.Popup;
        btClose.Location = new Point(841, 23);
        btClose.Margin = new Padding(23);
        btClose.Name = "btClose";
        btClose.Size = new Size(160, 35);
        btClose.TabIndex = 3;
        btClose.Text = "Fermer";
        btClose.UseVisualStyleBackColor = false;
        btClose.Click += btClose_Click;
        // 
        // flpBtnGestion
        // 
        flpBtnGestion.AutoSize = true;
        flpBtnGestion.Controls.Add(btCMateriels);
        flpBtnGestion.Controls.Add(btGestions);
        flpBtnGestion.Controls.Add(btDeconction);
        flpBtnGestion.Controls.Add(btCloseWindows);
        flpBtnGestion.Dock = DockStyle.Fill;
        flpBtnGestion.Location = new Point(3, 348);
        flpBtnGestion.Name = "flpBtnGestion";
        flpBtnGestion.Size = new Size(1056, 109);
        flpBtnGestion.TabIndex = 2;
        // 
        // btCMateriels
        // 
        btCMateriels.Anchor = AnchorStyles.Left;
        btCMateriels.Cursor = Cursors.Hand;
        btCMateriels.Location = new Point(23, 23);
        btCMateriels.Margin = new Padding(23);
        btCMateriels.Name = "btCMateriels";
        btCMateriels.Size = new Size(210, 63);
        btCMateriels.TabIndex = 2;
        btCMateriels.Text = "&Consultion des actifs matériels";
        btCMateriels.UseVisualStyleBackColor = true;
        btCMateriels.Click += btCMateriels_Click;
        // 
        // btGestions
        // 
        btGestions.Anchor = AnchorStyles.Right;
        btGestions.Cursor = Cursors.Hand;
        btGestions.Location = new Point(279, 23);
        btGestions.Margin = new Padding(23);
        btGestions.Name = "btGestions";
        btGestions.Size = new Size(210, 63);
        btGestions.TabIndex = 3;
        btGestions.Text = "&Gestions des actifs matériels";
        btGestions.UseVisualStyleBackColor = true;
        btGestions.Click += btGestions_Click;
        // 
        // btDeconction
        // 
        btDeconction.Anchor = AnchorStyles.Right;
        btDeconction.Cursor = Cursors.Hand;
        btDeconction.Location = new Point(535, 23);
        btDeconction.Margin = new Padding(23);
        btDeconction.Name = "btDeconction";
        btDeconction.Size = new Size(210, 63);
        btDeconction.TabIndex = 5;
        btDeconction.Text = "&Déconnexion";
        btDeconction.UseVisualStyleBackColor = true;
        btDeconction.Click += btDeconction_Click;
        // 
        // btCloseWindows
        // 
        btCloseWindows.Anchor = AnchorStyles.Right;
        btCloseWindows.Cursor = Cursors.Hand;
        btCloseWindows.Location = new Point(791, 23);
        btCloseWindows.Margin = new Padding(23);
        btCloseWindows.Name = "btCloseWindows";
        btCloseWindows.Size = new Size(210, 63);
        btCloseWindows.TabIndex = 4;
        btCloseWindows.Text = "&Fermer";
        btCloseWindows.UseVisualStyleBackColor = true;
        btCloseWindows.Click += button1_Click;
        // 
        // flpMessageError
        // 
        flpMessageError.Anchor = AnchorStyles.None;
        flpMessageError.AutoSize = true;
        flpMessageError.Controls.Add(lbMessageError);
        flpMessageError.Location = new Point(472, 463);
        flpMessageError.Name = "flpMessageError";
        flpMessageError.Size = new Size(118, 21);
        flpMessageError.TabIndex = 3;
        // 
        // lbMessageError
        // 
        lbMessageError.AutoSize = true;
        lbMessageError.Location = new Point(3, 0);
        lbMessageError.Name = "lbMessageError";
        lbMessageError.Size = new Size(112, 21);
        lbMessageError.TabIndex = 0;
        lbMessageError.Text = "MessageError";
        // 
        // ViewConnexion
        // 
        AutoScaleDimensions = new SizeF(9F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.LightSlateGray;
        ClientSize = new Size(1062, 488);
        Controls.Add(tableLayoutPanel1);
        Font = new Font("Tahoma", 9.857143F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(3, 4, 3, 4);
        MaximumSize = new Size(1080, 535);
        MinimumSize = new Size(1080, 535);
        Name = "ViewConnexion";
        Text = "AMIO SI - Actifs matériels";
        FormClosing += Form1_FormClosing;
        Load += Form1_Load;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        flpBtnConnexion.ResumeLayout(false);
        flpBtnConnexion.PerformLayout();
        flpBtnGestion.ResumeLayout(false);
        flpMessageError.ResumeLayout(false);
        flpMessageError.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private PictureBox pictureBox1;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbSI;
    private Label lbGestionMatériels;
    private FlowLayoutPanel flpBtnConnexion;
    private TextBox txtEmail;
    private TextBox txtPassword;
    private Button btConnexion;
    private FlowLayoutPanel flpBtnGestion;
    private Button btGContrats;
    private Button btGMateriels;
    private Button btCMateriels;
    private Button btGestions;
    private FlowLayoutPanel flpMessageError;
    private Label lbMessageError;
    private ToolTip toolTipConnexion;
    private Button btClose;
    private Button btCloseWindows;
    private Button btDeconction;
}