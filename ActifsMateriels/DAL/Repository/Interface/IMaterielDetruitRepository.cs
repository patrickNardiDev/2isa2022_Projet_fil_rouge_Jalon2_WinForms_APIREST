using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IMaterielDetruitRepository
{
    public Task<MaterielDetruit> AddAsync(MaterielDetruit materielADetruit);

    public Task<MaterielDetruit> GetByIdAsync(long id);

    public Task<int> DeleteAsync(long id);

}
