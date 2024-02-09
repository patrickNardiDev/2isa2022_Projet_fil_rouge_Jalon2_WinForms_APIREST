namespace Domain.Entities;

public class Role
{
    public static RoleEnum[] _List = (RoleEnum[])Enum.GetValues(typeof(RoleEnum));
    public static List<int> _ListNumber = ((RoleEnum[])Enum.GetValues(typeof(RoleEnum))).Select(c => (int)c).ToList();

    /// <summary>
    /// Rôle utilisateur appartenant au service informatique de l'association AMIO
    /// </summary>
    public enum RoleEnum
    {
        Consultation = 1,
        Gestion = 2,
        noRole = 3,
    };

    public static List<string> GetListRole()
    {
        List<string> result = new();
        foreach (RoleEnum item in _List)
        {
            result.Add(item.ToString());
        }
        return result;
    }
}
