using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Proxy.Gestion.Interface;

public interface IProxiGestionCMaintenance
{
    public Task<TrGestionCMaintenanceResponse> GetGestionCMaintenances();

}
