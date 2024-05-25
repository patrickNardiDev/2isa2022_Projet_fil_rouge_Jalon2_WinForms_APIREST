using Domain.DTO.Gestion;
using Domain.Exeption;
using GestionActifsMateriels.ObjTransf;
using GestionActifsMateriels.Proxy;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using GestionMaterielIntExt.Proxy.Gestion.Interface;
using System.Net.Http.Json;

namespace GestionMaterielIntExt.Proxy.Gestion.Implementation;

internal class ProxiGestionCMaintenance : MyProxy, IProxiGestionCMaintenance
{
    private static string _Uri { get => URIapi.uri + "gestioncmaintenance"; }

    private IProxiConnexion _ProxiConnexion { get; set; }

    private static HttpClient _client;

    public ProxiGestionCMaintenance(IProxiConnexion proxiConnexion)
    {
        _ProxiConnexion = proxiConnexion;
    }

    #region Get
    public async Task<TrGestionCMaintenanceResponse> GetGestionCMaintenances()
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

            var result = await response.Content.ReadFromJsonAsync<GestionCMaintenanceResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");

            var responseTransian = DtoToTransian(result);
            return responseTransian;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion

    private TrGestionCMaintenanceResponse DtoToTransian(GestionCMaintenanceResponseDto gestionCMaintenanceResponseDto)
    {
        var responseTransian = new TrGestionCMaintenanceResponse()
        {
            Id = gestionCMaintenanceResponseDto.Id,
            EnumCMaintenanceGestions = gestionCMaintenanceResponseDto.EnumCMaintenanceInfos,
            Exp = gestionCMaintenanceResponseDto.Exp,
            ErrorMessage = gestionCMaintenanceResponseDto.ErrorMessage,
        };
        return responseTransian;
    }
}
