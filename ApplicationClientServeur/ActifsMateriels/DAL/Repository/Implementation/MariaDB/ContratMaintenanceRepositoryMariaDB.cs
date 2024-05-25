using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;

namespace DAL.Repository.Implementation.MariaDB;

internal class ContratMaintenanceRepositoryMariaDB : IContratMaintenanceRepository
{
    private readonly IDBSession dB;

    public ContratMaintenanceRepositoryMariaDB(IDBSession dB)
    {
        this.dB = dB;
    }

    public async Task<IEnumerable<ContratMaintenance>> GetAllAsync()
    {
        var myQuery = "SELECT Id, Nom, Info, DateDebut, DateFin, DateDerniereIntervention, DateProfaineIntervention, IdEntreprise, Archive, LastModif FROM D_MCONTRAT;";
        //var result = db.Connection.Query<Categorie>(myQuery);
        var result = await dB.Connection.QueryAsync<ContratMaintenance>(myQuery);
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }
}
