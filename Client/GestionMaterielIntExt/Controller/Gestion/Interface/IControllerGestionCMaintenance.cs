using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;

namespace GestionMaterielIntExt.Controller.Gestion.Interface;

public interface IControllerGestionCMaintenance
{
    public Task<IMViewGestionMateriel> GetCMaintenanceGestion();
}
