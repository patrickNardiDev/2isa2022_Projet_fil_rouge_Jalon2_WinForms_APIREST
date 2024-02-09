using Domain.DTO.Consultation;
using Domain.ValueObjects;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using TestIntegrations.Fixtures;

//[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace TestIntegrations.TestController.Consultation;
public static class Extensions
{
    public static string RemoveWhiteSpaces(this string str)
    {
        return string.Concat(str.Where(c => !char.IsWhiteSpace(c)));
    }
}

public class ConsultationControllerTest : AbstractIntegrationTest
{
    private static int _count = 0;
    private static string myToken = string.Empty;

    private Stopwatch _timer = new Stopwatch();
    private APIWebApplicationFactory _fixtureConnexion { get; set; }
    public ConsultationControllerTest(APIWebApplicationFactory fixture) : base(fixture)
    {
        _fixtureConnexion = fixture;
    }

    [Fact]
    public async Task Get_With_RoleConsultation_ShouldBeRetrieve_ConsultationResponseDto_Whith_ExactMaterielInfo_Id33()
    {
        //Arrange
        //DeleteAllEmenetsInDatabase();
        //AddAllElementsInDatabase();
        var exceptedMaterielsInfos = new MaterielsInfos()
        {
            IdMat = 33,
            NomMat = "ItemTestGet",
            DateMiseEnService = DateTime.Parse("2024-01-03 00:00:00.000"),
            DateFinGarantie = null,
            IdUser = 96128,
            NomUser = "D. DUNORD",
            IdMContrat = 4,
            NomContrat = "mtpc_cpu_inter_iç",
            Contrat = "Maintenance",
            Archive = false,

        };


        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.GetAsync("api/consultation");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);
        //var matCatActualy = actualy.IenummaterielCategories.FirstOrDefault();

        //Assert.Collection(actualy,
        //    x => Assert.Equal(10, x.IenumCategories.Count()),
        //    x => Assert.Equal(5, x.IenumMaterielsInfos.Count()),
        //    x => Assert.Equal(8, x.IenummaterielCategories.Count()));
        var a = 1;
        Assert.Equal(exceptedMaterielsInfos.IdMat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdMat);
        Assert.Equal(exceptedMaterielsInfos.NomMat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomMat);
        Assert.Equal(exceptedMaterielsInfos.DateMiseEnService, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].DateMiseEnService);
        Assert.Equal(exceptedMaterielsInfos.DateFinGarantie, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].DateFinGarantie);
        Assert.Equal(exceptedMaterielsInfos.IdUser, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdUser);
        Assert.Equal(exceptedMaterielsInfos.NomUser, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomUser);
        Assert.Equal(exceptedMaterielsInfos.IdMContrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdMContrat);
        Assert.Equal(exceptedMaterielsInfos.NomContrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomContrat);
        Assert.Equal(exceptedMaterielsInfos.Contrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].Contrat);
        Assert.Equal(exceptedMaterielsInfos.Archive, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].Archive);
    }

    [Fact]
    public async Task Get_With_RoleGestion_ShouldBeRetrieve_ConsultationResponseDto_Whith_ExactMaterielInfo_Id33()
    {
        //Arrange
        var exceptedMaterielsInfos = new MaterielsInfos()
        {
            IdMat = 33,
            NomMat = "ItemTestGet",
            DateMiseEnService = DateTime.Parse("2024-01-03 00:00:00.000"),
            DateFinGarantie = null,
            IdUser = 96128,
            NomUser = "D. DUNORD",
            IdMContrat = 4,
            NomContrat = "mtpc_cpu_inter_iç",
            Contrat = "Maintenance",
            Archive = false,

        };
        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync("api/consultation");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);
        var a = 1;

        Assert.Equal(exceptedMaterielsInfos.IdMat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdMat);
        Assert.Equal(exceptedMaterielsInfos.NomMat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomMat);
        Assert.Equal(exceptedMaterielsInfos.DateMiseEnService, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].DateMiseEnService);
        Assert.Equal(exceptedMaterielsInfos.DateFinGarantie, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].DateFinGarantie);
        Assert.Equal(exceptedMaterielsInfos.IdUser, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdUser);
        Assert.Equal(exceptedMaterielsInfos.NomUser, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomUser);
        Assert.Equal(exceptedMaterielsInfos.IdMContrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].IdMContrat);
        Assert.Equal(exceptedMaterielsInfos.NomContrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].NomContrat);
        Assert.Equal(exceptedMaterielsInfos.Contrat, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].Contrat);
        Assert.Equal(exceptedMaterielsInfos.Archive, actualy.IenumMaterielsInfos.Where(x => x.IdMat == 33).ToList()[0].Archive);
    }

    [Fact]
    public async Task Get_With_NoRole_ShouldBeRetrieve_ConsultationResponseDtoNull()
    {
        //Arrange

        //Act
        HeaderWithTokenNoRole(_client);
        var response = await _client.GetAsync("api/consultation");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);
        //if (actualy == null)
        //    throw new Exception();//TODO Aseert à faire ???

        Assert.True(actualy is null);
    }

    [Fact]
    public async Task Get_WithOutRole_ShouldBeRetrieve_ConsultationResponseDtoNull()
    {
        //Arrange

        //Act
        HeaderWithoutToken(_client);
        var response = await _client.GetAsync("api/consultation");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);

        Assert.True(actualy is null);
    }

    [Fact]
    public async Task GetByIdCategorie_ShouldBeRetrieve_OneRows()
    {
        //InitDatabaseTest();
        //Arrange

        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.GetAsync($"api/consultation/8");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);

        var a = 1;
        //Assert
        Assert.Equal(1, actualy.IenumMaterielsInfos.Count());
    }

    [Fact]
    public async Task GetByIdCategorie_WithNgativeValue_ShouldBeRetrieve_FluentValidationNotFound()
    {
        //Arrange

        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.GetAsync($"api/consultation/-1");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response?.StatusCode);

    }

    // injection SQL : https://book.hacktricks.xyz/pentesting-web/sql-injection
    [Theory]
    [InlineData("(select 1 and row(1,1)>(select count(*),concat(CONCAT(@@VERSION),0x3a,floor(rand()*2))x from (select 1 union select 2)a group by x limit 1))")] // => donne une colonne avec 0, pas en erreur avec sgbd
    [InlineData("(select conv(hex(substr(table_name,1,6)),16,10) FROM information_schema.tables WHERE table_schema=database() ORDER BY table_name ASC limit 0,1)")]  //  => donne 75175940936773 pas en erreur avec sgbd 
    [InlineData("(SHOW TABLES;\r\nDESCRIBE table_name;\r\nSHOW TABLE STATUS from table_name;)")]
    public async Task GetByIdCategorie_ShouldBeRetrieve_FluentValidationBadRequest400(string idString)
    {
        //Arrange
        //InitDatabaseTest();
        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.GetAsync($"api/consultation/{idString}");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConsultationResponseDto>(jsonStringActual);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.BadRequest);

        //Assert.is
    }
}
