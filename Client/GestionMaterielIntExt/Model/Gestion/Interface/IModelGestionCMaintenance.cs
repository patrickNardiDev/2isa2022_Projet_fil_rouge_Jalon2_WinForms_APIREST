using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Model.Gestion.Interface;

public interface IModelGestionCMaintenance
{
    public Task<TrGestionCMaintenanceResponse> GetGestionCMaintenance();
}
