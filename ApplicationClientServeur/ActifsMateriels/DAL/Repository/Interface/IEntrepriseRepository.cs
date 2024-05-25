using DAL.Repository.Interface.Generic;
using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IEntrepriseRepository : IgenericReadAllRepository<Entreprise>
{
    public Task<IEnumerable<Entreprise>> GetAllAsync();

}
