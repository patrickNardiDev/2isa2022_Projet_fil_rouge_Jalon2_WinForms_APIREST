using Domain.Entities;
using Domain.Exeption;

namespace GestionMaterielIntExt.ObjTransf.Connexion;

public class trConnexionResponse
{
    public long? Id { get; set; }

    public string NomPrenom { get; set; }
    public Role.RoleEnum Role { get; set; }
    public bool IsConnected { get; set; }
    public SysException? Exeption { get; set; }

    public string AccessToken { get; set; }

    public trConnexionResponse() { }
    public trConnexionResponse(long id, string nomPrenom, Role.RoleEnum role, bool isConnected)
    {
        Id = id;
        NomPrenom = nomPrenom;
        Role = role;
        IsConnected = isConnected;
    }

    public trConnexionResponse(SysException e)
    {
        Exeption = e;
    }
}
