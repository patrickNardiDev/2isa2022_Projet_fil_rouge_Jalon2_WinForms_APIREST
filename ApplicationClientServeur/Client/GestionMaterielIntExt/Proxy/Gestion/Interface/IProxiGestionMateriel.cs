using Domain.Entities;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

namespace GestionMaterielIntExt.Proxy.Gestion.Interface;

public interface IProxiGestionMateriel
{
    public Task<TrGestionMaterielResponse> GetGestionMateriels();
    public Task<TrGestionMaterielResponse> GetGestionMateriels(long categorie);

    public Task<Materiel> GetMaterielById(long id);

    public Task<TrGestionMaterielResponse> PostNewMat(TrGestionMaterielRequest ValuesContext);

    public Task<TrGestionMaterielResponse> PutMat(TrGestionMaterielRequest trGestionMaterielRequest);
    public Task<TrGestionMaterielResponse> ArchiveMat(TrGestionMaterielRequest trGestionMaterielRequest);
    public Task<TrGestionMaterielResponse> DeleteMat(TrGestionMaterielRequest trGestionMaterielRequest);

    public Task<MaterielDetruit> GetMatDeleted(long idMatDeleted);



}
