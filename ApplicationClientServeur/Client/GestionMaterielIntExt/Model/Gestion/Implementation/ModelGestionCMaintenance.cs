using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Gestion.Interface;

namespace GestionMaterielIntExt.Model.Gestion.Implementation;

internal class ModelGestionCMaintenance : IModelGestionCMaintenance
{
    private readonly IProxiGestionCMaintenance _pgestioncmaintenance;

    public ModelGestionCMaintenance(IProxiGestionCMaintenance proxiGestionCMaintenance)
    {
        _pgestioncmaintenance = proxiGestionCMaintenance;
    }
    public Task<TrGestionCMaintenanceResponse> GetGestionCMaintenance()
    {
        return _pgestioncmaintenance.GetGestionCMaintenances();
    }

}
