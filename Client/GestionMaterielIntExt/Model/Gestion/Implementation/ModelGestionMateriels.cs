using Domain.Entities;
using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;
using GestionMaterielIntExt.Proxy.Gestion.Interface;

namespace GestionMaterielIntExt.Model.Gestion.Implementation;

public class ModelGestionMateriels : IModelGestionMateriels
{
    private readonly IProxiGestionMateriel _pgestionmat;

    public ModelGestionMateriels(IProxiGestionMateriel pgestionmat)
    {
        _pgestionmat = pgestionmat;
    }

    public async Task<TrGestionMaterielResponse> GetGestionMateriels() => await _pgestionmat.GetGestionMateriels();

    public async Task<TrGestionMaterielResponse> GetGestionMateriels(long categorie) => await _pgestionmat.GetGestionMateriels(categorie);


    public async Task<Materiel> GetMaterielById(long id)
    {
        var result = await _pgestionmat.GetMaterielById(id);
        return result;
    }


    public async Task<TrGestionMaterielResponse> PostNewMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {

        var result = await _pgestionmat.PostNewMat(trGestionMaterielRequest);
        return result;
    }

    public async Task<TrGestionMaterielResponse> UpdatefMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var result = await _pgestionmat.PutMat(trGestionMaterielRequest);
        return result;
    }

    public async Task<TrGestionMaterielResponse> ArchivefMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var result = await _pgestionmat.ArchiveMat(trGestionMaterielRequest);
        return result;
    }

    public async Task<TrGestionMaterielResponse> SupprimeMat(TrGestionMaterielRequest trGestionMaterielRequest)
    {
        var result = await _pgestionmat.DeleteMat(trGestionMaterielRequest);
        return result;
    }

    public async Task<MaterielDetruit> GetMatDeleted(long idMatDeleted)
    {
        var result = await _pgestionmat.GetMatDeleted(idMatDeleted);
        return result;
    }
}
