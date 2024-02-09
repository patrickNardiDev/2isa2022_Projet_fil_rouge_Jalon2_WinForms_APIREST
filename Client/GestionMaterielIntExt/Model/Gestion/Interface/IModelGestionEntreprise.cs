using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Model.Gestion.Interface;

public interface IModelGestionEntreprise
{
    public Task<TrGestionEntrepriseResponse> GetGestionEntreprise();
}
