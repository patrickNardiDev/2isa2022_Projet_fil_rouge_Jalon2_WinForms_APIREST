using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;

namespace GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

public class TrGestionMaterielResponse
{

    #region properties
    /// <summary>
    /// liste du matériels infos
    /// </summary>
    public IEnumerable<MaterielGestion> EnumMaterielsGestion { get; set; } = new List<MaterielGestion>();
    /// <summary>
    /// liste des catégories
    /// </summary>
    public IEnumerable<Domain.Entities.Categorie> EnumCategories { get; set; } = new List<Domain.Entities.Categorie>();

    public IEnumerable<UserTransit> EnumUsers { get; set; } = new List<UserTransit>(); // TODO passé en UserShort

    public IEnumerable<ContratMaintenance> EnumCMaintenance { get; set; } = new List<ContratMaintenance>();
    public IEnumerable<MaterielCategorie> IenummaterielCategories { get; set; } = new List<MaterielCategorie>();


    public SysException? Exp { get; set; } = null;

    /// <summary>
    /// Id du matériel ajouté ou modifié
    /// </summary>
    public long IdMat { get; set; } = -1;
    public long IdCMaintenance { get; set; } = -1;
    public long IdEntreprise { get; set; } = -1;

    /// <summary>
    /// liste de catégogie du marétiel ajouté ou modifié
    /// </summary>
    public List<Categorie> CatOfIdMat { get; set; } = new();

    public string ErrorMessage { get; set; } = null;


    #endregion

    #region constructor
    public TrGestionMaterielResponse() { }
    public TrGestionMaterielResponse(IEnumerable<MaterielGestion> enumMaterielsGestion, IEnumerable<Domain.Entities.Categorie> enumCategories, IEnumerable<UserTransit> enumUsers, IEnumerable<ContratMaintenance> contratMaintenances) : base()
    {
        EnumMaterielsGestion = enumMaterielsGestion;
        EnumCategories = enumCategories;
        EnumUsers = enumUsers;
        EnumCMaintenance = contratMaintenances;
    }

    public TrGestionMaterielResponse(SysException? exeption) : base()
    {
        Exp = exeption;
    }

    #endregion
}
