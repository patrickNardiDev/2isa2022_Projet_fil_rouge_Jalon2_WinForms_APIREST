using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class MaterielDetruitRepositoryMariaDB : IMaterielDetruitRepository
{
    private readonly IDBSession _db;

    public MaterielDetruitRepositoryMariaDB(IDBSession db)
    {
        this._db = db;
    }

    public async Task<MaterielDetruit> AddAsync(MaterielDetruit materielADetruit)
    {
        string table = "D_MATERIEL_DETRUIT";

        string items = "Nom, OldNumMarquage, DateMiseEnService, DateDestruction, IdMContrat, ListCategories";

        string variables = "@Nom, @OldNumMarquage, @DateMiseEnService, @DateDestruction, @IdMContrat, @ListCategories";

        string myQuery = $"INSERT INTO {table}({items}) VALUES({variables}); SELECT LAST_INSERT_ID()";
        var newMatDetruit = new MaterielDetruit
        {
            OldNumMarquage = materielADetruit.OldNumMarquage,
            Nom = materielADetruit.Nom,
            DateMiseEnService = materielADetruit.DateMiseEnService,
            DateDestruction = DateTime.Now,
            IdMContrat = materielADetruit?.IdMContrat ?? null,
            ListCategories = materielADetruit.ListCategories,
        };
        materielADetruit.DateDestruction = DateTime.Now;
        var insertIdMat = await _db.Connection.QueryFirstOrDefaultAsync<long>(myQuery, newMatDetruit, _db.Transaction);
        if (insertIdMat <= 0) throw new InsertEntityException(materielADetruit);
        //materielADetruit.Id = insertIdMat;
        materielADetruit.Id = insertIdMat;
        return materielADetruit;
    }

    public async Task<MaterielDetruit> GetByIdAsync(long id)
    {
        string table = "D_MATERIEL_DETRUIT";

        string items = "Id, Nom, OldNumMarquage, DateMiseEnService, DateDestruction, IdMContrat, ListCategories";

        string myQuery = $"SELECT {items} FROM {table} WHERE OldNumMarquage = @ID";

        var parametre = new { Id = id };

        var result = await _db.Connection.QueryFirstOrDefaultAsync<MaterielDetruit>(myQuery, parametre); // defaut
        if (result is null) throw new NotFoundEntitiesExeption(); // différentiation des exeptions
        return result;
    }

    public async Task<int> DeleteAsync(long id)
    {
        //    DELETE FROM D_MATERIEL
        //WHERE Id = 53;
        string table = "D_MATERIEL_DETRUIT";

        string myQuery = $"DELETE FROM {table} WHERE OldNumMarquage = @Id;";
        var parameters = new
        {
            Id = id,
        };
        var result = await _db.Connection.ExecuteAsync(myQuery, parameters, _db.Transaction);
        if (result <= 0) throw new DeleteEntityException(new MaterielDetruit(), id);
        return result;
    }
}
