using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;

namespace GestionMaterielIntExt.ObjTransf.Consultation;

public class TrConsultationResponse
{
    #region properties
    /// <summary>
    /// liste du matériels infos
    /// </summary>
    public IEnumerable<MaterielsInfos> IenumMaterielsInfos { get; set; } = new List<MaterielsInfos>();
    /// <summary>
    /// liste des catégories
    /// </summary>
    public IEnumerable<Categorie> IenumCategories { get; set; } = new List<Domain.Entities.Categorie>();

    public IEnumerable<MaterielCategorie> IenummaterielCategories { get; set; } = new List<MaterielCategorie>();


    public SysException? Exp { get; set; } = null;


    #endregion

    #region constructor
    public TrConsultationResponse() { }
    public TrConsultationResponse(IEnumerable<MaterielsInfos> ienumMaterielsInfos, IEnumerable<Domain.Entities.Categorie> ienumCategories) : base()
    {
        IenumMaterielsInfos = ienumMaterielsInfos;
        IenumCategories = ienumCategories;
    }

    public TrConsultationResponse(SysException? exeption) : base()
    {
        Exp = exeption;
    }

    #endregion
}
