using Domain.DTO.Gestion;
using Domain.Exeption;
using GestionActifsMateriels.ObjTransf;
using GestionActifsMateriels.Proxy;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using GestionMaterielIntExt.Proxy.Gestion.Interface;
using System.Net.Http.Json;

namespace GestionMaterielIntExt.Proxy.Gestion.Implementation;

internal class ProxiGestionEntreprise : MyProxy, IProxiGestionEntreprise
{
    private static string _Uri { get => URIapi.uri + "gestionentreprise"; }

    private IProxiConnexion _ProxiConnexion { get; set; }

    private static HttpClient _client;

    public ProxiGestionEntreprise(IProxiConnexion proxiConnexion)
    {
        _ProxiConnexion = proxiConnexion;
    }

    #region Get
    public async Task<TrGestionEntrepriseResponse> GetGestionEntreprises()
    {
        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();

            HttpResponseMessage response = new();
            try
            {
                response = await _client.GetAsync(_Uri);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                var ProblemDetail = await response.Content.ReadFromJsonAsync<EProblemDetails>();
                //throw new SysException(ProblemDetail.titre + "\r\n" + ProblemDetail.status + "\r\n" + ProblemDetail.detail);
                throw MadeSysException(ProblemDetail);
            }

            var result = await response.Content.ReadFromJsonAsync<GestionEntrepriseResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");

            var responseTransian = DtoToTransian(result);
            return responseTransian;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion

    private TrGestionEntrepriseResponse DtoToTransian(GestionEntrepriseResponseDto gestionEntrepriseResponseDto)
    {
        var responseTransian = new TrGestionEntrepriseResponse()
        {
            Id = gestionEntrepriseResponseDto.Id,
            EnumEntrepriseGestions = gestionEntrepriseResponseDto.EnumEntrepriseInfos,
            Exp = gestionEntrepriseResponseDto.Exp,
            ErrorMessage = gestionEntrepriseResponseDto.ErrorMessage,
        };
        return responseTransian;
    }
}
