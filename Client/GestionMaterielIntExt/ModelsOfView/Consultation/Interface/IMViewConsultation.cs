using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;

namespace GestionMaterielIntExt.ModelsOfView.Consultation.Interface;

public interface IMViewConsultation
{
    #region Properties
    public IEnumerable<Categorie> IenumCategories { get; set; }
    public IEnumerable<MaterielsInfos> IenumMaterielsInfo { get; set; }
    public IEnumerable<MaterielCategorie> IenumMaterielCategories { get; set; }
    public SysException? Exp { get; set; }

    #endregion

    /// <summary>
    /// Fournit une instationation du modèle de vue
    /// </summary>
    /// <returns></returns>
    public IMViewConsultation FactoMViewConsultation();

    /// <summary>
    /// Mo
    /// </summary>
    /// <param name="newIenumMaterielInfos"></param>
    public void ChangeIenumMaterielInfos(IEnumerable<MaterielsInfos> newIenumMaterielInfos);
    public void ChangeIenumCategories(IEnumerable<Categorie> newIenumCategories);

    public void ChangeExeption(SysException exp);

    public IEnumerable<Categorie> GetIEnumerableCategorie();
    public IEnumerable<MaterielsInfos> GetIenumMaterielsInfo();
    public SysException GetExeptionClient();


}
