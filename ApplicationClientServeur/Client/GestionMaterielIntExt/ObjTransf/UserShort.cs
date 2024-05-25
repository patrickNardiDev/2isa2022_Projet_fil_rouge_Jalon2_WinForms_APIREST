namespace GestionMaterielIntExt.ObjTransf;

/// <summary>
/// Classe permettant l'affichage du nom formaté
/// Plus trop d'inforations dans User
/// </summary>
public class UserShort
{
    public long Id { get; set; }
    public string ShortName { get; set; }
    public string Email { get; set; }
    public int? idSIRole { get; set; }
}
