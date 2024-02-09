using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;

namespace GestionMaterielIntExt.Controller.Gestion.Interface;

public interface IControllerGestionEntreprise
{
    public Task<IMViewGestionMateriel> GetEntrepriseGestion();

}
