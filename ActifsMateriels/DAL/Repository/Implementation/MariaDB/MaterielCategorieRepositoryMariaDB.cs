using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class MaterielCategorieRepositoryMariaDB : IMaterielCategorieRepository
{
    private readonly IDBSession _db;

    public MaterielCategorieRepositoryMariaDB(IDBSession db)
    {
        this._db = db;
    }

    #region Get

    public async Task<IEnumerable<MaterielCategorie>> GetAllAsync(long? idMat)
    {

        string myQuery = @"SELECT IdMateriel, IdCategorie FROM D_MATERIEL_CATEGORIE";
        object parametre = null;

        if (idMat != null && idMat > 0)
        {
            myQuery += " WHERE IdMateriel = @IdMat";
            parametre = new { IdMat = idMat };
        }
        //dB.Connection.QueryAsync<MaterielsInfos>(q, new { idCategorie });
        var result = await _db.Connection.QueryAsync<MaterielCategorie>(myQuery, parametre);
        if (result is null) throw new NotFoundEntitiesExeption(); // différentiation des exeptions
        return result;
    }
    #endregion

    #region Post
    public async Task<int> AddAsync(MaterielCategorie materielCategorie)
    {

        string myQuery2 = $"INSERT INTO D_MATERIEL_CATEGORIE(IdMateriel, IdCategorie) VALUES(@IdMateriel, @IdCategorie);";
        var parameter = new
        {
            materielCategorie.IdMateriel,
            materielCategorie.IdCategorie,
        };

        var matCat = await _db.Connection.ExecuteAsync(myQuery2, parameter, _db.Transaction);
        if (matCat <= 0) throw new InsertEntityException(materielCategorie);
        return matCat;
    }
    #endregion

    #region Delete
    public async Task<int> DeleteAsync(long idCategorie, long idMateriel) //TODO erreur dans teste intégration mais pas avec client
    {
        string table = "D_MATERIEL_CATEGORIE";

        string myQuery = $"DELETE FROM {table} WHERE IdMateriel = @IdMateriel AND IdCategorie = @IdCategorie;";
        var parameters = new
        {
            IdMateriel = idMateriel,
            IdCategorie = idCategorie,
        };
        var result = await _db.Connection.ExecuteAsync(myQuery, parameters, _db.Transaction);
        if (result <= 0) throw new DeleteEntityException(new MaterielCategorie(), idMateriel); // TODO ???
        return result;
    }

    #endregion


}
