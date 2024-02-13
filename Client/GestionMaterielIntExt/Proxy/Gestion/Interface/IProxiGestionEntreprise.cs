using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Proxy.Gestion.Interface;

public interface IProxiGestionEntreprise
{
    public Task<TrGestionEntrepriseResponse> GetGestionEntreprises();

}
