using Domain.Entities;

namespace DAL.Repository.Interface.Generic;

public interface IgenericReadIdRepository<U, T> where T : IEntity
{
    public Task<T> GetByIdAsync(U id);

}
