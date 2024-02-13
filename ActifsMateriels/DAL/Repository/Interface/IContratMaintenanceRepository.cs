using DAL.Repository.Interface.Generic;
using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IContratMaintenanceRepository : IgenericReadAllRepository<ContratMaintenance>
{
    public Task<IEnumerable<ContratMaintenance>> GetAllAsync();

}
