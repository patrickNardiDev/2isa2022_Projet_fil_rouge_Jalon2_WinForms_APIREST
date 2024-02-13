using Domain.DTO.Consultation;
using Domain.Exeption;
using GestionActifsMateriels.ObjTransf;
using GestionActifsMateriels.Proxy;
using GestionMaterielIntExt.ObjTransf.Consultation;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using GestionMaterielIntExt.Proxy.Consultation.Interface;
using System.Net.Http.Json;


namespace GestionMaterielIntExt.Proxy.Consultation.Implementation;

public class ProxiConsultation : MyProxy, IProxiConsultation
{
    private IProxiConnexion _ProxiConnexion;
    private static string _Uri { get => URIapi.uri + "consultation"; }

    static HttpClient _client;

    public ProxiConsultation(IProxiConnexion proxiConnexion)
    {
        _ProxiConnexion = proxiConnexion;
    }

    #region GetConsultationMateriels
    public async Task<TrConsultationResponse> GetConsultationMateriels2()
    {
        // 0- transformation d'un objet transian en Dto
        // 1- J'envoie la requête post à l'api comprenant l'objet de requête ConnexionRequestDto
        // 2- je vérifie la réussit de la requête, si non lève une exeption
        // 3- je recupère le resultat en json en le transformant en ConnexionResponseDto
        // 4- je crée un Dto de réponse de consultation
        // 5- j'assigne le le réponse de 1 à la propritété IenumMaterielsInfos du nouveau Dto
        // 6- je retourne un transian
        HttpResponseMessage response = new();

        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();
            try
            {
                response = await _client.GetAsync(_Uri);
                // je vérifie la réussit de la requête, si non lève une exeption
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                var ProblemDetail = await response.Content.ReadFromJsonAsync<EProblemDetails>();
                //throw new SysException(ProblemDetail.titre + "\r\n" + ProblemDetail.status + "\r\n" + ProblemDetail.detail);
                throw MadeSysException(ProblemDetail);
            }

            var result = await response.Content.ReadFromJsonAsync<ConsultationResponseDto>();

            var transianResuslt = new TrConsultationResponse()
            {
                IenumMaterielsInfos = result.IenumMaterielsInfos,
                IenumCategories = result.IenumCategories,
                IenummaterielCategories = result.IenummaterielCategories,
                Exp = result.Exep,
            };

            return transianResuslt;
        }
        return new TrConsultationResponse(new SysException("Vous n'êtes pas connecté"));
    }
    #endregion

    #region GetConsultationMateriels filtre par catégorie

    //public async Task<ConsultationResponseDto> GetConsultationMateriels(long categorie)
    //{
    //    try
    //    {
    //        // 1- je construit l'iru avec l'indengtifiant de la catégorie
    //        // 2- J'envoie la requête post à l'api comprenant l'objet de requête ConnexionRequestDto
    //        // 3- je vérifie la réussit de la requête, si non lève une exeption
    //        // 4- je recupère le resultat en json en le transformant en ConnexionResponseDto
    //        // 5- je crée un Dto de réponse de consultation
    //        // 6- j'assigne le le réponse de 1 à la propritété IenumMaterielsInfos du nouveau Dto
    //        // 7- je retourne ce Dto
    //        if (_ProxiConnexion.IsConnected)
    //        {
    //            _client = _ProxiConnexion.GetClient();

    //            string uri = _Uri + "/" + categorie;
    //            var requestDto = new ConsultationRequestDto(categorie);
    //            HttpResponseMessage response = await _client.GetAsync(uri);
    //            response.EnsureSuccessStatusCode();
    //            var result = await response.Content.ReadFromJsonAsync<ConsultationResponseDto>();
    //            return result;
    //        }
    //        return new ConsultationResponseDto(new SysExeption("Vous n'êtes pas connecté"));
    //    }
    //    catch (HttpRequestException e)
    //    {
    //        return new ConsultationResponseDto(new SysExeption("Erreur serveur"));

    //    }
    //}

    public async Task<TrConsultationResponse> GetConsultationMateriels(long categorie)
    {
        // 0- transformation d'un objet transian en Dto
        // 1- je construit l'iru avec l'indengtifiant de la catégorie
        // 2- J'envoie la requête post à l'api comprenant l'objet de requête ConnexionRequestDto
        // 3- je vérifie la réussit de la requête, si non lève une exeption
        // 4- je recupère le resultat en json en le transformant en ConnexionResponseDto
        // 5- je crée un Dto de réponse de consultation
        // 6- j'assigne le le réponse de 1 à la propritété IenumMaterielsInfos du nouveau Dto
        // 7- je retourne un transian


        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = new();
            var requestDto = new ConsultationRequestDto(categorie);
            string uri = _Uri + "/" + categorie;
            try
            {
                response = await _client.GetAsync(uri);
                // je vérifie la réussit de la requête, si non lève une exeption
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                var ProblemDetail = await response.Content.ReadFromJsonAsync<EProblemDetails>();
                //throw new SysException(ProblemDetail.titre + "\r\n" + ProblemDetail.status + "\r\n" + ProblemDetail.detail);
                throw MadeSysException(ProblemDetail);
            }

            var result = await response.Content.ReadFromJsonAsync<ConsultationResponseDto>();
            var resultTransian = new TrConsultationResponse()
            {
                IenumMaterielsInfos = result.IenumMaterielsInfos,
                IenumCategories = result.IenumCategories,
                IenummaterielCategories = result.IenummaterielCategories,
                Exp = result.Exep,

            };
            return resultTransian;
        }
        return new TrConsultationResponse(new SysException("Vous n'êtes pas connecté"));
    }


    #endregion
}
