using Domain.DTO.Connexion;
using GestionMaterielIntExt.ObjTransf.Connexion;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using System.Net.Http.Json;

namespace GestionMaterielIntExt.Proxy.Connexion.Implementation;

internal class ProxiConnexion : IProxiConnexion
{
    private static string _Uri { get => URIapi.uri + "connexion"; }

    private static HttpClient _client = new HttpClient();
    private static string AccessToken { get; set; }
    public bool IsConnected => !string.IsNullOrEmpty(AccessToken);


    /// <summary>
    /// Fornit un client cmprenant le header avec le Token Jwt
    /// </summary>
    /// <returns>client connecté en Jwt</returns>
    public HttpClient GetClient()
    {
        HttpClient client = _client;
        //if (!string.IsNullOrEmpty(AccessToken))
        //{
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        //}
        return client;
    }

    public async Task<trConnexionResponse> ConnexionUserAsync(TrConnexionRequest trConnexionRequest)
    {
        // 0- transformation d'un objet transian en Dto
        // 1- réalise la requête Http en Post avec le Dto de requête
        // 2- Vérifie la statut de la réponse et lève une exeption si besoin
        // 3- désérialise la réponse http pour récupérer le Dto de réponse
        // 4- transformation d'un Dto en transian
        // 5- retourne la Dto réponse

        var dtoRequest = new ConnexionRequestDto()
        {
            Email = trConnexionRequest.Email,
            Password = trConnexionRequest.Password,
        };

        // J'envoie la requête post à l'api comprenant l'objet de requête ConnexionRequestDto
        HttpResponseMessage response = await _client.PostAsJsonAsync(_Uri, dtoRequest);
        // je vérifie la réussit de la requête, si non lève une exeption
        response.EnsureSuccessStatusCode();
        // je recupère et retourne le resultat en json en le transformant en ConnexionResponseDto
        var reponse = await response.Content.ReadFromJsonAsync<ConnexionResponseDto>();
        //enregistrement du token de connexion et inscription dans le header
        AccessToken = reponse.AccessToken;
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);

        var trResponse = new trConnexionResponse()
        {
            Id = reponse.Id,
            NomPrenom = reponse.NomPrenom,
            Role = reponse.Role,
            IsConnected = reponse.IsConnected,
            Exeption = reponse.Exeption,
        };

        return trResponse;
    }

    public bool DeConnexionUserAsync()
    {
        AccessToken = "Listen up! The stars are bright, Does that mean that someone needs them, that someone wants them to exist, that someone is throwing Daisies to the pigs? Vladimir Maïakovski";
        _client.DefaultRequestHeaders.Clear();
        return true;
    }
}
