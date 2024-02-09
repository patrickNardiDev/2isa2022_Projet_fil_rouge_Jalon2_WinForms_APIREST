using Domain.Entities;
using Domain.Exeption;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.ObjTransf.Context;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Gestion.Interface;
using static GestionMaterielIntExt.ObjTransf.Gestion.TrCRUDtype;

namespace GestionMaterielIntExt.Controller.Gestion.Implementation;

public class ControllerGestionMetariel : IControllerGestionMetariel
{
    #region properties et constructeur
    //private  IViewGestion InstanceView;
    private readonly IModelGestionMateriels _mgestionMat;
    private readonly IMViewGestionMateriel _mviewgestionmat;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IControllerGestionCMaintenance _cgcm;
    private readonly IControllerGestionEntreprise _cge;

    // DI circulaire entre IView et Controller
    // instanciation en deux temps par appel de la méthode ChangeIControllerGestionMateriel avec ce controleur
    private readonly IViewGestion _viewGestion;

    public ControllerGestionMetariel(IModelGestionMateriels modelGestionMateriels, IMViewGestionMateriel mViewGestionMateriels, IViewGestion viewGestion, IMessageBoxService messageBoxService, IControllerGestionCMaintenance controllerGestionCMaintenance, IControllerGestionEntreprise controllerGestionEntreprise)
    {
        _mgestionMat = modelGestionMateriels;
        _mviewgestionmat = mViewGestionMateriels;
        _viewGestion = viewGestion;
        _messageBoxService = messageBoxService;
        _cgcm = controllerGestionCMaintenance;
        _cge = controllerGestionEntreprise;
    }
    #endregion


    #region Open Close view
    public async Task OpenViewGestion()
    {
        int countClass = 0;
        Object Locker = new();

        if (!_viewGestion.GetIsOpen())
        {
            var responseTransian = await _mgestionMat.GetGestionMateriels();
            if (responseTransian is not null)
            {
                var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);
                if (myIMViewGestionMateriels is not null)
                {
                    myIMViewGestionMateriels.EnumMaterielsGestion = responseTransian.EnumMaterielsGestion;
                    myIMViewGestionMateriels.EnumCategories = responseTransian.EnumCategories;
                    myIMViewGestionMateriels.EnumMaterielCategories = responseTransian.IenummaterielCategories;
                    myIMViewGestionMateriels.Exp = responseTransian.Exp;

                    var viewGestionMat = _viewGestion.FactoViewGestion(myIMViewGestionMateriels, _messageBoxService);
                    // Intanciation en 2 temps /!\
                    _viewGestion.ChangeIControllerGestionMateriel(this, _cgcm, _cge);

                    _viewGestion.Show(viewGestionMat);
                    // je définis la vue comme ouverte
                    _viewGestion.ChangeValueOpen(true);
                }
            }
        }
    }

    public void CloseViewGestion()
    {
        _viewGestion.CloseView();
    }
    #endregion

    #region Get - Mise à jour - recherche par catégorie
    public async Task<IMViewGestionMateriel> GetMaterielGestion()
    {
        try
        {
            var responseTransian = await _mgestionMat.GetGestionMateriels();
            _mviewgestionmat.Dispose();
            return MadeMViewGestionMateriel(responseTransian, null);
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }
    }

    public async Task<IMViewGestionMateriel> GetMaterielGestion(Categorie categorie)
    {
        try
        {
            if (categorie is not null)
            {
                //var responseGestionMaterielsDto = await _mgestionMat.GetGestionMateriels(categorie.Id);
                var responseTransian = await _mgestionMat.GetGestionMateriels(categorie.Id);
                if (responseTransian.EnumMaterielsGestion.Count() == 0)
                    _messageBoxService.InformationErrorUserOK($"Pas d'actifs matériels pour la catégorie \"{categorie.Nom}\" sélectionnée");
                _mviewgestionmat.Dispose();
                return MadeMViewGestionMateriel(responseTransian, null);
            }
            else // utile ???
            {
                _mviewgestionmat.Dispose();
                var myIMViewGestionMaterielsError = _mviewgestionmat.FactoMViewGestionMateriels(null);
                var exp = new SysException("Erreur système");
                return myIMViewGestionMaterielsError;
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }

    }

    public async Task<Materiel> GetMaterielById(long id)
    {
        if (id <= 0) return new Materiel();
        var materiel = await _mgestionMat.GetMaterielById(id);
        return materiel;
    }
    #endregion

    #region Ajouter matériel, activation de la zone et flux d'ajout matériel

    public async Task<IMViewGestionMateriel> Ajouter(TrContextTabMateriel ValuesContext)
    {
        try
        {
            if (!_viewGestion.GetModeAjoutIsActif())
                return await ActiverSequenceNewMat(ValuesContext?.IdMat ?? -1);
            else
                return await PostNewMat(ValuesContext);
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }

    }

    /// <summary>
    /// Active les éléments clicables et modifiables de la vue, désactive les autres
    /// </summary>
    /// <returns>void</returns>
    private async Task<IMViewGestionMateriel> ActiverSequenceNewMat(long idmat)
    {
        var myMview = await GetMaterielGestionFactoAsync(TypeMode.Ajouter);
        // j'indique que la séquence d'ajout matériel est activée
        myMview.SequenceAjouterIsActive = true;
        myMview.IdMat = idmat;
        return myMview;
    }

    /// <summary>
    /// Ajouter un matériel
    /// </summary>
    /// <param name="ValuesContext">Valeurs entrées par l'utilisateur</param>
    /// <returns>modèle de vue</returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task<IMViewGestionMateriel> PostNewMat(TrContextTabMateriel ValuesContext)
    {
        // 1- je transforme le ContextTabMateriel en GestionMatrielRequestDto
        var gestionMatrielRequestTransian = TransformContext(ValuesContext);
        gestionMatrielRequestTransian.CRUDtype = MyType.Add;
        // 2- je demande confirmation à l'utilisateurZ
        var reponseQuestion = _messageBoxService.QuestionPopup(ValuesContext, Service.MessageBoxIcon.Type.Ajouter);
        if (reponseQuestion == DialogResult.Yes)
        {
            // 3- si oui j'appel le modèle avec le Dto et je recupère une response Dto 
            var resultTransian = await _mgestionMat.PostNewMat(gestionMatrielRequestTransian);
            if (resultTransian is null)
            {
                return MadeMViewGestionMateriel(null, null);
            }
            else if (resultTransian.ErrorMessage != null)
            {
                _messageBoxService.InformationErrorUserOK(resultTransian.ErrorMessage);
                return await BackupGestionMaterielWithContext(ValuesContext);
            }
            else
            {
                // 4- je transforme la réponse en modèle de vue
                var myMView = MadeMViewGestionMateriel(resultTransian, TypeMode.Ajouter);
                // 5- je récupère le nouveau matériel par son id
                Materiel materielAdded = await GetMaterielById(resultTransian.IdMat);
                // 6- j'informe l'utilisateur de la réussite ou non de l'ajout
                var responseInformation = _messageBoxService.InformationPopupMaterielOK(ValuesContext, materielAdded, resultTransian.CatOfIdMat, Service.MessageBoxIcon.Type.Ajouter);
                // 7- je retourne le modèle de vue avec le nouveau matériel sélectionné si réussite
                return myMView;
            }
        }
        else
        {
            _mviewgestionmat.Dispose();
            return await BackupGestionMaterielWithContext(ValuesContext);
        }
    }
    #endregion

    #region Modifier
    public async Task<IMViewGestionMateriel> Modifier(TrContextTabMateriel ValuesContext)
    {
        try
        {
            if (!_viewGestion.GetModeModifIsActif())
                //if (!_mviewgestionmat.SequenceAjouterIsActive)
                return await ActiverSequenceModifMat(ValuesContext);

            else
                return await ModifMat(ValuesContext);
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }
    }

    private async Task<IMViewGestionMateriel> ActiverSequenceModifMat(TrContextTabMateriel valuecontext)
    {
        var myMview = await GetMaterielGestionFactoAsync(TypeMode.Modifier);
        // j'indique que la séquence d'ajout matériel est activée
        myMview.SequenceModifierIsActive = true;
        myMview.IdMat = valuecontext?.IdMat ?? -1;
        //myMview.
        return myMview;
    }

    private async Task<IMViewGestionMateriel> ModifMat(TrContextTabMateriel ValuesContext)
    {
        // 1- je transforme le ContextTabMateriel en GestionMatrielRequestDto
        var gestionMatrielRequestTransian = TransformContext(ValuesContext);
        gestionMatrielRequestTransian.CRUDtype = MyType.Modify;


        // 2- je demande confirmation à l'utilisateur
        var reponseQuestion = _messageBoxService.QuestionPopup(ValuesContext, Service.MessageBoxIcon.Type.Modifier);

        if (reponseQuestion == DialogResult.Yes)
        {
            // 3- si oui j'appel le modèle avec le Dto et je recupère une response Dto 
            var resultTransian = await _mgestionMat.UpdatefMat(gestionMatrielRequestTransian);
            if (resultTransian is null)
            {
                return MadeMViewGestionMateriel(null, null);
            }
            else if (resultTransian.ErrorMessage != null)
            {
                _messageBoxService.InformationErrorUserOK(resultTransian.ErrorMessage);
                return await BackupGestionMaterielWithContext(ValuesContext);
            }
            else
            {
                // 4- je transforme la réponse en modèle de vue
                var myMView = MadeMViewGestionMateriel(resultTransian, TypeMode.Modifier);

                // 5- je récupère le nouveau matériel par son id
                Materiel materielAdded = await GetMaterielById(resultTransian.IdMat);

                // 6- j'informe l'utilisateur de la réussite ou non de l'ajout
                _messageBoxService.InformationPopupMaterielOK(ValuesContext, materielAdded, resultTransian.CatOfIdMat, Service.MessageBoxIcon.Type.Modifier);

                // 7- je retourne le modèle de vue avec le nouveau matériel sélectionné si réussite
                return myMView;
            }
        }
        else
        {
            _mviewgestionmat.Dispose();
            return await BackupGestionMaterielWithContext(ValuesContext);
        }
        //}
    }

    #endregion

    #region Archive
    public async Task<IMViewGestionMateriel> Archiver(TrContextTabMateriel ValuesContext)
    {
        try
        {
            // je transforme le ContextTabMateriel en GestionMatrielRequestDto
            var gestionMatrielRequestTransian = TransformContext(ValuesContext);
            gestionMatrielRequestTransian.CRUDtype = MyType.Archive; // non utilisé


            // je demande confirmation à l'utilisateur
            var reponseQuestion = _messageBoxService.QuestionPopup(null, Service.MessageBoxIcon.Type.Archiver);
            if (reponseQuestion == DialogResult.Yes)
            {
                // j'appel le modèle
                var resultTransian = await _mgestionMat.ArchivefMat(gestionMatrielRequestTransian);
                if (resultTransian is null)
                {
                    return MadeMViewGestionMateriel(null, null);
                }
                else if (resultTransian.ErrorMessage != null)
                {
                    _messageBoxService.InformationErrorUserOK(resultTransian.ErrorMessage);
                    return await BackupGestionMaterielWithContext(ValuesContext);
                }
                else
                {
                    // je transforme la réponse en modèle de vue
                    var myMView = MadeMViewGestionMateriel(resultTransian, null);

                    // je récupère le nouveau matériel par son id
                    Materiel materielArchived = await GetMaterielById(resultTransian.IdMat);

                    // 6- j'informe l'utilisateur de la réussite ou non de l'ajout
                    _messageBoxService.InformationPopupMaterielOK(ValuesContext, materielArchived, resultTransian.CatOfIdMat, Service.MessageBoxIcon.Type.Archiver);

                    // je retourne le modèle de vue avec le nouveau matériel sélectionné si réussite
                    //myMView.IdMat = ValuesContext?.IdMat ?? -1;
                    //myMView.IdMat = resultTransian.IdMat;
                    return myMView;
                }
            }
            else
            {
                _mviewgestionmat.Dispose();
                return await BackupGestionMaterielWithContext(ValuesContext);
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return await BackupGestionMaterielWithContext(ValuesContext);
        }
    }

    #endregion

    #region Delete
    public async Task<IMViewGestionMateriel> Supprimer(TrContextTabMateriel ValuesContext)
    {
        try
        {
            // je transforme le ContextTabMateriel en GestionMatrielRequestDto
            var gestionMatrielRequestTransian = TransformContext(ValuesContext);
            gestionMatrielRequestTransian.CRUDtype = MyType.Delete; // non utilisé


            // je demande confirmation à l'utilisateur
            var reponseQuestion = _messageBoxService.QuestionPopup(ValuesContext, Service.MessageBoxIcon.Type.Supprimer);
            if (reponseQuestion == DialogResult.Yes)
            {
                // j'appel le modèle
                var resultTransian = await _mgestionMat.SupprimeMat(gestionMatrielRequestTransian);
                if (resultTransian is null)
                {
                    return MadeMViewGestionMateriel(null, null);
                }
                else if (resultTransian.ErrorMessage != null)
                {
                    _messageBoxService.InformationErrorUserOK(resultTransian.ErrorMessage);
                    return await BackupGestionMaterielWithContext(ValuesContext);
                }
                else
                {
                    // je transforme la réponse en modèle de vue
                    var myMView = MadeMViewGestionMateriel(resultTransian, null);

                    // je récupère l'ancien matériel par son id
                    MaterielDetruit materielArchived = await GetMaterielSupprimeById(resultTransian.IdMat); //TODO erreur ici ????

                    //var myMatDeleted = new Mat()
                    //{
                    //    Id = materielArchived.Id,
                    //    Nom = materielArchived.Nom,
                    //    DateMiseEnService = materielArchived.DateMiseEnService,
                    //    DateFinGarantie = null,
                    //    IdMContrat = materielArchived.IdMContrat,
                    //    Archive = true,
                    //    IdUser = null,
                    //    LastModif = materielArchived.DateDestruction,

                    //};

                    // 6- j'informe l'utilisateur de la réussite ou non de l'ajout
                    _messageBoxService.InformationPopupMaterielOK(ValuesContext, materielArchived, resultTransian.CatOfIdMat, Service.MessageBoxIcon.Type.Supprimer);

                    // je retourne le modèle de vue avec le nouveau matériel sélectionné si réussite
                    return myMView;
                }
            }
            else
            {
                _mviewgestionmat.Dispose();
                return await BackupGestionMaterielWithContext(ValuesContext);
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            _mviewgestionmat.Dispose();
            return await BackupGestionMaterielWithContext(ValuesContext);
        }
    }

    public async Task<MaterielDetruit> GetMaterielSupprimeById(long idMat)
    {
        return await _mgestionMat.GetMatDeleted(idMat);
    }

    #endregion

    #region fonctions privées - GetMaterielGestionFactoAsync -  MadeMViewGestionMateriel - TransformContext - GetGestionMateriel - 
    /// <summary>
    /// Fabrique un modèle de vue avec tous les éléments en provenance de la Database
    /// </summary>
    /// <returns></returns>
    private async Task<IMViewGestionMateriel> GetMaterielGestionFactoAsync(TypeMode type)
    {
        //var myMview = IMViewGestionMateriel.Instance;
        var myMview = _mviewgestionmat.FactoMViewGestionMateriels(type);
        // je récupère les data et je complète le modèle de vue
        var responseTransian = await _mgestionMat.GetGestionMateriels();
        //var response = await GetMaterielGestion();

        // si exception je retourne le modèle de vue de base
        if (responseTransian.Exp is not null)
        {
            return MadeMViewGestionMateriel(responseTransian, null);
        }
        else
        {
            // je complète les éléments du modèle de vue
            myMview.EnumUsersShort = MadeUserShort(responseTransian);
            myMview.EnumCategories = responseTransian.EnumCategories;
            myMview.EnumCMaintenances = responseTransian.EnumCMaintenance;
            myMview.EnumMaterielsGestion = responseTransian.EnumMaterielsGestion;
            myMview.EnumMaterielCategories = responseTransian.IenummaterielCategories;

            return myMview;
        }
    }

    /// <summary>
    /// fournit un IMViewGestionMateriel en fonction d'un transian
    /// </summary>
    /// <param name="transian"></param>
    /// <returns>IMViewGestionMateriel</returns>
    private IMViewGestionMateriel MadeMViewGestionMateriel(TrGestionMaterielResponse transian, TypeMode? typeMode)
    {

        if (transian is not null)
        {
            var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(typeMode);
            //var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);

            myIMViewGestionMateriels.EnumMaterielsGestion = transian.EnumMaterielsGestion;
            myIMViewGestionMateriels.EnumCategories = transian.EnumCategories;
            myIMViewGestionMateriels.EnumCMaintenances = transian.EnumCMaintenance;
            myIMViewGestionMateriels.EnumMaterielsGestion = transian.EnumMaterielsGestion;
            myIMViewGestionMateriels.EnumMaterielCategories = transian.IenummaterielCategories;
            myIMViewGestionMateriels.EnumUsersShort = MadeUserShort(transian);

            myIMViewGestionMateriels.IdMat = transian.IdMat; // id of new mat or update mat 

            myIMViewGestionMateriels.Exp = transian.Exp;

            myIMViewGestionMateriels.IsOpen = true;
            return myIMViewGestionMateriels;
        }
        else
        {
            var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);
            myIMViewGestionMateriels.Exp = new SysException("erreur récupération des données");
            return myIMViewGestionMateriels;
        }

    }

    private static List<UserShort> MadeUserShort(TrGestionMaterielResponse transian)
    {
        List<UserShort> myUShort = new();
        foreach (var user in transian.EnumUsers)
        {
            UserShort userShort = new();
            userShort.Id = user.Id;
            userShort.ShortName = user.NameFormatted;
            userShort.Email = user.Email;
            userShort.idSIRole = (int)user.Role;
            myUShort.Add(userShort);
        }
        return myUShort;
    }

    /// <summary>
    /// Transforme les données entrées par l'utilisateur en TrGestionMaterielRequest
    /// </summary>
    /// <param name="ValuesContext">ContextTabMateriel</param>
    /// <returns>TrGestionMaterielRequest</returns>
    private TrGestionMaterielRequest TransformContext(TrContextTabMateriel ValuesContext)
    {
        List<long> listCat = new List<long>();
        var qCategories = from categorie in ValuesContext.Categories
                          select new { id = categorie.Id };
        foreach (var idCat in qCategories)
        {
            listCat.Add(idCat.id);
        }
        var gestionMatrielRequestDto = new TrGestionMaterielRequest()
        {
            CRUDtype = MyType.Read,
            OldIdMat = ValuesContext?.IdMat,
            NewNomMat = ValuesContext.NomMat,
            NewDMService = ValuesContext.DMService,
            NewDFGarantie = ValuesContext.DFGarantie,
            NewIdUser = ValuesContext.UserShort?.Id,
            NewIdCMaintenance = ValuesContext.CMaintenance?.Id,
            NewArchive = ValuesContext.Archive,
            NewListIdCategories = listCat,
            LastModif = ValuesContext.LastModif,
        };
        return gestionMatrielRequestDto;
    }

    private async Task<IMViewGestionMateriel> BackupGestionMaterielWithContext(TrContextTabMateriel valueContext)
    {
        _mviewgestionmat.Dispose();
        var result = await GetMaterielGestion();
        result.ValueTrContextTabMateriel = valueContext;
        result.IsOpen = true;
        result.IdMat = valueContext?.IdMat ?? -1;
        return result;
    }

 
    #endregion
}
