using Domain.DTO.Connexion;
using GestionMaterielIntExt.Model.Connexion.Interface;
using GestionMaterielIntExt.ObjTransf.Connexion;
using GestionMaterielIntExt.Proxy.Connexion.Interface;

namespace GestionMaterielIntExt.Model.Connexion.Implementation;

public class ModelConnexion : IModelConnexion
{
    // ID
    private static IProxiConnexion _pxc;

    public ModelConnexion(IProxiConnexion proxiConnexion)
    {
        _pxc = proxiConnexion;
    }


    // fonction de connexion 
    //public async Task<ConnexionResponseDto> ConnexionUser(ConnexionRequestDto connexionRequestDto)
    //{
    //    // 1- appel du proxi pour effectuer la requête http
    //    // 2- je retourne le Dto réponse
    //    return await _pxc.ConnexionUserAsync(connexionRequestDto);
    //}

    public async Task<trConnexionResponse> ConnexionUser(TrConnexionRequest trConnexionRequest)
    {
        
        return await _pxc.ConnexionUserAsync(trConnexionRequest);
    }

    public bool DeConnexionUser()
    {
        return _pxc.DeConnexionUserAsync();
    }


    //public Task<ConnexionResponseDto> AccesConsultation()
    //{
    //    //ConnexionResponseDto result = await _pxc.AccesConsultation();
    //    //return result;
    //}
}
