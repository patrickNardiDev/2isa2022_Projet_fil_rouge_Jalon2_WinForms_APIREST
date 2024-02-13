
using Domain.Entities;
using Domain.ValueObjects;
using GestionActifsMateriels.Properties;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.ObjTransf.Context;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Gestion.Interface;
using System.ComponentModel;

namespace GestionMaterielIntExt;

/// <summary>
/// Vue Gestion
/// 
/// /!\ Injection de dépendance en 2 temps :
/// Constructeur IMViewConsultation
/// puis Mutateurs pour chaque controllers de gestion lors de l'affichage de la tab concernée
/// </summary>
public partial class ViewGestion : Form, IViewGestion
{
    #region Properties and construtor  and change properties and FactoViewGestion --------------------------------------------


    public static bool ViewIsOpen { get; private set; }
    public static bool ActionEnCours { get; set; }

    // variable d'état pour les séquence d'ajout et de modification matériel
    public static bool ModeAjoutIsOpen { get; private set; }
    public static bool ModeModifIsOpen { get; private set; }

    private bool SequenceAjouterIsActive { get; set; } = false; //NON
    private bool SequenceModifierIsActive { get; set; } = false;
    // end

    // implémentations
    private readonly IMViewGestionMateriel _mv;
    private readonly IMessageBoxService _messageBoxService;
    private static IControllerGestionMetariel _cgm { get; set; }
    private static IControllerGestionCMaintenance _cgcm { get; set; }
    private static IControllerGestionEntreprise _cge { get; set; }
    // end

    //Ienumerable for biding Source
    private static IEnumerable<MaterielCategorie> EnumMaterielCategories { get; set; } = new List<MaterielCategorie>();
    private static IEnumerable<CMaintenanceGestion> EnumCMaintenanceGestions { get; set; } = new List<CMaintenanceGestion>();

    private static IEnumerable<EntrepriseGestion> EnumEntrepriseGestions { get; set; } = new List<EntrepriseGestion>();

    // Binding liste for binding source
    internal BindingList<MaterielGestion> _ListMaterielGestion = new();
    internal BindingList<EntrepriseGestion> _ListEntrepriseGestions = new();
    internal BindingList<CMaintenanceGestion> _ListCMaintenanceGestions = new();

    private bool IsClosing;

    private bool IsDeconnexting;

    /// <summary>
    /// Instance de la vue
    /// </summary>
    private static ViewGestion _Instance { get; set; } = null;


    public ViewGestion(IMViewGestionMateriel mViewGestion, IMessageBoxService messageBoxService)
    {
        InitializeComponent();
        BindingTabMateriel();
        _mv = mViewGestion;
        _messageBoxService = messageBoxService;
        UpDateView(mViewGestion);


    }

    /// <summary>
    /// fontion d'implémentation post instanciation
    /// </summary>
    /// <param name="controllerGestionMetariel"></param>
    /// <param name="controllerGestionCMaintenance"></param>
    /// <param name="controllerGestionEntreprise"></param>
    public void ChangeIControllerGestionMateriel(IControllerGestionMetariel controllerGestionMetariel, IControllerGestionCMaintenance controllerGestionCMaintenance, IControllerGestionEntreprise controllerGestionEntreprise)
    {
        _cgm = controllerGestionMetariel;
        _cgcm = controllerGestionCMaintenance;
        _cge = controllerGestionEntreprise;
    }


    public IViewGestion FactoViewGestion(IMViewGestionMateriel mviewGestion, IMessageBoxService messageBoxService)
    {
        if (_Instance is null)
        {
            _Instance = new ViewGestion(mviewGestion, messageBoxService);
            //ViewIsOpen = true;
            return _Instance;
        }
        else
        {
            //ViewIsOpen = true;
            return _Instance;
        }
        //var result = new ViewGestion(mviewGestion, messageBoxService);
    }

    public void Show(IViewGestion viewGestion)
    {
        ((ViewGestion)viewGestion).Show();
    }

    public void CloseView()
    {
        //this.
        btClose.PerformClick();
        IsDeconnexting = true;
        this.Close();
    }


    public bool GetIsOpen() => ViewIsOpen;

    public bool GetModeAjoutIsActif() => ModeAjoutIsOpen;

    public bool GetModeModifIsActif() => ModeModifIsOpen;

    public void ChangeValueOpen(bool value) => ViewIsOpen = value;

    #endregion

    #region Fonction Filter  --------------------------------------------
    private async void btRecherche_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            Categorie selectedCategorie = this.cbxCatRecherche.SelectedItem as Categorie;
            UpDateView(await _cgm.GetMaterielGestion(selectedCategorie));
        }

    }
    #endregion


    #region Bouton Rafraichir --------------------------------------------

    private async void btRafraichir_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            UpdateTabChange();
        }
    }
    #endregion

    #region Closing --------------------------------------------
    private void ViewGestion_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!ActionEnCours)
        {
            if (!IsClosing)
                MessageBox.Show("Vous allez quitter la fenêtre de gestion des actifs matériels", "Information", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            SaveWindowPosition();
            _mv.Dispose();
            ViewIsOpen = false;
            _Instance = null;
        }
    }

    private void btClose_Click(object sender, EventArgs e)
    {
        DialogResult msg;
        if (!IsDeconnexting)
        {
            msg = MessageBox.Show("Voulez vous quitter la fenêtre de gestion des actifs matériels", "Information", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
        }
        else
        {
            msg = DialogResult.Yes;
        }

        if (msg == DialogResult.Yes)
        {
            SaveWindowPosition();
            IsClosing = true;
            this.Close();
            IsDeconnexting = false;
        }
    }
    #endregion

    #region Tab Changed --------------------------------------------
    private void tbcGestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            UpdateTabChange();
        }
    }

    private async void UpdateTabChange()
    {
        if (ViewIsOpen)
        {
            AllEnabled();
            this.flpFiltreCategorie.Enabled = this.tbcGestion.SelectedIndex == 0 ? true : false;
            this.flpFiltreCategorie.Visible = this.tbcGestion.SelectedIndex == 0 ? true : false;
            //this.flpButton.Enabled = this.tbcGestion.SelectedIndex == 0 ? true : false;
            //this.tlpContextTabMateriel.Visible = this.tbcGestion.SelectedIndex == 0 ? true : false;
            //this.tlpContextTabMateriel.Enabled = this.tbcGestion.SelectedIndex == 0 ? true : false;
            if (this.tbcGestion.SelectedTab == tabMateriel)
            {
                MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

                UpDateView(await _cgm.GetMaterielGestion());

                //repositionnement sur l'ancienne séléction
                if (current is not null)
                    this.bdsTabMateriel.Position = _ListMaterielGestion.IndexOf(_ListMaterielGestion.Where(u => u.IdMat == current.IdMat).FirstOrDefault());

            }
            if (this.tbcGestion.SelectedTab == tabCMaintenance)
            {
                CMaintenanceGestion current = this.bdsTabMateriel.Current as CMaintenanceGestion;

                UpDateView(await _cgcm.GetCMaintenanceGestion());

                // repositionnement sur l'ancienne séléction
                if (current is not null)
                    this.bdsTabMateriel.Position = _ListCMaintenanceGestions.IndexOf(_ListCMaintenanceGestions.Where(u => u.Id == current.Id).FirstOrDefault());
            }
            if (this.tbcGestion.SelectedTab == tabEntreprise)
            {
                EntrepriseGestion current = this.bdsTabMateriel.Current as EntrepriseGestion;

                UpDateView(await _cge.GetEntrepriseGestion());

                // repositionnement sur l'ancienne séléction
                if (current is not null)
                    this.bdsTabMateriel.Position = _ListCMaintenanceGestions.IndexOf(_ListCMaintenanceGestions.Where(u => u.Id == current.Id).FirstOrDefault());
            }
        }
    }
    #endregion

    #region Bouton Annuler --------------------------------------------
    private void btAnnuler_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            string typeAction = string.Empty;
            if (ModeAjoutIsOpen)
                typeAction = "Ajouter un matériel";
            if (ModeAjoutIsOpen)
                typeAction = "Modifier un matériel";
            if (_messageBoxService.QuestionAnnulerActionYesNo(typeAction) == DialogResult.Yes)
            {
                btRafraichir.PerformClick();
            }
        }
    }
    #endregion

    #region Bouton Nouveau --------------------------------------------
    private async void btAjouter_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            AllEnabled();
            MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

            var ValuesContext = MadeContextValues();
            UpDateView(await _cgm.Ajouter(ValuesContext));
        }
    }
    #endregion

    #region Bouton Modifier --------------------------------------------
    private async void btModifier_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            AllEnabled();
            MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

            var ValuesContext = MadeContextValues();
            UpDateView(await _cgm.Modifier(ValuesContext));

            //if (current is not null)
            //    this.bdsTabMateriel.Position = _ListMaterielGestion.IndexOf(_ListMaterielGestion.Where(u => u.IdMat == current.IdMat).FirstOrDefault());
        }


    }
    #endregion


    #region Bouton Archiver --------------------------------------------
    private async void btArchiver_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            this.btArchiver.Enabled = false;
            MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

            AllEnabled();
            var ValuesContext = MadeContextValues();
            ValuesContext.Archive = true;

            UpDateView(await _cgm.Archiver(ValuesContext));

            //if(current is not null)
            //    this.bdsTabMateriel.Position = _ListCMaintenanceGestions.IndexOf(_ListCMaintenanceGestions.Where(u => u.Id == current.IdMat).FirstOrDefault());
        }

    }
    #endregion

    #region Bouton Détruire / Supprimer --------------------------------------------
    private async void btDetruire_Click(object sender, EventArgs e)
    {
        if (!ActionEnCours)
        {
            ActionEnCours = true;
            this.btDetruire.Enabled = false;
            MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

            AllEnabled();
            var ValuesContext = MadeContextValues();

            UpDateView(await _cgm.Supprimer(ValuesContext));
        }
    }
    #endregion

    #region Update View --------------------------------------------

    private void UpDateView(IMViewGestionMateriel mViewGestionMateriels)
    {
        if (mViewGestionMateriels.Exp is not null)
        {
            this.lbError.Visible = true;
            this.lbError.Text = mViewGestionMateriels.Exp.Message;
        }
        else
        {
            initColors();
            ViewIsOpen = mViewGestionMateriels.IsOpen;
            ModeAjoutIsOpen = mViewGestionMateriels.SequenceAjouterIsActive;
            ModeModifIsOpen = mViewGestionMateriels.SequenceModifierIsActive;
            UpdateButtom(mViewGestionMateriels);

            if (ViewIsOpen)
            {
                if (mViewGestionMateriels.EnumMaterielsGestion.Count() > 0)
                {
                    UpDateTabMateriel(mViewGestionMateriels);
                    tlpContextTabMateriel.Visible = true;

                }
                if (mViewGestionMateriels.EnumCMaintenanceGestions.Count() > 0)
                {
                    UpDateTabCMaintenance(mViewGestionMateriels);
                    tlpContextTabMateriel.Visible = false;
                    EnableButton();

                }
                if (mViewGestionMateriels.EnumEntrepriseGestions.Count() > 0)
                {
                    UpDateTabEntreprise(mViewGestionMateriels);
                    tlpContextTabMateriel.Visible = false;
                    EnableButton();
                }
            }
        }
        ActionEnCours = false;
    }

    public void UpDateTabMateriel(IMViewGestionMateriel mViewGestionMateriels)
    {
        UpdateListAndComboboxTabMateriel(mViewGestionMateriels);
        UpdateDatagridTabMateriel(mViewGestionMateriels.EnumMaterielsGestion, mViewGestionMateriels.IdMat);

        //UpdateDatagrid(mViewGestionMateriels.EnumMaterielsGestion, _ListMaterielGestion, bdsTabMateriel, mViewGestionMateriels.IdMat, "IdMat");
        this.tbxContextIdMatTM.Visible = !mViewGestionMateriels.SequenceAjouterIsActive;


        //if (mViewGestionMateriels.IdMat > 0)
        //{
        //    this.bdsTabMateriel.Position = _ListMaterielGestion.IndexOf(_ListMaterielGestion.Where(u => u.IdMat == mViewGestionMateriels.IdMat).FirstOrDefault());
        //}
        if (mViewGestionMateriels.ValueTrContextTabMateriel is not null)
        {
            // je rédéfinis les valeur des inputs du context

            string dateValue = "1900/01/01";

            this.tbxContextNomMatTB.Text = mViewGestionMateriels.ValueTrContextTabMateriel.NomMat;
            this.dtpContextDMServiceTM.Value = (mViewGestionMateriels.ValueTrContextTabMateriel.DMService is not null ? (DateTime)mViewGestionMateriels.ValueTrContextTabMateriel.DMService : DateTime.Parse(dateValue));

            this.dtpContextDFGarantieTM.Value = (mViewGestionMateriels.ValueTrContextTabMateriel?.DFGarantie is not null ? (DateTime)mViewGestionMateriels.ValueTrContextTabMateriel.DFGarantie : DateTime.Parse(dateValue));

            this.cbxContextUserTM.SelectedValue = mViewGestionMateriels.ValueTrContextTabMateriel?.UserShort;

            this.tbxContextIdMatTM.Text = mViewGestionMateriels.ValueTrContextTabMateriel.IdMat.ToString();
            this.cbxContextIdMContratTM.SelectedValue = mViewGestionMateriels.ValueTrContextTabMateriel?.CMaintenance;
        }
    }

    public void UpDateTabCMaintenance(IMViewGestionMateriel mViewGestionMateriels)
    {
        UpdateDatagridTabCMaintenance(mViewGestionMateriels.EnumCMaintenanceGestions, mViewGestionMateriels.IdCMaintenance);

        //UpdateDatagrid(mViewGestionMateriels.EnumCMaintenanceGestions, _ListCMaintenanceGestions, bdsTabCMaintenance, mViewGestionMateriels.IdCMaintenance, "Id");
    }

    public void UpDateTabEntreprise(IMViewGestionMateriel mViewGestionMateriels)
    {
        UpdateDatagridTabEntreprise(mViewGestionMateriels.EnumEntrepriseGestions, mViewGestionMateriels.IdEntreprise);
        //UpdateDatagrid(mViewGestionMateriels.EnumEntrepriseGestions, _ListEntrepriseGestions, bdsTabEntreprise, mViewGestionMateriels.IdEntreprise, "Id");
    }

    #region update Datagrid
    private void UpdateDatagridTabMateriel(IEnumerable<MaterielGestion> materielsGestions, long? idSelected)
    {
        // je mets à jour la datagrid
        _ListMaterielGestion.Clear();

        foreach (var item in materielsGestions)
        {
            _ListMaterielGestion.Add(item);
        }
        if (idSelected > 0 | idSelected is not null)
        {
            this.bdsTabMateriel.Position = _ListMaterielGestion.IndexOf(_ListMaterielGestion.Where(u => u.IdMat == idSelected).FirstOrDefault());
        }
    }

    private void UpdateDatagridTabCMaintenance(IEnumerable<CMaintenanceGestion> cMaintenanceGestions, long? idSelected)
    {
        // je mets à jour la datagrid
        _ListCMaintenanceGestions.Clear();

        foreach (var item in cMaintenanceGestions)
        {
            _ListCMaintenanceGestions.Add(item);
        }
        if (idSelected > 0 | idSelected is not null)
        {
            this.bdsTabCMaintenance.Position = _ListCMaintenanceGestions.IndexOf(_ListCMaintenanceGestions.Where(u => u.Id == idSelected).FirstOrDefault());
        }
    }

    private void UpdateDatagridTabEntreprise(IEnumerable<EntrepriseGestion> entrepriseGestions, long? idSelected)
    {
        // je mets à jour la datagrid
        _ListEntrepriseGestions.Clear();


        foreach (var item in entrepriseGestions)
        {
            _ListEntrepriseGestions.Add(item);
        }
        if (idSelected > 0 | idSelected is not null)
        {
            this.bdsTabEntreprise.Position = _ListEntrepriseGestions.IndexOf(_ListEntrepriseGestions.Where(u => u.Id == idSelected).FirstOrDefault());
        }
    }
    /// <summary>
    /// Définit bindingSource.Position suivant le retour du modèle de vue gestion 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumGestion">Enumérable de retour du modèle de vue</param>
    /// <param name="bListForEnumGestion">Liste privée recevant le énumérable</param>
    /// <param name="bSource">Binding source recevant la liste</param>
    /// <param name="value">Valeur de retour à comparer</param>
    /// <param name="propertyName">Nom de la propritété du databinding à comparer</param>
    private void UpdateDatagrid<T>(IEnumerable<T> enumGestion, BindingList<T> bListForEnumGestion, BindingSource bSource, long? value, string propertyName) where T : ElmntGestion
    {

        // je mets à jour la datagrid
        bListForEnumGestion.Clear();
        var propertyName3 = value.GetType().Attributes; //Test pour supp propertyName
        var propertyName4 = value.GetType().GenericTypeArguments; //Test pour supp propertyName


        foreach (var item in enumGestion)
        {
            bListForEnumGestion.Add(item);
        }
        // je sélectionne le matériel sélectionné ou créé/ mise-à-jour
        if (value is null || value <= 0)
        {
            T current = (T)bSource.Current;
            if (current is not null)
                bSource.Position = bListForEnumGestion.IndexOf(bListForEnumGestion.Where(u =>
                u.GetType().GetProperty(propertyName).GetValue(u) == current.GetType().GetProperty(propertyName).GetValue(current)).FirstOrDefault());
        }
    }
    #endregion



    private void UpdateListAndComboboxTabMateriel(IMViewGestionMateriel mViewGestionMateriels)
    {
        //Catégories
        this.lbxContextCategoriesTM.DisplayMember = "Nom";
        this.cbxCatRecherche.DisplayMember = "Nom";
        this.cbxContextUserTM.DisplayMember = "ShortName";
        this.cbxContextIdMContratTM.DisplayMember = "Nom";

        // je vide les éléments
        this.lbxContextCategoriesTM.ResetText();
        this.lbxContextCategoriesTM.Items.Clear();
        this.cbxCatRecherche.ResetText();
        this.cbxCatRecherche.Items.Clear();
        this.cbxContextUserTM.ResetText();
        this.cbxContextUserTM.Items.Clear();
        this.cbxContextIdMContratTM.ResetText();
        this.cbxContextIdMContratTM.Items.Clear();

        EnumMaterielCategories = mViewGestionMateriels.EnumMaterielCategories;

        // je remplis la listbox et la combobox de catégorie
        foreach (var categorie in mViewGestionMateriels.EnumCategories)
        {
            this.lbxContextCategoriesTM.Items.Add(categorie);
            this.cbxCatRecherche.Items.Add(categorie);
            //_ListCategories.Add(categorie);
        }
        this.cbxContextUserTM.Sorted = true;
        this.lbxContextCategoriesTM.Sorted = true;

        //je remplis la liste des UserShort
        foreach (var user in mViewGestionMateriels.EnumUsersShort)
        {
            this.cbxContextUserTM.Items.Add(user);
            //_ListUsersShrort.Add(user);
        }
        this.cbxContextUserTM.Sorted = true;
        cbxCatRecherche.Text = "Catégorie";
        //ContratMaintenance
        foreach (var contrat in mViewGestionMateriels.EnumCMaintenances)
        {
            this.cbxContextIdMContratTM.Items.Add(contrat);
            //_ListContratMaintenance.Add(contrat);
        }
        this.cbxContextIdMContratTM.Sorted = true;

        if (mViewGestionMateriels.BtAjouterIsActive || mViewGestionMateriels.BtModifierIsActive)
            this.lbxContextCategoriesTM.Enabled = true;
        else
            this.lbxContextCategoriesTM.Enabled = false;

    }

    private void UpdateButtom(IMViewGestionMateriel mViewGestionMateriels)
    {
        this.btAjouter.Enabled = mViewGestionMateriels.BtAjouterIsActive;
        this.btModifier.Enabled = mViewGestionMateriels.BtModifierIsActive;

        this.btRafraichir.Enabled = mViewGestionMateriels.BtRafraichirIsActive;
        this.btAnnuler.Enabled = (mViewGestionMateriels.SequenceAjouterIsActive || mViewGestionMateriels.SequenceModifierIsActive) ? true : false;
        this.btArchiver.Enabled = mViewGestionMateriels.BtArchiveIsActive;
        this.btDetruire.Enabled = mViewGestionMateriels.BtDetruireIsActive;
        this.flpFiltreCategorie.Enabled = mViewGestionMateriels.ZoneFiltreIsActive;

        // je change la couleur des bouton et j'attribut les séquences de flux ouverte
        if (mViewGestionMateriels.SequenceAjouterIsActive)
        {
            SequenceAjouterIsActive = true;
            SequenceModifierIsActive = false;
            this.tlpContextTabMateriel.Enabled = true;
            ActiveContext(true);
            this.btModifier.BackColor = Color.DarkGray;
            this.btArchiver.BackColor = Color.DarkGray;
            this.btDetruire.BackColor = Color.DarkGray;

            //ViderInputs();
        }
        if (mViewGestionMateriels.SequenceModifierIsActive)
        {
            SequenceAjouterIsActive = false;
            SequenceModifierIsActive = true;
            this.tlpContextTabMateriel.Enabled = true;
            ActiveContext(true);
            this.btAjouter.BackColor = Color.DarkGray;
            this.btArchiver.BackColor = Color.DarkGray;
            this.btDetruire.BackColor = Color.DarkGray;
        }
    }

    private void InitButtoms()
    {
        this.btAjouter.Enabled = true;
        this.btModifier.Enabled = true;
        this.btRafraichir.Enabled = true;
        this.btArchiver.Enabled = true;
        this.btDetruire.Enabled = true;
        this.flpFiltreCategorie.Enabled = true;

        this.btAnnuler.Enabled = false;
    }

    private void EnableButton()
    {
        this.btRafraichir.Enabled = true;

        this.btAjouter.Enabled = false;
        this.btModifier.Enabled = false;
        this.btArchiver.Enabled = false;
        this.btDetruire.Enabled = false;
        this.btAnnuler.Enabled = false;
    }


    #endregion


    #region Load position --------------------------------------------
    private void Gestions_Load(object sender, EventArgs e)
    {
        RestoreWindowPosition();
        this.lbError.Visible = false;

        initColors();
        btRafraichir.PerformClick();

        this.toolTipGestion.SetToolTip(this.btAjouter, "Cliquez pour activer la séquence d'ajout.\nCliquez à nouveau pour ajouter");
        this.toolTipGestion.SetToolTip(this.btModifier, "Cliquez pour activer la séquence de modification.\nCliquez à nouveau pour modifier");
        this.toolTipGestion.SetToolTip(this.btAnnuler, "Cliquez pour annuler l'action en cours.");
        this.toolTipGestion.SetToolTip(this.btArchiver, "Cliquez pour archiver l'élément sélectionné.");
        this.toolTipGestion.SetToolTip(this.btDetruire, "Cliquez pour détruite l'élément archivé sélectionné.");
        this.toolTipGestion.SetToolTip(this.btRecherche, "Cliquez pour rechercher les matériels suivant la catégorie sélectionnée.");
        this.toolTipGestion.SetToolTip(this.cbxCatRecherche, "Sélection de la catégorie à filtrer.");
        this.toolTipGestion.SetToolTip(this.lbxContextCategoriesTM, "Catégorie du matériel sélectionné.");




    }
    #endregion


    #region BindingSource et mise en forme tableau et inputs --------------------------------------------

    public void BindingTabMateriel()
    {
        #region Matériel
        //biding Tab gestion matériel
        this.bdsTabMateriel.DataSource = _ListMaterielGestion;
        this.dgvTabMateriel.DataSource = this.bdsTabMateriel;

        //biding Tab gestion contrat maintenance
        this.bdsTabCMaintenance.DataSource = _ListCMaintenanceGestions;
        this.dgvTabCMaintenance.DataSource = this.bdsTabCMaintenance;

        //dibig Tab gestion Entreprise
        this.bdsTabEntreprise.DataSource = _ListEntrepriseGestions;
        this.dgvTabEntreprise.DataSource = this.bdsTabEntreprise;


        this.dgvTabMateriel.Columns["IdMat"].HeaderText = "Référence";
        this.dgvTabMateriel.Columns["NomMat"].HeaderText = "Nom"; // input
        //this.dgvTabMateriel.Columns["IdCat"].Visible = false;
        //this.dgvTabMateriel.Columns["NomCat"].HeaderText = "Catégorie"; // input
        this.dgvTabMateriel.Columns["DateMiseEnService"].HeaderText = "Mise en service"; // input
        this.dgvTabMateriel.Columns["DateFinGarantie"].HeaderText = "Fin de garatie"; // input
        this.dgvTabMateriel.Columns["IdUser"].Visible = false;
        this.dgvTabMateriel.Columns["NomUser"].HeaderText = "Propriétaire"; // input
        this.dgvTabMateriel.Columns["IdMContrat"].HeaderText = "N° contrat de maintenance"; // input
        this.dgvTabMateriel.Columns["NomContrat"].HeaderText = "Nom contrat de maintenance";
        this.dgvTabMateriel.Columns["LastModif"].Visible = false;


        // input
        dgvTabMateriel.Columns["DateMiseEnService"].DefaultCellStyle.Format = "D";
        dgvTabMateriel.Columns["DateFinGarantie"].DefaultCellStyle.Format = "D";

        this.tbxContextNomMatTB.DataBindings.Add("Text", bdsTabMateriel, "NomMat", false, DataSourceUpdateMode.Never);
        this.dtpContextDMServiceTM.DataBindings.Add("Text", bdsTabMateriel, "DateMiseEnService", false, DataSourceUpdateMode.Never);
        this.dtpContextDFGarantieTM.DataBindings.Add("Text", bdsTabMateriel, "DateFinGarantie", false, DataSourceUpdateMode.Never);
        this.tbxContextIdMatTM.DataBindings.Add("Text", bdsTabMateriel, "IdMat", false, DataSourceUpdateMode.Never);

        this.cbxContextUserTM.DataBindings.Add("Text", bdsTabMateriel, "NomUser", false, DataSourceUpdateMode.Never);
        this.cbxContextIdMContratTM.DataBindings.Add("Text", bdsTabMateriel, "NomContrat", false, DataSourceUpdateMode.Never);
        #endregion

        #region contrat de maintenance
        this.dgvTabCMaintenance.Columns["Id"].HeaderText = "Référence";
        this.dgvTabCMaintenance.Columns["Info"].HeaderText = "Informations";
        this.dgvTabCMaintenance.Columns["NbMat"].HeaderText = "nombre de matériels";
        this.dgvTabCMaintenance.Columns["IdEntreprise"].HeaderText = "Identifiant entreprise";
        this.dgvTabCMaintenance.Columns["NomEntreprise"].HeaderText = "Nom entreprise";
        this.dgvTabCMaintenance.Columns["DateDebut"].HeaderText = "Date de début";
        this.dgvTabCMaintenance.Columns["DateFin"].HeaderText = "Date de fin";
        this.dgvTabCMaintenance.Columns["DateDerniereIntervention"].HeaderText = "Dernière intevention";
        this.dgvTabCMaintenance.Columns["DateProchaineIntervention"].HeaderText = "Prochaine intevention";

        this.dgvTabCMaintenance.Columns["LastModif"].Visible = false;
        #endregion

        #region entreprise
        this.dgvTabEntreprise.Columns["Id"].HeaderText = "Référence";
        this.dgvTabEntreprise.Columns["NbContrat"].HeaderText = "Nombre de contrats";
        this.dgvTabEntreprise.Columns["LastModif"].Visible = false;
        #endregion

    }
    #endregion


    #region couleurs --------------------------------------------

    public void initColors()
    {

        this.tlpFormGestion.BackColor = ColorClient.BgColor; ;
        this.lbSI.ForeColor = ColorClient.BlueColor;
        this.lbGestionMatériels.ForeColor = ColorClient.RedColor;
        this.tabMateriel.BorderStyle = BorderStyle.None;
        this.lbError.ForeColor = ColorClient.RedColor;

        this.btAjouter.BackColor = ColorClient.GreenColor;
        // ForeColor à faire
        this.btModifier.BackColor = ColorClient.GreenColor;
        this.btArchiver.BackColor = ColorClient.GreenColor;
        this.btDetruire.BackColor = ColorClient.GreenColor;

        this.btRafraichir.BackColor = ColorClient.BlueColor;
        this.btAnnuler.BackColor = ColorClient.RedColor;
        this.btClose.BackColor = ColorClient.RedColor;
    }

    private void tbxContextNomMatTB_TextChanged(object sender, EventArgs e)
    {
        if (this.tbxContextNomMatTB.Text == string.Empty)
        {
            this.tbxContextNomMatTB.BackColor = ColorClient.RedColor;
        }
        else
        {
            this.tbxContextNomMatTB.BackColor = SystemColors.Window;
        }
    }
    #endregion


    #region Selection change --------------------------------------------
    private void dgvTabMateriel_SelectionChanged(object sender, EventArgs e)
    {
        MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;

        if (current is not null)
        {
            this.lbxContextCategoriesTM.ClearSelected();

            var MatCatSelected = from mc in EnumMaterielCategories
                                 where mc.IdMateriel == current.IdMat
                                 select mc;

            foreach (var matcat in MatCatSelected)
            {
                for (int i = 0; i < lbxContextCategoriesTM.Items.Count; i++)
                {
                    var Cat = (Categorie)lbxContextCategoriesTM.Items[i];
                    if (matcat.IdCategorie == Cat.Id)
                        this.lbxContextCategoriesTM.SetSelected(i, true);
                }
            }
        }

    }
    #endregion


    #region Context --------------------------------------------

    /// <summary>
    /// récupère et fabrique une classe contenant les éléments entrée par l'utilisateur
    /// </summary>
    /// <returns></returns>
    private TrContextTabMateriel MadeContextValues()
    {
        var ValuesContext = new TrContextTabMateriel();

        MaterielGestion current = this.bdsTabMateriel.Current as MaterielGestion;
        ValuesContext.LastModif = current.LastModif;
        ValuesContext.IdMat = current?.IdMat ?? null;



        ValuesContext.NomMat = this.tbxContextNomMatTB.Text != string.Empty ? this.tbxContextNomMatTB.Text : null;
        //ValuesContext.NomMat = this.tbxContextNomMatTB.Text;

        if (!this.dtpContextDMServiceTM.Checked)
            ValuesContext.DMService = null;
        else
            ValuesContext.DMService = this.dtpContextDMServiceTM.Value;

        if (!this.dtpContextDFGarantieTM.Checked)
            ValuesContext.DFGarantie = null;
        else
            ValuesContext.DFGarantie = this.dtpContextDFGarantieTM.Value;

        //UserShort
        //this.cbxContextUserTM.Items.Contains(int.Parse(this.cbxContextUserTM.Text))
        if (cbxContextUserTM.SelectedItem is null)
            ValuesContext.UserShort = null;
        else
        {
            ValuesContext.UserShort = cbxContextUserTM.SelectedItem as UserShort;
            //ValuesContext.IdUser = (cbxContextUserTM.SelectedItem as UserShort).Id; //TODO à supp
            //ValuesContext.UserShortName = (cbxContextUserTM.SelectedItem as UserShort).ShortName;
        }


        // TODO if not UserSort idem en bas
        if (cbxContextIdMContratTM.SelectedItem is null)
            ValuesContext.CMaintenance = null;
        else
            ValuesContext.CMaintenance = cbxContextIdMContratTM.SelectedItem as ContratMaintenance;

        ValuesContext.Archive = current.Archive;

        List<Categorie> myCategories = new();
        foreach (var item in this.lbxContextCategoriesTM.SelectedItems)
        {
            var categorie = item as Categorie;
            if (categorie != null)
                myCategories.Add(categorie);
        }
        ValuesContext.Categories = myCategories;
        return ValuesContext;
    }
    public void ViderInputs()
    {
        //tabMateriel 
        this.tbxContextNomMatTB.Text = string.Empty;
        this.dtpContextDMServiceTM.Text = string.Empty;
        this.dtpContextDFGarantieTM.Text = string.Empty;
        this.cbxContextUserTM.Text = string.Empty;
        this.tbxContextIdMatTM.Text = string.Empty;
        this.cbxContextIdMContratTM.Text = string.Empty;

        this.tbxContextIdMatTM.Visible = false;
    }

    private void ActiveContext(bool active)
    //private void ActiveInput(IMViewGestionMateriel mViewGestionMateriels)
    {
        //tabMateriel 
        this.tbxContextNomMatTB.Enabled = active;
        this.dtpContextDMServiceTM.Enabled = active;
        this.dtpContextDFGarantieTM.Enabled = active;
        this.cbxContextUserTM.Enabled = active;
        //this.tbxContextIdMatTM.Enabled = active;
        this.cbxContextIdMContratTM.Enabled = active;
        this.flpButton.Enabled = active;

    }

    private void AllEnabled()
    {
        this.flpFiltreCategorie.Enabled = false;
        //this.flpButton.Enabled = false;
        this.tlpContextTabMateriel.Enabled = false;
    }

    #endregion


    #region position & size
    private void RestoreWindowPosition()
    {
        if (Settings.Default.Position_FormGestion != Point.Empty)
        {
            this.Location = Settings.Default.Position_FormGestion;
            this.Size = Settings.Default.Size_FormGestion;
        }
    }

    private void SaveWindowPosition()
    {
        Settings.Default.Position_FormGestion = this.Location;
        Settings.Default.Size_FormGestion = this.Size;


        Settings.Default.Save();
    }
    #endregion




}
