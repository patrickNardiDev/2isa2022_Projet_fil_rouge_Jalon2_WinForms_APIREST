using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;

namespace Domain.DTO.Consultation;

public class ConsultationResponseDto : DtoAbstract
{
    #region properties
    /// <summary>
    /// liste du matériels infos
    /// </summary>
    public IEnumerable<MaterielsInfos> IenumMaterielsInfos { get; set; } = new List<MaterielsInfos>();
    /// <summary>
    /// liste des catégories
    /// </summary>
    public IEnumerable<Entities.Categorie> IenumCategories { get; set; } = new List<Entities.Categorie>();

    public IEnumerable<MaterielCategorie> IenummaterielCategories { get; set; } = new List<MaterielCategorie>();



    public SysException Exep { get; set; }


    #endregion

    #region constructor
    public ConsultationResponseDto() { }
    public ConsultationResponseDto(IEnumerable<MaterielsInfos> ienumMaterielsInfos, IEnumerable<Domain.Entities.Categorie> ienumCategories) : base()
    {
        IenumMaterielsInfos = ienumMaterielsInfos;
        IenumCategories = ienumCategories;
    }

    public ConsultationResponseDto(SysException exeption) : base()
    {
        Exep = exeption;
    }

    #endregion



}
