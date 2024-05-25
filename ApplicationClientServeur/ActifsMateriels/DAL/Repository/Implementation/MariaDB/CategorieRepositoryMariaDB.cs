using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class CategorieRepositoryMariaDB : ICategorieRepository
{
    private readonly IDBSession db;

    public CategorieRepositoryMariaDB(IDBSession db)
    {
        this.db = db;
    }
    #region GetAll
    /// <summary>
    /// Retourne la table entière des Categorie
    /// </summary>
    /// <returns>Iénumérable de Categorie, ou null</returns>
    public async Task<IEnumerable<Categorie>> GetAllAsync()
    {
        var myQuery = "SELECT Id, Nom FROM D_CATEGORIE;";
        //var result = db.Connection.Query<Categorie>(myQuery);
        var result = await db.Connection.QueryAsync<Categorie>(myQuery);
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;


    }



    public async Task<Categorie> GetByIdCatAsync(long id)
    {
        var myQuery = "SELECT Id, Nom FROM D_CATEGORIE WHERE Id LIKE @id;";
        var parameters = new { id = id, };
        //var result = db.Connection.Query<Categorie>(myQuery);
        var result = await db.Connection.QueryFirstOrDefaultAsync<Categorie>(myQuery, parameters);
        if (result is null) throw new NotFoundEntityException(nameof(result), id);
        return result;
    }
    #endregion
}
