using Domain.Entities;
using Domain.Exeption;


namespace Domain.DTO.Connexion;

public class ConnexionResponseDto : DtoAbstract
{
    public long Id { get; set; } = -1;

    public string NomPrenom { get; set; }
    public Role.RoleEnum Role { get; set; }
    public bool IsConnected { get; set; }
    public SysException Exeption { get; set; }

    public string AccessToken { get; set; }

    public string SettingType { get; set; }

    public ConnexionResponseDto() { }
    public ConnexionResponseDto(long id, string nomPrenom, Role.RoleEnum role, bool isConnected)
    {
        Id = id;
        NomPrenom = nomPrenom;
        Role = role;
        IsConnected = isConnected;
    }

    public ConnexionResponseDto(SysException e)
    {
        Exeption = e;
    }

}
