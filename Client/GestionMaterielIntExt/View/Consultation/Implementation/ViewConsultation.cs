using Domain.Entities;
using Domain.ValueObjects;
using GestionActifsMateriels.Properties;
using GestionMaterielIntExt.Controller.Consultation.Interface;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Consultation.Interface;
using System.ComponentModel;

namespace GestionMaterielIntExt.View.Consultation.Implementation;

/// <summary>
/// Vue Consultaion
/// 
/// /!\ Injection de dépendance en 2 temps :
/// Constructeur IMViewConsultation
/// puis Mutateur IControllerConsultation par l'appelant
/// </summary>
public partial class ViewConsultation : Form, IViewConsultation
{
    #region properties and construct and change properties and FactoViewConsultation
    public static bool ViewIsOpen { get; private set; } = false;

    internal BindingList<MaterielsInfos> _ListVueMateriel { get; set; } = new();
    private static IEnumerable<MaterielCategorie> IenummaterielCategories { get; set; } = new List<MaterielCategorie>();


    //private readonly IControllerConsultation _cc;
    private static IControllerConsultation _cc { get; set; }
    private readonly IMViewConsultation _mv;

    private bool IsClosing;

    //public ViewConsultation(IMViewConsultation mViewConsultation, IControllerConsultation cc)
    public ViewConsultation(IMViewConsultation mViewConsultation)
    {
        InitializeComponent();
        //if (mViewConsultation.GetIenumMaterielsInfo() != null)
        UpDateView(mViewConsultation);
        //_cc = cc;
    }

    public void ChangeIControllerConsultation(IControllerConsultation controllerConsultation) => _cc = controllerConsultation;

    public ViewConsultation FactoViewConsultation(IMViewConsultation mViewConsultation) => new ViewConsultation(mViewConsultation);

    public bool GetIsOpen() => ViewIsOpen;
    public void ChangeValueOpen(bool value) => ViewIsOpen = value;
    #endregion


    #region Fonction Filter --------------------------------------------
    private async void btRecherche_Click(object sender, EventArgs e)
    {
        // 1- je récupère la valeur sélectionnée (numéro de ligne du combobox string)
        // 2- je demande au contrôleur de consultation de le retourner un modèle de vue comprenant les données necessaires à la vue
        // avec un fitre sur une catégorie
        // 3- je met à jour la vue suivant le modèle de vue retourné


        Categorie selectedCategorie = this.cbxCatRecherche.SelectedItem as Categorie;

        //bool result = int.TryParse(this.cbxCatRecherche.SelectedValue.ToString(), out idCategorie);
        UpDateView(await _cc.GetMaterielsInfos(selectedCategorie));
    }
    #endregion


    #region Load --------------------------------------------
    private void CMateriel_Load(object sender, EventArgs e)
    {
        BindingMaterielsInfos();
        RestoreWindowPosition();

        this.lbError.Visible = false;

        initCMateriel();
        //btRafraichir.PerformClick();

        this.toolTipConsultation.SetToolTip(this.cbxCatRecherche, "Sélection de la catégorie à filtrer.");
        this.toolTipConsultation.SetToolTip(this.btRecherche, "Cliquez pour rechercher les matériels suivant la catégorie sélectionnée.");

    }
    #endregion


    #region Rafraichir --------------------------------------------
    /// <summary>
    /// Bouton rafraichir, affiche la vue matériel asynchrone
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btRafraichir_Click(object sender, EventArgs e)
    {
        // 1- je demande au contrôleur de consultation de le retourner un modèle de vue comprenant les données necessaires à la vue
        // 2- je met à jour la vue suivant le modèle de vue retourné
        UpDateView(await _cc.GetMaterielsInfos());
    }
    #endregion


    #region BindingSource et mise en forme --------------------------------------------
    public void BindingMaterielsInfos()
    {

        //_ListVueMateriel = new();
        //if (_ListVueMateriel.Count() != 0)
        //{
        this.bdsCMateriel.DataSource = _ListVueMateriel;
        this.dgvCMateriel.DataSource = this.bdsCMateriel;

        this.dgvCMateriel.Columns["IdMat"].HeaderText = "Ref Marquage"; //
        this.dgvCMateriel.Columns["NomMat"].HeaderText = "Nom"; // 
        this.dgvCMateriel.Columns["IdCat"].Visible = false; // 2
        this.dgvCMateriel.Columns["NomCat"].HeaderText = "Catégorie"; // 3
        this.dgvCMateriel.Columns["DateMiseEnService"].HeaderText = "Mise en service"; // 4
        this.dgvCMateriel.Columns["DateFinGarantie"].HeaderText = "Fin de garatie"; // 5
        this.dgvCMateriel.Columns["IdUser"].Visible = false; // 6
        this.dgvCMateriel.Columns["NomUser"].HeaderText = "Propriétaire"; // 7
        this.dgvCMateriel.Columns["IdMContrat"].Visible = false; // 8
        this.dgvCMateriel.Columns["NomContrat"].Visible = false; // 9
        this.dgvCMateriel.Columns["Contrat"].HeaderText = "Contrat"; // 10
        this.dgvCMateriel.Columns["Archive"].HeaderText = "Archivé"; // 11

        //dgvCMateriel.Columns[6].DisplayIndex = 0;


        //dgvCMateriel.Columns[1].DisplayIndex = 0;
        //dgvCMateriel.Columns[7].DisplayIndex = 1;
        //dgvCMateriel.Columns[8].DisplayIndex = 2;
        //dgvCMateriel.Columns[0].DisplayIndex = 3;
        //dgvCMateriel.Columns[5].DisplayIndex = 4;
        //dgvCMateriel.Columns[1].DisplayIndex = 5;
        //dgvCMateriel.Columns[4].DisplayIndex = 6;

        dgvCMateriel.Columns["DateMiseEnService"].DefaultCellStyle.Format = "D";
        dgvCMateriel.Columns["DateFinGarantie"].DefaultCellStyle.Format = "D";

        this.txbId.DataBindings.Add("Text", bdsCMateriel, "IdMat", false, DataSourceUpdateMode.Never);
        this.txbNom.DataBindings.Add("Text", bdsCMateriel, "NomMat", false, DataSourceUpdateMode.Never);
        this.lbxCategoriesCM.DataBindings.Add("Text", bdsCMateriel, "NomCat", false, DataSourceUpdateMode.Never);
        this.dtpDMService.DataBindings.Add("Text", bdsCMateriel, "DateMiseEnService", false, DataSourceUpdateMode.Never);
        this.dtpDFGarantie.DataBindings.Add("Text", bdsCMateriel, "DateFinGarantie", false, DataSourceUpdateMode.Never);
        this.txbUser.DataBindings.Add("Text", bdsCMateriel, "NomUser", false, DataSourceUpdateMode.Never);
        this.txbCMaitemance.DataBindings.Add("Text", bdsCMateriel, "Contrat", false, DataSourceUpdateMode.Never);
        this.txbArchive.DataBindings.Add("Text", bdsCMateriel, "Archive", false, DataSourceUpdateMode.Never);
        //this.cbxCategorieCM.DataBindings.Add("Text", bdsCMateriel, "NomCat", false, DataSourceUpdateMode.Never);
        //}


    }
    #endregion


    #region Initialisation couleurs --------------------------------------------
    public void initCMateriel()
    {
        this.BackColor = ColorClient.BgColor;
        lbSI.ForeColor = ColorClient.BlueColor;
        lbGestionMatériels.ForeColor = ColorClient.RedColor;
        this.lbError.ForeColor = ColorClient.RedColor;
        this.btClose.BackColor = ColorClient.RedColor;
    }

    #endregion


    #region Update View --------------------------------------------
    private void UpDateView(IMViewConsultation mViewConsultation)
    {
        if (mViewConsultation.GetExeptionClient() is not null)
        {
            this.lbError.Visible = true;
            this.lbError.Text = "Erreur : vous n'avez pas sélectionné de catégorie";
        }
        else if (mViewConsultation.GetIEnumerableCategorie != null && mViewConsultation.GetIenumMaterielsInfo != null)
        {
            this.lbxCategoriesCM.DisplayMember = "Nom";
            this.cbxCatRecherche.DisplayMember = "Nom";
            //if (this.lbxCategoriesCM is not null && this.cbxCatRecherche is not null)
            //{
            // je vide les éléments
            this.lbxCategoriesCM.ResetText();
            this.lbxCategoriesCM.Items.Clear();
            this.cbxCatRecherche.ResetText();
            this.cbxCatRecherche.Items.Clear();
            // je rempli la liste et la combobox de catégorie
            foreach (var item in mViewConsultation.GetIEnumerableCategorie())
            {
                this.lbxCategoriesCM.Items.Add(item);
                this.cbxCatRecherche.Items.Add(item);
            }
            cbxCatRecherche.Sorted = true;
            lbxCategoriesCM.Sorted = true;
            cbxCatRecherche.Text = "Catégorie";
            IenummaterielCategories = mViewConsultation.IenumMaterielCategories;
            // je mets à jour le tableau
            UpdateDatagrid(mViewConsultation.GetIenumMaterielsInfo());
        }
        else
        {
            this.lbError.Text = "Erreur système";
        }
    }

    private void UpdateDatagrid(IEnumerable<MaterielsInfos> materielInfos)
    {
        // je récupère la position actuelle de sélection
        MaterielsInfos current = this.bdsCMateriel.Current as MaterielsInfos;
        // je remplie ma liste
        //if (current != null)
        _ListVueMateriel.Clear();
        //else
        //    _ListVueMateriel = new();
        foreach (var item in materielInfos)
        {
            _ListVueMateriel.Add(item);
        }
        // On se repositionne sur le current
        if (current is not null)
            this.bdsCMateriel.Position = _ListVueMateriel.IndexOf(_ListVueMateriel.Where(u => u.IdMat == current.IdMat).FirstOrDefault());
    }
    #endregion


    #region Selection change --------------------------------------------
    /// <summary>
    /// Sélectionne les catégories du matériel sélectionné
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dgvCMateriel_SelectionChanged(object sender, EventArgs e)
    {
        MaterielsInfos current = this.bdsCMateriel.Current as MaterielsInfos;

        if (current is not null)
        {
            this.lbxCategoriesCM.ClearSelected();

            var MatCatSelected = from mc in IenummaterielCategories
                                 where mc.IdMateriel == current.IdMat
                                 select mc;

            foreach (var matcat in MatCatSelected)
            {
                for (int i = 0; i < lbxCategoriesCM.Items.Count; i++)
                {
                    var Cat = (Categorie)lbxCategoriesCM.Items[i];
                    if (matcat.IdCategorie == Cat.Id)
                        this.lbxCategoriesCM.SetSelected(i, true);
                }
            }
        }
    }
    #endregion


    #region Closing --------------------------------------------
    private void ViewConsultation_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!IsClosing)
            MessageBox.Show("La fenêtre de consultation des actifs matériels va se fermer", "Information", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        ViewIsOpen = false;
        SaveWindowPosition();
    }

    private void btClose_Click(object sender, EventArgs e)
    {
        var msg = MessageBox.Show("Voulez vous quitter la fenêtre de consultation des actifs matériels", "Information", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);

        if (msg == DialogResult.Yes)
        {
            SaveWindowPosition();
            IsClosing = true;
            this.Close();
        }
    }

    public void CloseView()
    {
        SaveWindowPosition();
        IsClosing = true;
        this.Close();
    }
    #endregion


    #region position size
    private void RestoreWindowPosition()
    {
        if (Settings.Default.Position_FormCMateriel != Point.Empty)
        {
            this.Location = Settings.Default.Position_FormCMateriel;
            this.Size = Settings.Default.Size_FormCMateriel;
        }
    }

    private void SaveWindowPosition()
    {
        Settings.Default.Position_FormCMateriel = this.Location;
        Settings.Default.Size_FormCMateriel = this.Size;

        Settings.Default.Save();
    }
    #endregion




}






