using DAL.Repository.Interface.Generic;
using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IUserRepository : IgenericReadAllRepository<User>
{
    public Task<User> GetByEmailAsync(string email);

}
