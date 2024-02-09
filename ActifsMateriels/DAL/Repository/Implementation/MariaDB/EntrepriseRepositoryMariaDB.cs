using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class EntrepriseRepositoryMariaDB : IEntrepriseRepository
{
    private readonly IDBSession dB;

    public EntrepriseRepositoryMariaDB(IDBSession dB)
    {
        this.dB = dB;
    }

    public async Task<IEnumerable<Entreprise>> GetAllAsync()
    {
        var myQuery = "SELECT Id, Nom, Tel, Email, Archive, LastModif FROM D_ENTREPRISE;";
        //var result = db.Connection.Query<Categorie>(myQuery);
        var result = await dB.Connection.QueryAsync<Entreprise>(myQuery); 
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }
}
