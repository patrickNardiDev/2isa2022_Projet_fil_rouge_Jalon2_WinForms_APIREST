using Domain.DTO.Connexion;
using GestionMaterielIntExt.ObjTransf.Connexion;

namespace GestionMaterielIntExt.Model.Connexion.Interface;

public interface IModelConnexion
{
    /// <summary>
    /// fonction de connexion, qui suivant les données de connexion de l'inputsConnexionRDto retourne une ConnexionResponseDto
    /// </summary>
    /// <param name="inputsConnexionRDto"> DTO de requête portant les données entrée par l'utilisateur, email et password</param>
    /// <returns>DTO de réponse</returns>
    //public Task<ConnexionResponseDto> ConnexionUser(ConnexionRequestDto connexionRequestDto);
    public Task<trConnexionResponse> ConnexionUser(TrConnexionRequest trConnexionRequest);
    public bool DeConnexionUser();

}
