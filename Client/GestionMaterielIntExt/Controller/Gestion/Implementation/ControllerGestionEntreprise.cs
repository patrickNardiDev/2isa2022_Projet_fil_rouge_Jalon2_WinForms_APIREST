using Domain.Exeption;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Gestion.Interface;

namespace GestionMaterielIntExt.Controller.Gestion.Implementation;

internal class ControllerGestionEntreprise : IControllerGestionEntreprise
{
    #region properties et constructeur
    private IViewGestion InstanceView;
    private readonly IModelGestionEntreprise _mgestionentreprise;
    private readonly IMViewGestionMateriel _mviewgestionmat;
    private readonly IMessageBoxService _messageBoxService;

    // DI circulaire entre IView et Controller
    // instanciation en deux temps par appel de la méthode ChangeIControllerGestionMateriel avec ce controleur

    public ControllerGestionEntreprise(IModelGestionEntreprise modelGestionEntreprise, IMViewGestionMateriel mViewGestionMateriels, IMessageBoxService messageBoxService)
    {
        _mgestionentreprise = modelGestionEntreprise;
        _mviewgestionmat = mViewGestionMateriels;
        _messageBoxService = messageBoxService;
    }
    #endregion
    public async Task<IMViewGestionMateriel> GetEntrepriseGestion()
    {
        try
        {
            var responseTransian = await _mgestionentreprise.GetGestionEntreprise();
            return MadeMViewGestionMateriel(responseTransian);
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }
    }

    private IMViewGestionMateriel MadeMViewGestionMateriel(TrGestionEntrepriseResponse transian)
    {
        if (transian is not null)
        {
            var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);
            myIMViewGestionMateriels.IdEntreprise = transian.Id;
            myIMViewGestionMateriels.EnumEntrepriseGestions = transian.EnumEntrepriseGestions;

            return myIMViewGestionMateriels;
        }
        else
        {
            var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);
            myIMViewGestionMateriels.Exp = new SysException("erreur récupération des données");
            return myIMViewGestionMateriels;
        }

    }
}
