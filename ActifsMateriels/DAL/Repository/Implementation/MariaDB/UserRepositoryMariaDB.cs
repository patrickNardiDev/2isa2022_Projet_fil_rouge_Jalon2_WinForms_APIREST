using DAL.Repository.Interface;
using DAL.Session.Interface;
using Dapper;
using Domain.Entities;
using Domain.Exeption;


namespace DAL.Repository.Implementation.MariaDB;

internal class UserRepositoryMariaDB : IUserRepository
{
    //Connexion à la base de donnéees par injection de dépendance
    private readonly IDBSession db;

    public UserRepositoryMariaDB(IDBSession dBSession)
    {
        db = dBSession;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var myQuery = @"SELECT Id, Name, Firstname, Email, Password, IdSIRole
                        FROM users";
        var result = await db.Connection.QueryAsync<User>(myQuery); //TODO QueryFirstOrDefaultAsync
        if (result is null) throw new NotFoundEntitiesExeption();
        return result;
    }

    #region GetByEmail
    /// <summary>
    /// Retourne un utilisateur existant ou nul suivant un email
    /// </summary>
    /// <param name="email">courriel</param>
    /// <returns>User{Id, Name, Firstname, Email, Password}</returns>
    /// <exception cref="Exception"></exception>
    public async Task<User> GetByEmailAsync(string email)
    {
        // 1- je définis ma query slq paramétrée suivant le package Nuget Dapper
        // 2- via la dBSession je réalise QueryFirstOrDefaultAsync avec comme paramètre l'email
        // 3- je retourne le résultat

        var result = new User();
        // 1 TestU , la query correspond ? non => SysException --> 
        var myQuery = @"SELECT Id, Name, Firstname, Email, Password, IdSIRole
                        FROM users
                        WHERE Email = @email";

        result = await db.Connection.QueryFirstOrDefaultAsync<User>(myQuery, new { email });
        if (result is null) throw new UnauthorizedAccessException();

        // Default value of return
        //if (result is null) throw new SysException("Aucune correspondance"); // 2 TestU, pas de correspondance => SysException
        return result; // 3 Test U, est-ce bien un user qui est renvoyé si correspondance avec un email ? => User

    }
    #endregion
}
