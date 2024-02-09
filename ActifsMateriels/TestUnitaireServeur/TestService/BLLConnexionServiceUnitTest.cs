using BLL.Implementation;
using BLL.Interface;
using DAL;
using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace TestUnitaireServeur.TestService;

public class BLLConnexionServiceUnitTest
{
    /// <summary>
    /// Test si la vérification du résutat du service Bcrypt lève une exception
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_VerifiedBCryptIsFalse_Should_Throw_UnauthorizeException()
    {
        // Arrange (arranger)
        // je simule unit of work et la configuration
        IUOW _dbContext = Mock.Of<IUOW>();
        IConfiguration _configuration = Mock.Of<IConfiguration>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je définie les valeurs d'entrée de la function à tester
        var email = "999@amio.com";
        var password = "+1Mot@Passe"; //TODO très génant le couple email+mdp d'un vrai utilisateur dans une fonction public ???
        // je définie le résultat attendu
        var user = new User()
        {
            id = 96129,
            name = "PARIS",
            firstname = "Paris",
            email = email,
            password = "no correct password",
            idSIRole = 2,
        };

        // je simule var user = await _dbContext.User.GetByEmailAsync(email);
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(email)).ReturnsAsync(user);
        bool exceptedVerified = _bCrypService.Verify(password, user.password);


        // construction de l'instance à tester
        var connexonService = new BLLConnexionService(_dbContext, _configuration, _bCrypService);

        // Act (réaliser)
        await Assert.ThrowsAsync<UnauthorizeException>(async () => await connexonService.ConnexionUserAsync(email, password));

        // Asset (vérifier) => Vérifié ci-dessus
    }

    /// <summary>
    /// test si l'utilisateur avec un couple mail+mdp sans rôle retourne un UserTransit sans rôle
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_RoleNull_Should_UserTransitWithRoleNoRole()
    {
        // Arrange (arranger)
        // je simule unit of work et la configuration
        IUOW _dbContext = Mock.Of<IUOW>();
        IConfiguration _configuration = Mock.Of<IConfiguration>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "999@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96129,
            name = "Paris",
            firstname = "Paris",
            email = "999@amio.com",
            password = "+1Mot@Passe",
            idSIRole = null,
            //idSIRole = RoleValue is null ? 3 : Convert.ToInt32(RoleValue),
        };


        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        var connexonServiceMock = new Mock<BLLConnexionService>(_dbContext, _configuration, _bCrypService) { CallBase = true };
        connexonServiceMock.Setup(cs => cs.GenerateJwtToken(userExpected.id.ToString(), It.IsAny<List<string>>()))
            .Returns("SuperToken");

        // construction de l'instance à tester
        var connexionService = connexonServiceMock.Object;

        // Act (réaliser)
        var resultActual = await connexionService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        Assert.Equal((Role.RoleEnum)userExpected.idSIRole, resultActual.Role);
    }

    /// <summary>
    /// test si le role de l'utilisateur est dans la liste des rôlr alors le user transit à le même rôle
    /// </summary>
    /// <param name="RoleValue">tupe de rôle</param>
    /// <param name="emailValue">courriel</param>
    /// <param name="passwordValue">mot de passe</param>
    [Theory]
    [InlineData(Role.RoleEnum.noRole, "111@amio.com", "+1Mot@Passe")]
    [InlineData(Role.RoleEnum.Consultation, "120@amio.com", "+1Mot@Passe")]
    [InlineData(Role.RoleEnum.Gestion, "115@amio.com", "+1Mot@Passe")]
    public async void ConnexionUser_With_RoleInEnumRole_Should_UserTransitWithRoleInEnumRole(Role.RoleEnum RoleValue, string emailValue, string passwordValue)
    {
        // Arrange (arranger)
        // je simule unit of work et la configuration
        IUOW _dbContext = Mock.Of<IUOW>();
        IConfiguration _configuration = Mock.Of<IConfiguration>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "120@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96106,
            name = "Marc",
            firstname = "Dimars",
            email = emailInputValue,
            password = passwordInputValue,
            idSIRole = Convert.ToInt32(RoleValue),
        };

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        var connexonServiceMock = new Mock<BLLConnexionService>(_dbContext, _configuration, _bCrypService) { CallBase = true };
        connexonServiceMock.Setup(cs => cs.GenerateJwtToken(userExpected.id.ToString(), It.IsAny<List<string>>()))
            .Returns("SuperToken");

        // construction de l'instance à tester
        var connexionService = connexonServiceMock.Object;

        // Act (réaliser)
        var resultActual = await connexionService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        Assert.Contains(RoleValue.ToString(), string.Join(",", Role._List.ToList()));
    }

    /// <summary>
    /// test si un role non compris dans la liste des rôle renvoie un UserTransit avec le rôle noRole
    /// </summary>
    /// <param name="RoleValue">valeur du rôle</param>
    /// <param name="emailValue">courriel</param>
    /// <param name="passwordValue">password</param>
    [Theory]
    [InlineData("0", "111@amio.com", "+1Mot@Passe")]
    [InlineData("4", "112@amio.com", "+1Mot@Passe")]
    //[InlineData(null, "113@amio.com", "+1Mot@Passe")]
    public async void ConnexionUser_With_RoleNotInEnumRole_Should_UserTransitWithRoleNoRole(string RoleValue, string emailValue, string passwordValue)
    {
        // Arrange (arranger)
        // je simule unit of work et la configuration
        IUOW _dbContext = Mock.Of<IUOW>();
        IConfiguration _configuration = Mock.Of<IConfiguration>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "120@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96106,
            name = "Marc",
            firstname = "Dimars",
            email = emailInputValue,
            password = passwordInputValue,
            idSIRole = string.Join(",", Role._ListNumber.ToList()).Contains(RoleValue) ? Convert.ToInt32(RoleValue) : Convert.ToInt32(Role.RoleEnum.noRole),
        };

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        var connexonServiceMock = new Mock<BLLConnexionService>(_dbContext, _configuration, _bCrypService) { CallBase = true };
        connexonServiceMock.Setup(cs => cs.GenerateJwtToken(userExpected.id.ToString(), It.IsAny<List<string>>()))
            .Returns("SuperToken");

        // construction de l'instance à tester
        var connexionService = connexonServiceMock.Object;

        // Act (réaliser)
        var resultActual = await connexionService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        Assert.Equal((Role.RoleEnum)userExpected.idSIRole, resultActual.Role);
    }

    /// <summary>
    /// test si l'utilisateur à un rôle gestion, le UserTransit résultant a les rôle Admin et User
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_RoleGestion_Should_UserTransitWithRoleAdminAndUserInToken()
    {
        // Arrange (arranger)
        // je simule unit of work et le service de de BCrypt
        IUOW _dbContext = Mock.Of<IUOW>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je simule le fichier de configuration appsetings.json par un dictionary 
        var inMemorySettings = new Dictionary<string, string> {
            {"JwToken:JwtKey", "Henri Michaux : Cette habitude, dis-je, je l'ai justement gardée, et jusqu'aujourd'hui gardée secrète"},
            {"JwToken:JwtIssuer", "https://localhost:5294"},
            {"JwToken:JwtExpireDays","1" },
        };
        // j'injecte cet nouvelle implémentation 
        IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "115@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96101,
            name = "Patterson",
            firstname = "Cooper",
            email = "115@amio.com",
            password = "+1Mot@Passe",
            idSIRole = 2, // => Excepted value "Admin", "User"
        };
        // je simule le rélustat attendu dans le token
        var resultExcepted = new List<string>() { "Admin", "User" }; //TODO correct ??? ou l'obtenir avec le userExpected et décomposer le token ???

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        var a = Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        // construction de l'instance à tester
        var connexonService = new BLLConnexionService(_dbContext, _configuration, _bCrypService);

        // Act (réaliser)
        var resultActual = await connexonService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        // je técupère la liste des roles dans le token par la classe JwtSecurityTokenHandler https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytokenhandler?view=msal-web-dotnet-latest
        var ActualToken = resultActual.AccessToken;
        var Handler = new JwtSecurityTokenHandler();
        var HandlerToken = Handler.ReadJwtToken(ActualToken) as JwtSecurityToken;
        var RolesActualToken = (HandlerToken.Claims.Where(c => c.Type.Contains("role")).ToList()).Select(c => c.Value).ToList();
        // func lamba pour tester les correspondance engre Actual et Excepted
        var test = () =>
        {
            bool result = false;
            int compt = 0;
            foreach (var roleActual in RolesActualToken)
            {
                if (!resultExcepted.Contains(roleActual))
                    result = false;
                else
                {
                    result = true;
                    compt++;
                }
            }
            if (compt != RolesActualToken.Count && RolesActualToken.Count != resultExcepted.Count)
                return false;
            else return true;
        };

        Assert.True(test());
    }


    /// <summary>
    /// test si l'utilisateur à un rôle consultation, le UserTransit résultant a les rôle User
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_RoleConsultation_Should_UserTransitWithRoleUserInToken()
    {
        // Arrange (arranger)
        // je simule unit of work et le service de de BCrypt
        IUOW _dbContext = Mock.Of<IUOW>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je simule le fichier de configuration appsetings.json par un dictionary 
        var inMemorySettings = new Dictionary<string, string> {
            {"JwToken:JwtKey", "Henri Michaux : Cette habitude, dis-je, je l'ai justement gardée, et jusqu'aujourd'hui gardée secrète"},
            {"JwToken:JwtIssuer", "https://localhost:5294"},
            {"JwToken:JwtExpireDays","1" },
        };
        // j'injecte cet nouvelle implémentation 
        IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "120@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96106,
            name = "Marc",
            firstname = "Dimars",
            email = "120@amio.com",
            password = "+1Mot@Passe",
            idSIRole = 1, // => Excepted value "Admin", "User"
        };
        // je simule le rélustat attendu dans le token
        var resultExcepted = new List<string>() { "User" }; //TODO correct ??? ou l'obtenir avec le userExpected et décomposer le token ???

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        var a = Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        // construction de l'instance à tester
        var connexonService = new BLLConnexionService(_dbContext, _configuration, _bCrypService);

        // Act (réaliser)
        var resultActual = await connexonService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        // je técupère la liste des roles dans le token par la classe JwtSecurityTokenHandler https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytokenhandler?view=msal-web-dotnet-latest
        var ActualToken = resultActual.AccessToken;
        var Handler = new JwtSecurityTokenHandler();
        var HandlerToken = Handler.ReadJwtToken(ActualToken) as JwtSecurityToken;
        var RolesActualToken = (HandlerToken.Claims.Where(c => c.Type.Contains("role")).ToList()).Select(c => c.Value).ToList();
        // func lamba pour tester les correspondance engre Actual et Excepted
        var test = () =>
        {
            bool result = false;
            int compt = 0;
            foreach (var roleActual in RolesActualToken)
            {
                if (!resultExcepted.Contains(roleActual))
                    result = false;
                else
                {
                    result = true;
                    compt++;
                }
            }
            if (compt != RolesActualToken.Count && RolesActualToken.Count != resultExcepted.Count)
                return false;
            else return true;
        };

        Assert.True(test());
    }

    /// <summary>
    /// test si l'utilisateur n'a pas de rôle, le UserTransit résultant a les rôle noRole
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_NoRole_Should_UserTransitWithNoRoleInToken()
    {
        // Arrange (arranger)
        // je simule unit of work et le service de de BCrypt
        IUOW _dbContext = Mock.Of<IUOW>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je simule le fichier de configuration appsetings.json par un dictionary 
        var inMemorySettings = new Dictionary<string, string> {
            {"JwToken:JwtKey", "Henri Michaux : Cette habitude, dis-je, je l'ai justement gardée, et jusqu'aujourd'hui gardée secrète"},
            {"JwToken:JwtIssuer", "https://localhost:5294"},
            {"JwToken:JwtExpireDays","1" },
        };
        // j'injecte cet nouvelle implémentation 
        IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "999@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp
        var userExpected = new User()
        {
            id = 96129,
            name = "Paris",
            firstname = "Paris",
            email = "999@amio.com",
            password = "+1Mot@Passe",
            idSIRole = 0, // => Excepted value "Admin", "User"
        };
        // je simule le rélustat attendu dans le token
        var resultExcepted = new List<string>() { }; //TODO correct ??? ou l'obtenir avec le userExpected et décomposer le token ???

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        var a = Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        // construction de l'instance à tester
        var connexonService = new BLLConnexionService(_dbContext, _configuration, _bCrypService);

        // Act (réaliser)
        var resultActual = await connexonService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        // je técupère la liste des roles dans le token par la classe JwtSecurityTokenHandler https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytokenhandler?view=msal-web-dotnet-latest
        var ActualToken = resultActual.AccessToken;
        var Handler = new JwtSecurityTokenHandler();
        var HandlerToken = Handler.ReadJwtToken(ActualToken) as JwtSecurityToken;
        var RolesActualToken = (HandlerToken.Claims.Where(c => c.Type.Contains("role")).ToList()).Select(c => c.Value).ToList();
        // func lamba pour tester les correspondance engre Actual et Excepted
        var test = () =>
        {
            bool result = false;
            int compt = 0;
            foreach (var roleActual in RolesActualToken)
            {
                if (!resultExcepted.Contains(roleActual))
                    result = false;
                else
                {
                    result = true;
                    compt++;
                }
            }
            if (compt != RolesActualToken.Count && RolesActualToken.Count != resultExcepted.Count)
                return false;
            else return true;
        };

        Assert.True(test());
    }

    /// <summary>
    /// test si un couple couriel+mdp donne un UserTransitCorrespondant
    /// </summary>
    [Fact]
    public async void ConnexionUser_With_SpecifiedMailPasswork_Should_UserTransitCorrectAttributs()
    {
        // Arrange (arranger)
        // je simule unit of work et le service de de BCrypt
        IUOW _dbContext = Mock.Of<IUOW>();
        IBCrypService _bCrypService = Mock.Of<IBCrypService>();

        // je simule le fichier de configuration appsetings.json par un dictionary 
        var inMemorySettings = new Dictionary<string, string> {
            {"JwtKey", "Henri Michaux : Cette habitude, dis-je, je l'ai justement gardée, et jusqu'aujourd'hui gardée secrète"},
            {"JwtIssuer", "https://localhost:5294"},
            {"JwtExpireDays","1" },
        };
        // j'injecte cet nouvelle implémentation 
        IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        // je définie les valeurs d'entrée de la function à tester
        var emailInputValue = "117@amio.com";
        var passwordInputValue = "+1Mot@Passe";
        // je définie le résultat attendu, utilisateur correspondant au couple email+mdp pour le mock de GetByEmailAsync()
        var userExpected = new User()
        {
            id = 96103,
            name = "Wallace",
            firstname = "Odysseus",
            email = emailInputValue,
            password = passwordInputValue,
            idSIRole = 2,
        };
        // je simule le résultat attendu
        var UserTransitExcepted = new UserTransit()
        {
            Id = 96103,
            NameFormatted = "O. Wallace",
            Email = emailInputValue,
            Role = Role.RoleEnum.Gestion,
            AccessToken = "SuperToken",
            IsConnected = true,
        };

        // je simule
        Mock.Get(_dbContext).Setup(_bd => _bd.User.GetByEmailAsync(emailInputValue)).ReturnsAsync(userExpected);
        Mock.Get(_bCrypService).Setup(_db => _db.Verify(passwordInputValue, userExpected.password)).Returns(true);

        var connexonServiceMock = new Mock<BLLConnexionService>(_dbContext, _configuration, _bCrypService) { CallBase = true };
        connexonServiceMock.Setup(cs => cs.GenerateJwtToken(userExpected.id.ToString(), It.IsAny<List<string>>()))
            .Returns("SuperToken");

        // construction de l'instance à tester
        var connexionService = connexonServiceMock.Object;

        // Act (réaliser)
        var resultActual = await connexionService.ConnexionUserAsync(emailInputValue, passwordInputValue);

        // Asset (vérifier)
        Assert.Equivalent(UserTransitExcepted, resultActual);

    }
}
