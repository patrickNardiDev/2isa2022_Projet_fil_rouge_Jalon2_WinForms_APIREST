using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;

namespace GestionMaterielIntExt.Controller.Connexion.Interface;

public interface IControllerConnexion
{
    /// <summary>
    /// Fonction de connexion.
    /// Suivant l'utilisateur renvoie un Imodèle-de-vue de la vue connexion.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<IMViewConnexion> ConnexionUser(string email, string password);

    public IMViewConnexion DeConnexionUser();



    public Task<bool> AccessConsultation();

    public Task AccessGestion();

}
