using GestionMaterielIntExt.Controller.Connexion.Interface;
using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;

namespace GestionMaterielIntExt.View.Connexion.Interface;

public interface IViewConnexion
{

    private static bool IsConnected = false;

    // properties d'injection de dépendance
    private static readonly IControllerConnexion _cconnexion;
    /// <summary>
    /// connexion de l'utilisateur avec renvoie du model de vue pour la mise à jour de la vue
    /// </summary>
    /// <returns></returns>
    public Task<IMViewConnexion> ConnexionUser();

    /// <summary>
    /// Ouverture jde la vue Gestion
    /// </summary>
    /// <returns></returns>
    public Task AccesGestion();

    /// <summary>
    /// Ouverture de la vue Consultation
    /// </summary>
    /// <returns></returns>
    public Task AccesConsultation();

    /// <summary>
    /// Mise à jour de la vue suivant le modèle de vue
    /// </summary>
    /// <param name="modelViewForm1"></param>
    public void UpDateView(IMViewConnexion modelViewForm1);


}
