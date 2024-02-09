using Domain.ValueObjects;

namespace BLL.Interface;

public interface IBLLConnexionService
{
    /// <summary>
    /// Fonction d'appel au UOW pour accéder à l'utilisateur en fonction de son email et vérifie la correspondance email+password
    /// </summary>
    /// <param name="connexionRequestDto"></param>
    /// <returns>Réponse Dto comprenant l'utilisateur si correspondance, si non réponse null</returns>
    public Task<UserTransit> ConnexionUserAsync(string username, string password);
}
