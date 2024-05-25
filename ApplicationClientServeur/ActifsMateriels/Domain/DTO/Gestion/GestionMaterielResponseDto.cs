using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;

namespace Domain.DTO.Gestion;

public class GestionMaterielResponseDto : DtoAbstract
{

    #region properties
    /// <summary>
    /// liste du matériels infos
    /// </summary>
    public IEnumerable<MaterielGestion> EnumMaterielsGestion { get; set; } = new List<MaterielGestion>();
    /// <summary>
    /// liste des catégories
    /// </summary>
    public IEnumerable<Categorie> EnumCategories { get; set; } = new List<Categorie>();

    public IEnumerable<UserTransit> EnumUsersTransit { get; set; } = new List<UserTransit>();

    public IEnumerable<ContratMaintenance> EnumCMaintenance { get; set; } = new List<ContratMaintenance>();

    public IEnumerable<MaterielCategorie> IenumMaterielCategories { get; set; } = new List<MaterielCategorie>();
    /// <summary>
    /// message pour erreur utilisateur
    /// </summary>
    public string ErrorMessage { get; set; } = null;
    public SysException Exp { get; set; } = null;

    /// <summary>
    /// Id du matériel ajouté ou modifié
    /// </summary>
    public long IdMat { get; set; } = -1;

    /// <summary>
    /// liste de catégogies du marétiel ajoutées ou modifiées
    /// </summary>
    public List<Categorie> CatOfIdMat { get; set; } = new();


    #endregion

    #region constructor
    public GestionMaterielResponseDto() { }
    public GestionMaterielResponseDto(IEnumerable<MaterielGestion> enumMaterielsGestion, IEnumerable<Categorie> enumCategories, IEnumerable<UserTransit> enumUsers, IEnumerable<ContratMaintenance> contratMaintenances) : base()
    {
        EnumMaterielsGestion = enumMaterielsGestion;
        EnumCategories = enumCategories;
        EnumUsersTransit = enumUsers;
        EnumCMaintenance = contratMaintenances;
    }

    public GestionMaterielResponseDto(SysException exeption) : base()
    {
        Exp = exeption;
    }



    #endregion
}
