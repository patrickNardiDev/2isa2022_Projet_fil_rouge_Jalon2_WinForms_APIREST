using DAL.Repository.Interface.Generic;
using Domain.ValueObjects;


namespace DAL.Repository.Interface;

public interface IMaterielsInfosRepository : IgenericReadAllRepository<MaterielsInfos>
{
    public Task<IEnumerable<MaterielsInfos>> GetByCategorie(long categorieId);
}
