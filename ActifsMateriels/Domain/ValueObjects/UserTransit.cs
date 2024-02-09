using Domain.Entities;

namespace Domain.ValueObjects;
public class UserTransit
{
    public UserTransit()
    {
    }

    public UserTransit(long id, string formattedName, string email, Role.RoleEnum? role, bool isConnected)
    {
        Id = id;
        NameFormatted = formattedName;
        Email = email;
        Role = role;
        IsConnected = isConnected;
    }

    public long Id { get; set; }
    public string NameFormatted { get; set; }
    public string Email { get; set; }
    public Role.RoleEnum? Role { get; set; }

    public string AccessToken { get; set; }
    public bool IsConnected { get; set; }

    public string SettingType { get; set; }

}
