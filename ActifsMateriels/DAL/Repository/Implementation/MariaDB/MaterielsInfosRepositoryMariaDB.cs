using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Exeption;
using Domain.ValueObjects;

namespace DAL.Repository.Implementation.MariaDB;

internal class MaterielsInfosRepositoryMariaDB : IMaterielsInfosRepository
{
    private readonly IDBSession dB;

    public MaterielsInfosRepositoryMariaDB(IDBSession dBSession)
    {
        this.dB = dBSession;
    }
    /// <summary>
    /// Récupère la table entière des MaterielsInfos
    /// </summary>
    /// <returns>Iénumérable de MaterielsInfos, ou null</returns>
    public async Task<IEnumerable<MaterielsInfos>> GetAllAsync()
    {
        // 1- je définis ma query slq paramétrée suivant le package Nuget Dapper
        // 2- via la dBSession je réalise QueryFirstOrDefaultAsync
        // 3- je retourne le résultat

        var q = "SELECT IdMat, NomMat, IdMat, DateMiseEnService, DateFinGarantie, IdUser, NomUser, IdMContrat, NomContrat, Contrat, Archive FROM D_MaterielsInfos;";
        //var q = "SELECT IdMat, NomMat, IdMat, IdCat, NomCat, DateMiseEnService, DateFinGarantie, IdUser, NomUser, IdMContrat, NomContrat, Contrat, Archive FROM D_MaterielsInfos;";

        var result = await dB.Connection.QueryAsync<MaterielsInfos>(q); //TODO QueryFirstOrDefaultAsync
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }

    /// <summary>
    /// Retourne la table entière des MaterielsInfos filstré par une catégorie
    /// </summary>
    /// <param categorie="idCategorie"></param>
    /// <returns>Iénumérable de MaterielsInfos, ou null</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<MaterielsInfos>> GetByCategorie(long idCategorie)
    {
        if (idCategorie <= 0) throw new NotFoundEntitiesExeption();

        // 1- je définis ma query slq paramétrée suivant le package Nuget Dapper
        // 2- via la dBSession je réalise QueryFirstOrDefaultAsync avec comme paramètre l'id de categorie
        // 3- je retourne le résultat

        var q = "SELECT IdMat, NomMat, IdMat, DateMiseEnService, DateFinGarantie, IdUser, NomUser, IdMContrat, NomContrat, Contrat, Archive FROM D_MaterielsInfos Where IdMat IN (select IdMateriel from D_MATERIEL_CATEGORIE where IdCategorie = @idCategorie);";
        //var q = "SELECT IdMat, NomMat, IdMat, IdCat, NomCat, DateMiseEnService, DateFinGarantie, IdUser, NomUser, IdMContrat, NomContrat, Contrat, Archive FROM D_MaterielsInfos Where IdCat = @idCategorie;";

        var result = await dB.Connection.QueryAsync<MaterielsInfos>(q, new { idCategorie }); //TODO QueryFirstOrDefaultAsync
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }

}
