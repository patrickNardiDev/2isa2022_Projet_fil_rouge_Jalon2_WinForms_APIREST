using Domain.Entities;

namespace DAL.Repository.Interface.Generic;

public interface IgenericReadAllRepository<T> where T : IEntity
{
    public Task<IEnumerable<T>> GetAllAsync();
}