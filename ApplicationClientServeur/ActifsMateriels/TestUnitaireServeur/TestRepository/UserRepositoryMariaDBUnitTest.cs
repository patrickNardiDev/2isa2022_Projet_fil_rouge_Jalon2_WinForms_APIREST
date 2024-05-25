namespace TestUnitaireServeur.TestRepository;

public class UserRepositoryMariaDBUnitTest
{
    //[Fact]
    //public async void GetByEmailAsync_With_EmailNotInDatabase_Should_Throw_SysException()
    //{
    //    // Arrange (arranger)
    //    // je simule la session
    //    IDBSession _db = Mock.Of<IDBSession>();
    //    // je simule la propriété connexion de IDBSession
    //    IDbConnection _cn = Mock.Of<IDbConnection>();
    //    Mock.Get(_db).Setup(db => db.Connection).Returns(_cn);

    //    // je définis l'email non présent dans la Database
    //    string email = "test@amio.com";

    //    // je simule la requête à réaliser
    //    var myQuery = @"SELECT Id, Name, Firstname, Email, Password, IdSIRole
    //                    FROM users
    //                    WHERE Email = @email";

    //    // je simule l'appel au SGBD MariaDB avec l'ORM Dapper
    //    Mock.Get(_cn)
    //        .Setup(dbc => dbc.QueryFirstOrDefaultAsync<User>(
    //                myQuery,
    //                //new { email },
    //                It.Is<object>(obj => true),
    //                null, null, null))
    //        .ReturnsAsync(new User());

    //    // je construit l'instance de classe
    //    var Respository = new UserRepositoryMariaDB(_db);

    //    // Act (réaliser)
    //    // je réalise et vérifie la levé de l'exception de type SysException
    //    await Assert.ThrowsAsync<SysException>(async () => await Respository.GetByEmailAsync(email));

    //    // Asset (vérifier) ci-dessus
    //}
}


