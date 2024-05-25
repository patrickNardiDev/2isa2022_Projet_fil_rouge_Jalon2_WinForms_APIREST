using DAL.Repository.Interface.Generic;
using Domain.Entities;

namespace DAL.Repository.Interface;

public interface IMaterielRepository : IgenericReadAllRepository<Materiel>
{
    /// <summary>
    /// Récupère un matériel suivant son identifiant
    /// </summary>
    /// <param name="idMateriel"></param>
    /// <returns></returns>
    public Task<Materiel> GetByIdAsync(long idMateriel);

    /// <summary>
    /// ajoute un matériel
    /// </summary>
    /// <param name="materiel"></param>
    /// <returns></returns>
    public Task<Materiel> AddAsync(Materiel materiel);

    /// <summary>
    /// met à jour un matériel
    /// </summary>
    /// <param name="materiel"></param>
    /// <returns></returns>
    public Task<Materiel> UpdateAsync(Materiel materiel);
    //public Task<Materiel> UpdateAsync(Materiel newmateriel, Materiel oldmateriel);

    /// <summary>
    /// archive un matériel
    /// </summary>
    /// <param name="materiel"></param>
    /// <returns></returns>
    public Task<Materiel> UpdateArchiveAsync(Materiel materiel);
    public Task<int> Delete(long idMat);





}
