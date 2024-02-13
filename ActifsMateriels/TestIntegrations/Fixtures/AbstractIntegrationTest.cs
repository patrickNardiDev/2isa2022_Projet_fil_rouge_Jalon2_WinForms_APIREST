using Dapper;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

//[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace TestIntegrations.Fixtures;


internal class DBSessionMariaDBTest : IDisposable
{
    public IDbConnection Connection { get; private set; }

    public IDbTransaction Transaction { get; set; }


    public DBSessionMariaDBTest(string connString)
    {
        Connection = new MySqlConnection(connString);
        Connection.Open();
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}

public abstract class AbstractIntegrationTest : IClassFixture<APIWebApplicationFactory>
{
    internal static AuthenticationHeaderValue TokenRoleGestion { get; set; }
    internal static AuthenticationHeaderValue TokenRoleConsultation { get; set; }
    internal static AuthenticationHeaderValue TokenNoRole { get; set; }
    internal static AuthenticationHeaderValue TokenWithoutRole { get; set; }

    protected readonly HttpClient _client;

    private static int countClass = 0;
    private static int countWithoutToken = 0;
    private static int countNoToken = 0;
    private static int countUserToken = 0;
    private static int countAdminToken = 0;
    private static object _lockerClass = new object();
    private static object _lockerWithoutToken = new object();
    private static object _lockerNoToken = new object();
    private static object _lockerUserToken = new object();
    private static object _lockerAdminToken = new object();

    private readonly DBSessionMariaDBTest _db;
    protected static APIWebApplicationFactory _fixture;
    public AbstractIntegrationTest(APIWebApplicationFactory fixture)
    {
        /// Http client d'appel de l'API
        _client = fixture.CreateClient();
        _fixture = fixture;

        // je récupère le string de connexion dans le fichier appsettings.Integrations.json
        var connectionString = fixture.Configuration.GetSection("DBConnection:ConnectionString").Value;

        // j'instancie la connexion et je l'ouvre
        _db = new DBSessionMariaDBTest(connectionString);
        //Instanciation unique de la base de donnée par un bouble Lock
        if (countClass == 0)
        {
            lock (_lockerClass)
            {
                if (countClass == 0)
                {
                    DropCreateTablesInDatabase();
                    DeleteAllEmenetsInDatabase();
                    AddAllElementsInDatabase();
                    countClass = 1;
                }
            }
        }
    }

    /// <summary>
    /// Connection to MariaDB Database
    /// </summary>
    public static void HeaderWithoutToken(HttpClient client)
    {
        if (countWithoutToken == 0)
        {
            lock (_lockerWithoutToken)
            {
                if (countWithoutToken == 0)
                {
                    TokenWithoutRole = new AuthenticationHeaderValue("Bearer", "");
                    client.DefaultRequestHeaders.Authorization = TokenWithoutRole;
                    countWithoutToken = 1;
                }
            }
        }
        else
        {
            client.DefaultRequestHeaders.Authorization = TokenWithoutRole;
        }
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

    }

    public static void HeaderWithTokenRoleUser(HttpClient client)
    {
        if (countUserToken == 0)
        {
            lock (_lockerUserToken)
            {
                if (countUserToken == 0)
                {
                    var mytokenUser = GenerateJwtToken("96111", new List<string>() { "User" });
                    TokenRoleConsultation = new AuthenticationHeaderValue("Bearer", $"{mytokenUser}");
                    client.DefaultRequestHeaders.Authorization = TokenRoleConsultation;
                    countUserToken = 1;
                }
            }
        }
        else
        {
            client.DefaultRequestHeaders.Authorization = TokenRoleConsultation;
        }
        //var token = GenerateJwtToken("96111", new List<string>() { "User" });
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
    }

    public static void HeaderWithTokenRoleUserAdmin(HttpClient client)
    {
        if (countAdminToken == 0)
        {
            lock (_lockerAdminToken)
            {
                if (countAdminToken == 0)
                {
                    var mytokenadmin = GenerateJwtToken("96101", new List<string>() { "Admin", "User" });
                    TokenRoleGestion = new AuthenticationHeaderValue("Bearer", $"{mytokenadmin}");
                    client.DefaultRequestHeaders.Authorization = TokenRoleGestion;
                    countAdminToken = 1;
                }
            }
        }
        else
        {
            client.DefaultRequestHeaders.Authorization = TokenRoleGestion;
        }
        //var token = GenerateJwtToken("96101", new List<string>() { "Admin", "User" });
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
    }

    public static void HeaderWithTokenNoRole(HttpClient client)
    {
        if (countNoToken == 0)
        {
            lock (_lockerNoToken)
            {
                if (countNoToken == 0)
                {
                    var mytokennorole = GenerateJwtToken("96128", new List<string>() { });
                    TokenNoRole = new AuthenticationHeaderValue("Bearer", $"{mytokennorole}");
                    client.DefaultRequestHeaders.Authorization = TokenNoRole;
                    countNoToken = 1;
                }
            }
        }
        else
        {
            client.DefaultRequestHeaders.Authorization = TokenNoRole;
        }
        //var token = GenerateJwtToken("96128", new List<string>() { });
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
    }

    public static void HeaderTokenNull(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = null;
    }

    /// <summary>
    /// Copy of methode to namespace BLL.Implementation
    /// </summary>
    /// <param name="idUser"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public static string GenerateJwtToken(string idUser, List<string> roles) // virtualisée pour être Mockée dans les TestU
    {
        //Add User Infos
        var claims = new List<Claim>(){
               new Claim(JwtRegisteredClaimNames.Sub, idUser), //ID User (Subject)
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //ID Token
               new Claim(ClaimTypes.NameIdentifier, idUser) //ID User (NameIdentifier)  == (Subject)
           };


        //Add Roles
        roles.ForEach(role =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        });

        var a = _fixture.Configuration.GetSection("JwtKey").Value;
        //Signing Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_fixture.Configuration.GetSection("JwToken:JwtKey").Value)); //TODO bug testUnitaire lors du Mock de GenerateJwtToken();
        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"])); //TODO bug testUnitaire lors du Mock de GenerateJwtToken();

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        //Expiration time
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_fixture.Configuration.GetSection("JwToken:JwtExpireDays").Value));
        //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        //Create JWT Token Object
        var token = new JwtSecurityToken(
            //_configuration["JwtIssuer"],
            _fixture.Configuration.GetSection("JwToken:JwtIssuer").Value,
            //_configuration["JwtIssuer"],
            _fixture.Configuration.GetSection("JwToken:JwtIssuer").Value,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool DropCreateTablesInDatabase()
    {
        bool result;
        string sFilePathAndName = "./scriptDropAndCreateTablesAndView.sql";
        string queries = File.ReadAllText(sFilePathAndName);
        var value = _db.Connection.Execute(queries);
        if (value < 0) result = false;
        else result = true;
        return result;
    }

    public bool DeleteAllEmenetsInDatabase()
    {
        bool result;
        string sFilePathAndName = "./scriptDeleteData.sql";
        string queries = File.ReadAllText(sFilePathAndName);
        var value = _db.Connection.Execute(queries);
        if (value < 0) result = false;
        else result = true;
        return result;
    }


    public bool AddAllElementsInDatabase()
    {
        bool result;
        string sFilePathAndName = "./scriptInsertData.sql";
        string queries = File.ReadAllText(sFilePathAndName);
        var value = _db.Connection.Execute(queries);
        if (value <= 0) result = false;
        else result = true;
        return result;
    }
}
