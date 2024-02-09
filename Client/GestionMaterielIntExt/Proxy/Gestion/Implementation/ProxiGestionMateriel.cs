using Domain.DTO.Gestion;
using Domain.Entities;
using Domain.Exeption;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using GestionMaterielIntExt.Proxy.Gestion.Interface;
using System.Net.Http.Json;
using System.Text.Json;

namespace GestionMaterielIntExt.Proxy.Gestion.Implementation;

public class ProxiGestionMateriel : IProxiGestionMateriel
{
    private static string _Uri { get => URIapi.uri + "gestionmateriel"; }

    private IProxiConnexion _ProxiConnexion { get; set; }

    private static HttpClient _client;

    public ProxiGestionMateriel(IProxiConnexion proxiConnexion)
    {
        _ProxiConnexion = proxiConnexion;
    }

    #region Get
    public async Task<TrGestionMaterielResponse> GetGestionMateriels()
    {
        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();

            HttpResponseMessage response = await _client.GetAsync(_Uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");

            var responseTransian = DtoToTransian(result);
            return responseTransian;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }

    public async Task<TrGestionMaterielResponse> GetGestionMateriels(long categorie)
    {
        // 1- je construit l'iru avec l'identifiant de la catégorie
        // 2- j'attends la réponse à la requête HTTP
        // 3- je vérifie la réussit de la requête, si non lève une exeption
        // 4- je recupère le resultat en json en le transformant en GestionMaterielResponseDto
        // 5- je retourne un transian

        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();

            string uri = _Uri + "/categorie/" + categorie;
            //var requestDto = new ConsultationRequestDto(categorie);
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");

            var responseTransian = DtoToTransian(result);
            return responseTransian;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }

    public async Task<Materiel> GetMaterielById(long id)
    {
        // 1- je construit l'iru avec l'identifiant du matériel
        // 2- j'attends la réponse à la requête HTTP
        // 3- je vérifie la réussit de la requête, si non lève une exeption
        // 4- je recupère le resultat en json en le transformant en Materiel
        // 5- je retourne ce matériel
        if (_ProxiConnexion.IsConnected)
        {
            _client = _ProxiConnexion.GetClient();
            string uri = _Uri + "/materiel/" + id;
            //var requestDto = new ConsultationRequestDto();
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Materiel>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return result;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion

    #region Post
    public async Task<TrGestionMaterielResponse> PostNewMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var myDto = TransianToDto(trGestionMaterielRequest);

        if (_ProxiConnexion.IsConnected)
        {
            string jsonString = JsonSerializer.Serialize(trGestionMaterielRequest);

            string uri = _Uri + "/add";
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, myDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return DtoToTransian(result);
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion

    #region Update
    public async Task<TrGestionMaterielResponse> PutMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var myDto = TransianToDto(trGestionMaterielRequest);
        //myDto.LastModif = trGestionMaterielRequest.LastModif;

        if (_ProxiConnexion.IsConnected)
        {
            string jsonString = JsonSerializer.Serialize(trGestionMaterielRequest);

            string uri = _Uri + "/update";
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, myDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return DtoToTransian(result);
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }

    #endregion

    #region Archive
    public async Task<TrGestionMaterielResponse> ArchiveMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var myDto = TransianToDto(trGestionMaterielRequest);

        if (_ProxiConnexion.IsConnected)
        {
            string jsonString = JsonSerializer.Serialize(trGestionMaterielRequest);

            string uri = _Uri + "/archive";
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, myDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return DtoToTransian(result);
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion

    #region Delete
    public async Task<TrGestionMaterielResponse> DeleteMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var myDto = TransianToDto(trGestionMaterielRequest);

        if (_ProxiConnexion.IsConnected)
        {
            string jsonString = JsonSerializer.Serialize(trGestionMaterielRequest);

            string uri = _Uri + "/delete/" + trGestionMaterielRequest.OldIdMat;
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GestionMaterielResponseDto>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return DtoToTransian(result);
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }

    public async Task<MaterielDetruit> GetMatDeleted(long idMatDeleted)
    {
        //var myDto = TransianToDto(trGestionMaterielRequest);

        if (_ProxiConnexion.IsConnected)
        {
            string uri = _Uri + "/deleted/" + idMatDeleted;
            _client = _ProxiConnexion.GetClient();
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<MaterielDetruit>();
            if (result is null) throw new SysException("Erreur consultation Matériel");
            else return result;
        }
        else throw new SysException("Vous n'êtes pas connecté");
    }
    #endregion


    #region Functions Transian <--> Dto
    private TrGestionMaterielResponse DtoToTransian(GestionMaterielResponseDto gestionMaterielResponseDto)
    {
        var responseTransian = new TrGestionMaterielResponse()
        {
            EnumMaterielsGestion = gestionMaterielResponseDto.EnumMaterielsGestion,
            EnumCategories = gestionMaterielResponseDto.EnumCategories,
            EnumUsers = gestionMaterielResponseDto.EnumUsersTransit,
            EnumCMaintenance = gestionMaterielResponseDto.EnumCMaintenance,
            IenummaterielCategories = gestionMaterielResponseDto.IenumMaterielCategories,
            Exp = gestionMaterielResponseDto.Exp,
            IdMat = gestionMaterielResponseDto.IdMat,
            CatOfIdMat = gestionMaterielResponseDto.CatOfIdMat,
            ErrorMessage = gestionMaterielResponseDto.ErrorMessage,
        };
        return responseTransian;
    }

    private GestionMatrielRequestDto TransianToDto(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        //var a = (DateTime)trGestionMaterielRequest.NewDMService.ToString("d MMMM yyyy");

        var requestDto = new GestionMatrielRequestDto()
        {
            CRUDtype = (CRUDtype.MyType)trGestionMaterielRequest.CRUDtype,
            NewNomMat = trGestionMaterielRequest.NewNomMat,
            NewDMService = trGestionMaterielRequest.NewDMService,
            NewDFGarantie = trGestionMaterielRequest.NewDFGarantie,
            NewIdUser = trGestionMaterielRequest.NewIdUser,
            NewIdCMaintenance = trGestionMaterielRequest.NewIdCMaintenance,
            NewArchive = trGestionMaterielRequest.NewArchive,
            NewListIdCategories = trGestionMaterielRequest.NewListIdCategories,
            OldIdMat = trGestionMaterielRequest.OldIdMat,
            LastModif = trGestionMaterielRequest.LastModif,
        };
        return requestDto;
    }
    #endregion
}
