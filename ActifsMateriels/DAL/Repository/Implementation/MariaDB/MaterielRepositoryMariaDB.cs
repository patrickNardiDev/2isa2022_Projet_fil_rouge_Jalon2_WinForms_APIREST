using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class MaterielRepositoryMariaDB : IMaterielRepository
{
    private readonly IDBSession _db;

    public MaterielRepositoryMariaDB(IDBSession dB)
    {
        this._db = dB;
    }

    #region Get
    public async Task<IEnumerable<Materiel>> GetAllAsync()
    {
        var myQuery = "SELECT Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif FROM D_MATERIEL;";
        //var result = db.Connection.Query<Categorie>(myQuery);
        var result = await _db.Connection.QueryAsync<Materiel>(myQuery);
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }

    public async Task<Materiel> GetByIdAsync(long idMateriel)
    {
        string myQuery = @"SELECT Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif FROM D_MATERIEL";
        object parametre = null;

        if (idMateriel != null && idMateriel > 0)
        {
            myQuery += " WHERE Id = @Id";
            parametre = new { Id = idMateriel };
        }
        var result = await _db.Connection.QueryFirstOrDefaultAsync<Materiel>(myQuery, parametre);
        if (result is null) throw new NotFoundEntitiesExeption(); // différentiation des exeptions
        return result;
    }

    #endregion

    #region Post
    public async Task<Materiel> AddAsync(Materiel materiel)
    {
        string table = "D_MATERIEL";

        string items = "Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive, LastModif";

        string variables = "@Nom, @DateMiseEnService, @DateFinGarantie, @IdUser, @IdMContrat, @Archive, @LastModif";

        string myQuery = $"INSERT INTO {table}({items}) VALUES({variables}); SELECT LAST_INSERT_ID()";
        var parameters = new
        {
            materiel.Nom,
            materiel.DateMiseEnService,
            materiel.DateFinGarantie,
            materiel.IdUser,
            materiel.IdMContrat,
            Archive = 0,
            LastModif = DateTime.Now,
        };
        var insertIdMat = await _db.Connection.QueryFirstOrDefaultAsync<long>(myQuery, parameters, _db.Transaction);
        if (insertIdMat <= 0) throw new InsertEntityException(materiel);
        materiel.Id = insertIdMat;
        return materiel;
    }
    #endregion

    #region Put
    public async Task<Materiel> UpdateAsync(Materiel materiel)
    //public async Task<Materiel> UpdateAsync(Materiel newmateriel, Materiel oldmateriel)
    {
        //bool NomModif, IdUserModif, DateMiseEnServiceModif, DateFinGarantieModif, IdMContratMofif = false;
        //List<string> items2 = new();
        //if (!newmateriel.Nom.Equals(oldmateriel.Nom))
        //{
        //    items2.Add("Nom = @Nom");
        //    NomModif = true;
        //}
        //if (newmateriel.IdUser != oldmateriel.Id)
        //{
        //    items2.Add("IdUser = @IdUser");
        //    IdUserModif = true;
        //}
        //if (newmateriel.DateMiseEnService != oldmateriel.DateMiseEnService)
        //{
        //    items2.Add("DateMiseEnService = @DateMiseEnService");
        //    DateMiseEnServiceModif = true;
        //}
        //if (newmateriel.DateFinGarantie != oldmateriel.DateFinGarantie)
        //{
        //    items2.Add("DateFinGarantie = @DateFinGarantie");
        //    DateFinGarantieModif = true;

        //}
        //if (newmateriel.IdMContrat != oldmateriel.IdMContrat)
        //{
        //    items2.Add("IdMContrat = @IdMContrat");
        //    IdMContratMofif = true;

        //}
        //var items3 = String.Join(",",items2);



        string table = "D_MATERIEL";

        var items = "Nom = @Nom, DateMiseEnService = @DateMiseEnService, DateFinGarantie = @DateFinGarantie, IdUser = @IdUser, IdMContrat = @IdMContrat, LastModif = @LastModif";

        string myQuery = $"UPDATE {table} SET {items} WHERE ID = @Id;";
        var parameters = new
        {
            materiel.Id,
            materiel.Nom,
            materiel.DateMiseEnService,
            materiel.DateFinGarantie,
            materiel.IdUser,
            materiel.IdMContrat,
            materiel.LastModif,
        };
        var result = await _db.Connection.ExecuteAsync(myQuery, parameters, _db.Transaction);
        if (result <= 0) throw new UpdateEntityException(materiel);
        return materiel;
    }


    #endregion

    #region Put Archive
    public async Task<Materiel> UpdateArchiveAsync(Materiel materiel)
    {
        string table = "D_MATERIEL";

        string items = "Archive = @Archive";

        string myQuery = $"UPDATE {table} SET {items} WHERE ID = @Id;";
        var parameters = new
        {
            materiel.Id,
            materiel.Archive,
        };
        var result = await _db.Connection.ExecuteAsync(myQuery, parameters, _db.Transaction);
        if (result <= 0) throw new UpdateEntityException(materiel);
        return materiel;
    }
    #endregion

    #region Delete

    public async Task<int> Delete(long idMat)
    {
        //    DELETE FROM D_MATERIEL
        //WHERE Id = 53;
        string table = "D_MATERIEL";

        string myQuery = $"DELETE FROM {table} WHERE Id = @Id;";
        var parameters = new
        {
            Id = idMat,
        };
        var result = await _db.Connection.ExecuteAsync(myQuery, parameters, _db.Transaction);
        if (result <= 0) throw new DeleteEntityException(new Materiel(), idMat);
        return result;
    }

    #endregion
}
