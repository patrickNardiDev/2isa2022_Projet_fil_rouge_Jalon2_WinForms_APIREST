using GestionActifsMateriels.Properties;
using GestionMaterielIntExt.Controller.Connexion.Interface;
using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.View.Connexion.Interface;

namespace GestionMaterielIntExt.View.Connexion.Implementation;

public partial class ViewConnexion : Form, IViewConnexion
{

    private bool IsConnected = false;

    // properties d'injection de dépendance
    private readonly IControllerConnexion _cconnexion;

    private bool IsClosing;

    //public ViewGestion FGestion = null;
    //public ViewConsultation FCMateriel = null;

    //Injection de dépendance par le constructeurs
    public ViewConnexion(IControllerConnexion cConnexion)
    {
        this._cconnexion = cConnexion;
        InitializeComponent();
        this.flpMessageError.Visible = false;

    }

    #region Connexion
    /// <summary>
    /// Action bouton connextion 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btConnexion_Click(object sender, EventArgs e)
    {
        // 1- j'appel le contrôleur de connexion pour réaliser la méthode ConnexionUser et récupérer le Modèle de vue
        // 2- je met à jour la vue suivant le modèle de vue
        this.btConnexion.Enabled = false;
        this.txtEmail.Enabled = false;
        this.txtPassword.Enabled = false;
        UpDateView(await ConnexionUser());

    }

    private async void btDeconction_Click(object sender, EventArgs e)
    {
        var msg = MessageBox.Show("Voulez vous vous déconnecter de l'application de gestion des actifs matériels ?", "Information", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
        if (msg == DialogResult.Yes)
        {
            UpDateView(DeconnexionUser());
            this.txtPassword.Text = "password";
            this.txtEmail.Text = "courriel";
        }
    }
    #endregion

    #region Open Consultation & Open Gestion
    private async void btCMateriels_Click(object sender, EventArgs e)
    {
        // 1- j'appel le contrôleur du de connexion pour récupérer le modèle de vue
        await AccesConsultation();
    }

    private async void btGestions_Click(object sender, EventArgs e)
    {
        await AccesGestion();
    }
    #endregion

    #region Load --------------------------------------------

    private void Form1_Load(object sender, EventArgs e)
    {
        RestoreWindowPosition();
        //if (GestionActifsMateriels.Properties.Settings.Default.Position_Form1 != Point.Empty)
        //{
        //    this.Location = GestionActifsMateriels.Properties.Settings.Default.Position_Form1;
        //}
        //if (Properties.Settings.Default.Size_Form1 != Size.Empty)
        //{
        //    this.Size = Properties.Settings.Default.Size_Form1;
        //}
        initForm1();

        this.toolTipConnexion.SetToolTip(this.btCMateriels, "Cliquez pour consulter les matériels.");
        this.toolTipConnexion.SetToolTip(this.btConnexion, "Cliquez pour vous connecter.");
        this.toolTipConnexion.SetToolTip(this.btGestions, "Cliquez accéder à la gestion matériel et fournisser.");
        this.toolTipConnexion.SetToolTip(this.txtEmail, "Veuillez entrer votre mail de connexion.");
        this.toolTipConnexion.SetToolTip(this.txtPassword, "Veuillez entrer votre mot de passe.");

    }
    #endregion

    #region Initialisation couleurs --------------------------------------------
    /// <summary>
    /// Initialisation de la fenêtre
    /// </summary>
    public void initForm1()
    {
        if (true)
        {
            this.BackColor = Color.FromArgb(35, 45, 63); //default colour

            lbSI.ForeColor = Color.FromArgb(77, 195, 250);
            lbGestionMatériels.ForeColor = Color.FromArgb(251, 102, 122);
            btConnexion.BackColor = Color.FromArgb(8, 253, 18);
            this.lbMessageError.ForeColor = Color.FromArgb(251, 102, 122);
            flpBtnGestion.Visible = false;
            //this.btCMateriels.BackColor = ColorClient.GreenColor;
            //this.btGestions.BackColor = ColorClient.GreenColor;
            this.btClose.BackColor = ColorClient.RedColor;
            this.btCloseWindows.BackColor = ColorClient.RedColor;
            this.btDeconction.BackColor = ColorClient.BlueColor;
        }
    }
    #endregion

    #region close
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!IsClosing)
            MessageBox.Show("Vous allez quitter l'application de gestion des actifs matériels ?", "Information", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        //Properties.Settings.Default.Size_Form1 = this.Size;
        //GestionActifsMateriels.Properties.Settings.Default.Position_Form1 = this.Location;
        SaveWindowPosition();
    }

    private void btClose_Click(object sender, EventArgs e)
    {
        closeForm();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        closeForm();
    }

    private void closeForm()
    {
        var msg = MessageBox.Show("Voulez vous quitter l'application de gestion des actifs matériels ?", "Information", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
        if (msg == DialogResult.Yes)
        {
            SaveWindowPosition();
            IsClosing = true;
            this.Close();
        }
    }
    #endregion

    /// <summary>
    /// Par l'InjcD j'appelle le contrôleur concret lui passant les valeurs d'entrées de la fenêtre
    /// </summary>
    /// <returns></returns>
    public async Task<IMViewConnexion> ConnexionUser()
    {
        return await _cconnexion.ConnexionUser(this.txtEmail.Text, this.txtPassword.Text);
    }

    public IMViewConnexion DeconnexionUser()
    {
        return _cconnexion.DeConnexionUser();
    }


    public async Task AccesConsultation()
    {
        //this.btCMateriels.Enabled = false;
        await _cconnexion.AccessConsultation();

    }

    public async Task AccesGestion()
    {
        //this.btGestions.Enabled = false;
        await _cconnexion.AccessGestion();
    }

    #region Update view
    public void UpDateView(IMViewConnexion mViewConnexion)
    {
        this.flpBtnGestion.Visible = mViewConnexion.GetflpBtnGestionVisible();
        this.flpBtnGestion.Enabled = mViewConnexion.GetflpBtnGestionVisible();
        this.flpBtnConnexion.Visible = mViewConnexion.GetflpBtnConnexionVisible();
        this.flpBtnConnexion.Enabled = mViewConnexion.GetflpBtnConnexionVisible();
        this.btCMateriels.Enabled = mViewConnexion.GetbtCMaterielsEnabled();
        this.btGestions.Enabled = mViewConnexion.GetbtGestionsEnabled();
        this.Text += $" - {mViewConnexion.GetbtUserName()}";
        this.IsConnected = mViewConnexion.UserIsConnected();
        this.lbMessageError.Text = mViewConnexion.GetMsgExeption();
        this.flpMessageError.Visible = mViewConnexion.GetFlpMessageError();

        this.btConnexion.Enabled = true;
        this.txtEmail.Enabled = true;
        this.txtPassword.Enabled = true;
        this.btGestions.Enabled = true;
        this.btCMateriels.Enabled = true;

        this.btCMateriels.Enabled = mViewConnexion.GetbtCMaterielsEnabled();
        this.btGestions.Enabled = mViewConnexion.GetbtGestionsEnabled();
    }


    #endregion




    #region position size
    private void RestoreWindowPosition()
    {
        if (Settings.Default.Position_Form1 != Point.Empty)
        {
            this.Location = Settings.Default.Position_Form1;
            this.Size = Settings.Default.Size_Form1;
        }
    }

    private void SaveWindowPosition()
    {
        Settings.Default.Position_Form1 = this.Location;
        Settings.Default.Size_Form1 = this.Size;


        Settings.Default.Save();
    }
    #endregion





}