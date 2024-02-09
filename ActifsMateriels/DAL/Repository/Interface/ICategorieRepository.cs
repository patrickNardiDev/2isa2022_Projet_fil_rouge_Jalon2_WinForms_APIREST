using DAL.Repository.Interface.Generic;
using Domain.Entities;

namespace DAL.Repository.Interface;

public interface ICategorieRepository : IgenericReadAllRepository<Categorie>
{
    public Task<IEnumerable<Categorie>> GetAllAsync();

    public Task<Categorie> GetByIdCatAsync(long id);
}
