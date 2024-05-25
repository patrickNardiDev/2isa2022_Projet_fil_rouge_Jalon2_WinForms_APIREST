using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;

namespace GestionMaterielIntExt.ModelsOfView.Consultation.Implementation;

public class MViewConsultation : IMViewConsultation
{
    #region Properties
    public IEnumerable<Categorie> IenumCategories { get; set; }
    public IEnumerable<MaterielsInfos> IenumMaterielsInfo { get; set; }
    public IEnumerable<MaterielCategorie> IenumMaterielCategories { get; set; }
    public SysException? Exp { get; set; }
    public bool Close { get; set; }
    #endregion

    #region Constructor
    public MViewConsultation()
    {
        IenumCategories = new List<Categorie>();
        IenumMaterielsInfo = new List<MaterielsInfos>();
        IenumMaterielCategories = new List<MaterielCategorie>();
        Exp = null;
        Close = false;
    }

    //public MViewConsultation(SysExeption exeption)
    //{
    //    if (exeption is not null)
    //        Exp = exeption;
    //}

    //public MViewConsultation(IEnumerable<Categorie> ienumCategories, IEnumerable<MaterielsInfos> ienumMaterielsInfo, SysExeption? exp, bool close)
    //{
    //    IenumCategories = ienumCategories;
    //    IenumMaterielsInfo = ienumMaterielsInfo;
    //    Exp = exp;
    //    Close = close;
    //}
    #endregion

    public IMViewConsultation FactoMViewConsultation() => new MViewConsultation();

    public void ChangeIenumCategories(IEnumerable<Categorie> newIenumCategories) => IenumCategories = newIenumCategories;

    public void ChangeIenumMaterielInfos(IEnumerable<MaterielsInfos> newIenumMaterielInfos) => IenumMaterielsInfo = newIenumMaterielInfos;

    public void ChangeExeption(SysException exp) => Exp = exp;

    public IEnumerable<Categorie> GetIEnumerableCategorie() => IenumCategories;
    public IEnumerable<MaterielsInfos> GetIenumMaterielsInfo() => IenumMaterielsInfo;

    public SysException GetExeptionClient() => Exp;

}

