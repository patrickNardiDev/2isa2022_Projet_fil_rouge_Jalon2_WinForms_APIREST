using Domain.Entities;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Model.Gestion.Interface;

public interface IModelGestionMateriels
{
    #region Get
    public Task<TrGestionMaterielResponse> GetGestionMateriels();
    public Task<TrGestionMaterielResponse> GetGestionMateriels(long categorie);

    public Task<Materiel> GetMaterielById(long id);
    #endregion

    #region Post
    public Task<TrGestionMaterielResponse> PostNewMat(TrGestionMaterielRequest trGestionMaterielRequest);
    #endregion

    #region Update
    public Task<TrGestionMaterielResponse> UpdatefMat(TrGestionMaterielRequest trGestionMaterielRequest);
    #endregion

    public Task<TrGestionMaterielResponse> ArchivefMat(TrGestionMaterielRequest trGestionMaterielRequest);
    public Task<TrGestionMaterielResponse> SupprimeMat(TrGestionMaterielRequest trGestionMaterielRequest);
    public Task<MaterielDetruit> GetMatDeleted(long idMatDeleted);

}
