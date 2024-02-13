using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Gestion.Interface;

namespace GestionMaterielIntExt.Model.Gestion.Implementation;

public class ModelGestionEntreprise : IModelGestionEntreprise
{
    private readonly IProxiGestionEntreprise _pgestionentreprise;

    public ModelGestionEntreprise(IProxiGestionEntreprise pgestionentreprise)
    {
        _pgestionentreprise = pgestionentreprise;
    }
    public async Task<TrGestionEntrepriseResponse> GetGestionEntreprise()
    {
        return await _pgestionentreprise.GetGestionEntreprises();
    }

}
