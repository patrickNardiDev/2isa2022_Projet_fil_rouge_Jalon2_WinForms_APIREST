using Domain.Exeption;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Gestion.Interface;

namespace GestionMaterielIntExt.Controller.Gestion.Implementation;

internal class ControllerGestionCMaintenance : IControllerGestionCMaintenance
{
    #region properties et constructeur
    private IViewGestion InstanceView;
    private readonly IModelGestionCMaintenance _mgestionCMaint;
    private readonly IMViewGestionMateriel _mviewgestionmat;
    private readonly IMessageBoxService _messageBoxService;

    // DI circulaire entre IView et Controller
    // instanciation en deux temps par appel de la méthode ChangeIControllerGestionMateriel avec ce controleur

    public ControllerGestionCMaintenance(IModelGestionCMaintenance modelGestionCMaintenance, IMViewGestionMateriel mViewGestionMateriels, IMessageBoxService messageBoxService)
    {
        _mgestionCMaint = modelGestionCMaintenance;
        _mviewgestionmat = mViewGestionMateriels;
        _messageBoxService = messageBoxService;
    }
    #endregion
    public async Task<IMViewGestionMateriel> GetCMaintenanceGestion()
    {
        try
        {
            var responseTransian = await _mgestionCMaint.GetGestionCMaintenance();
            return MadeMViewGestionMateriel(responseTransian);
        }
        catch (SysException e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK("Erreur système");
            return _mviewgestionmat.FactoMViewGestionMateriels(null);
        }
    }

    private IMViewGestionMateriel MadeMViewGestionMateriel(TrGestionCMaintenanceResponse transian)
    {
        if (transian is not null)
        {
            var myIMViewGestionMateriels = _mviewgestionmat.FactoMViewGestionMateriels(null);
            myIMViewGestionMateriels.IdCMaintenance = transian.Id;
            myIMViewGestionMateriels.EnumCMaintenanceGestions = transian.EnumCMaintenanceGestions;

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
