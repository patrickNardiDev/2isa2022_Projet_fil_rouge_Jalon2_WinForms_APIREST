using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IMaterielCategorieRepository // : IgenericReadAllRepository<MaterielCategorie> non employé pour utiliser une autre méthode
{

    public Task<IEnumerable<MaterielCategorie>> GetAllAsync(long? idMat);

    public Task<int> AddAsync(MaterielCategorie materielCategorie);

    public Task<int> DeleteAsync(long idCategorie, long idMateriel);

}
