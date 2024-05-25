using Domain.DTO.Gestion;
using Domain.Entities;
using Domain.ValueObjects;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using TestIntegrations.Fixtures;

namespace TestIntegrations.TestController.Gestion;

public class GestionMaterielControllerTest : AbstractIntegrationTest
{
    private List<long> _IdsUser = new List<long>() { 82001, 96101, 96102, 96103, 96106, 96111, 96118, 96128, 96129, 96130 },
                        _IdsCMaintenance = new List<long>() { 1, 2, 3, 4, 5, 6 },
                        _IdsCategorie = new List<long>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        _IdsMateriel = new List<long>() { 1, 2, 3, 4, 29 };
    private Random _Rand = new Random();




    #region properties and constructor
    private static int _count = 0;
    private static string myToken = string.Empty;
    private APIWebApplicationFactory _fixtureConnexion { get; set; }
    public GestionMaterielControllerTest(APIWebApplicationFactory fixture) : base(fixture)
    {
        _fixtureConnexion = fixture;
    }

    #endregion

    #region GetAll
    [Fact]
    public async Task GetAll_WithRoleUser_ShouldBe_Forbidden()
    {
        //Arrange
        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.GetAsync("api/gestionmateriel");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);

    }

    [Fact]

    public async Task GetAll_WithRoleAdmin_ShouldBe_ExactMaterielInfo_Id29()
    {
        //Arrange
        MaterielGestion ExpectedMaterielGestion_Id_29 = new MaterielGestion() // for GET
        {
            IdMat = 29,
            NomMat = "ItemTestGet",
            DateMiseEnService = DateTime.Parse("2023-12-27 20:44:10.000"),
            DateFinGarantie = null,
            IdUser = 82001,
            NomUser = "A. Vincent",
            IdMContrat = 6,
            NomContrat = "ContratTest2",
            Archive = false,
            LastModif = DateTime.Parse("2023-12-28 14:18:08.000")
        };
        //InitDatabaseTest();

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync("api/gestionmateriel");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        var a = 1;

        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        //Assert.Equal(actualy?.CatOfIdMat.Count(), 0);
        //Assert.Equal(actualy?.EnumCMaintenance.Count(), 6);
        //Assert.Equal(actualy?.EnumCategories.Count(), 5);
        //Assert.Equal(actualy?.EnumMaterielsGestion.Count(), 5);
        //Assert.Equal(actualy?.EnumUsersTransit.Count(), 4);
        //Assert.True(actualy.ErrorMessage is null);
        //Assert.True(actualy.Exp is null);
        //Assert.Equal(actualy.IdMat, -1);
        //Assert.Equal(actualy?.IenumMaterielCategories.Count(), 8);
        Assert.Equal(1, ExpectedMaterielGestion_Id_29.CompareTo(actualy.EnumMaterielsGestion.Where(x => x.IdMat == 29).ToList()[0]));
    }

    [Fact]
    public async Task GetAll_WithNoRole_ShouldBe_Forbidden()
    {
        //Arrange
        //Act
        HeaderWithTokenNoRole(_client);
        var response = await _client.GetAsync("api/gestionmateriel");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);

    }

    [Fact]
    public async Task GetAll_WithoutRole_ShouldBe_Unauthorized()
    {
        //Arrange
        //Act
        HeaderWithoutToken(_client);
        var response = await _client.GetAsync("api/gestionmateriel");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response?.StatusCode);
    }
    #endregion

    #region GetById
    [Fact]
    public async Task GetById_WithGoodValues_ShouldBe_ValuesExpected()
    {
        //Arrange
        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync($"api/gestionmateriel/materiel/29");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<Materiel>(jsonStringActual);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.OK);
        Assert.Equal(actualy.Id, 29);

    }

    [Fact]
    public async Task GetById_WithNegativeValues_ShouldBe_BadRequest()
    {
        //Arrange
        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync("api/gestionmateriel/materiel/-1");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("(select conv(hex(substr(table_name,1,6)),16,10) FROM information_schema.tables WHERE table_schema=database() ORDER BY table_name ASC limit 0,1)")]
    public async Task GetById_WithSQLValues_ShouldBe_NotFound(string sqlAttack)
    {
        //Arrange
        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync($"api/gestionmateriel/materiel/{sqlAttack}");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.BadRequest);
    }
    #endregion

    #region GetByIdCat
    [Fact]
    public async Task GetByIdCat_WithGoodValues_ShouldBe_OkResultAnd2Elements()
    {
        //Arrange
        //Act
        //Assert
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync($"api/gestionmateriel/categorie/8");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        var a = 1;

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.OK);
        Assert.True(actualy.EnumMaterielsGestion.Count() == 1);
    }

    [Fact]
    public async Task GetByIdCat_WithNegativeValues_ShouldBe_NotFound()
    {
        //Arrange
        //Act
        //Assert
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync($"api/gestionmateriel/categorie/-1");
        HeaderTokenNull(_client);
        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("(select conv(hex(substr(table_name,1,6)),16,10) FROM information_schema.tables WHERE table_schema=database() ORDER BY table_name ASC limit 0,1)")]
    public async Task GetByIdCat_WithSQLValues_ShouldBe_BadRequest(string sqlAttack)
    {
        //Arrange
        //Act
        //Assert
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.GetAsync($"api/gestionmateriel/categorie/{sqlAttack}");
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.BadRequest);
    }
    #endregion

    #region Post
    [Fact]
    public async Task Post_WithRoleAdminAndGoodValues_ShouldBe_ValuesExpected()
    {

        var requestDtoExceptedFixed = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = 96101,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = "TestPost",
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        var GetNewMaterial = await _client.GetAsync($"api/gestionmateriel/materiel/{actualy.IdMat}");
        var DeleteNewMaterial = await _client.DeleteAsync($"api/gestionmateriel/delete/{actualy.IdMat}");
        HeaderTokenNull(_client);

        var a = 1;
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        Assert.True(actualy.IdMat > _IdsMateriel.Max());
        Assert.Equal(HttpStatusCode.OK, GetNewMaterial.StatusCode);
        Assert.Equal(HttpStatusCode.OK, DeleteNewMaterial.StatusCode);

    }

    [Fact]
    public async Task Post_WithRoleAdmin_ErrorName_ShouldBe_OkResultWithMessageError()
    {
        //Arrange
        var requestDtoExceptedInError_1 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "", // error here
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedInError_1);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        Assert.True(actualy?.ErrorMessage.Equals("Vous devez nommer le matériel"));

    }

    [Fact]
    public async Task Post_WithRoleAdmin_ErrorNoCatégorie_ShouldBe_DtpResponseWithErrorUserNotNullAndText_02()
    {
        var requestDtoExceptedInError_2 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>(), // error here
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedInError_2);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        Assert.True(actualy?.ErrorMessage.Equals("Le matériel doit avoir au moins une catégorie"));

    }

    [Fact]
    public async Task Post_WithRoleAdmin_ErrorForenkey_ShouldBe_InternalServerError()
    {
        var requestDtoExceptedInError_3 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = 2024, // error here
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        var requestDtoExceptedInError_4 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = 2024, // error here
            NewListIdCategories = new List<long>() { 9, 10 },
        };

        var RequestDtoExceptedInErrors = new List<GestionMatrielRequestDto>() { requestDtoExceptedInError_3, requestDtoExceptedInError_4 };
        //Act & Assert
        foreach (var requestDtoExceptedInError in RequestDtoExceptedInErrors)
        {
            var myContent = JsonConvert.SerializeObject(requestDtoExceptedInError);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            HeaderWithTokenRoleUserAdmin(_client);
            var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
            HeaderTokenNull(_client);

            var jsonStringActual = await response.Content.ReadAsStringAsync();
            var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

            Assert.Equal(HttpStatusCode.InternalServerError, response?.StatusCode);
        }
    }

    [Fact]
    public async Task Post_WithRoleAdmin_ErrorCrudType_ShouldBe_BadRequest()
    {
        var requestDtoExceptedInError_5 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Read, // error here
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        var requestDtoExceptedInError_6 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Delete, // error here
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        var requestDtoExceptedInError_7 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Archive, // error here
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        var requestDtoExceptedInError_8 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify, // error here
            NewIdUser = null,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "test",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };

        var a = 1;
        var RequestDtoExceptedInErrors = new List<GestionMatrielRequestDto>() { requestDtoExceptedInError_5, requestDtoExceptedInError_6, requestDtoExceptedInError_7, requestDtoExceptedInError_8 };
        //Act & Assert
        foreach (var requestDtoExceptedInError in RequestDtoExceptedInErrors)
        {
            var myContent = JsonConvert.SerializeObject(requestDtoExceptedInError);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            HeaderWithTokenRoleUserAdmin(_client);
            var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
            HeaderTokenNull(_client);

            var jsonStringActual = await response.Content.ReadAsStringAsync();
            var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

            Assert.Equal(HttpStatusCode.BadRequest, response?.StatusCode);
        }
    }

    [Fact]
    public async Task Post_WithRoleUser_ShouldBe_Forbidden()
    {
        var requestDtoExceptedInError = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = 96128,
            NewDMService = null,
            NewDFGarantie = null,
            NewNomMat = "materielPostError",
            NewIdCMaintenance = null,
            NewListIdCategories = new List<long>() { 9, 10 },
        };
        //Arrange
        //Act
        var myContent = JsonConvert.SerializeObject(requestDtoExceptedInError);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUser(_client);
        var response = await _client.PostAsync("api/gestionmateriel/add", byteContent);
        HeaderTokenNull(_client);

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);

    }
    #endregion

    #region Put
    [Fact]
    public async Task Put_WithRoleAdminAndGoodValues_ShouldBe_ValuesExpected()
    {
        //Arrange
        //InitDatabaseTest();
        var requestDtoExceptedFixed = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify,
            OldIdMat = 1,
            NewIdUser = 96101,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = "TestPost",
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { 9, 10 },
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            //NewArchive
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PutAsync("api/gestionmateriel/update", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1, actualy.IdMat);
    }

    [Fact]
    public async Task Put_WithRoleAdmin_ErrorWithoutName_ShouldBe_OkResultWithMessageError()
    {
        //Arrange
        //InitDatabaseTest();
        var requestDtoExceptedFixed_1 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify,
            OldIdMat = 1,
            NewIdUser = 96111,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = "", // error here
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { 9, 10 },
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            //NewArchive
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed_1);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PutAsync("api/gestionmateriel/update", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        Assert.True(actualy?.ErrorMessage.Equals("Vous devez nommer le matériel"));

    }
    [Fact]
    public async Task Put_WithRoleAdmin_ErrorWithoutCategorie_ShouldBe_OkResultWithMessageError()
    {
        //Arrange
        var requestDtoExceptedFixed_2 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify,
            OldIdMat = 1,
            NewIdUser = 96111,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = "TestPost",
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { }, // error here
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            //NewArchive
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed_2);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PutAsync("api/gestionmateriel/update", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.OK, response?.StatusCode);
        Assert.True(actualy?.ErrorMessage.Equals("Le matériel doit avoir au moins une catégorie"));

    }

    [Fact]
    public async Task Put_WithRoleUser_ShouldBe_Forbidden()
    {
        //Arrange
        var requestDtoExceptedFixed_2 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify,
            OldIdMat = 1,
            NewIdUser = 96111,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = "TestPost",
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { }, // error here
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            //NewArchive
        };
        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed_2);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //Act
        //Assert
        HeaderWithTokenRoleUser(_client);
        var response = await _client.PutAsync("api/gestionmateriel/update", byteContent);
        HeaderTokenNull(_client);

        Assert.Equal(response.StatusCode, HttpStatusCode.Forbidden);
    }

    [Theory]
    [InlineData("(select conv(hex(substr(table_name,1,6)),16,10) FROM information_schema.tables WHERE table_schema=database() ORDER BY table_name ASC limit 0,1)")]
    public async Task Put_WithSQLValues_ShouldBe_InternalServerError(string sqlAttack) // taille du string
    {
        //Arrange
        var requestDtoExceptedFixed_2 = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Modify,
            OldIdMat = 36,
            NewIdUser = 96111,
            NewDMService = DateTime.Parse("2024-01-03T00:00:00"),
            NewDFGarantie = null,
            NewNomMat = sqlAttack,
            NewIdCMaintenance = 3,
            NewListIdCategories = new List<long>() { 1, 2 }, // error here
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            //NewArchive
        };
        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed_2);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //Act
        //Assert
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PutAsync("api/gestionmateriel/update", byteContent);
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
    #endregion

    #region Put Archive
    [Fact]
    public async Task PutArchive_WithRoleAdminAndGoodValues_ShouldBe_ValuesExpected()
    {
        //Arrange
        //InitDatabaseTest();
        var requestDtoExceptedFixed = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Archive,
            OldIdMat = 34,
            NewIdUser = 82001,
            NewDMService = DateTime.Parse("2022-01-03 00:00:00.000"),
            NewDFGarantie = DateTime.Parse("2024-01-03 00:00:00.000"),
            NewNomMat = "ItemTestPutArchive",
            NewIdCMaintenance = 1,
            NewListIdCategories = new List<long>() { 1 },
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000"),
            NewArchive = true,
        };

        var myContent = JsonConvert.SerializeObject(requestDtoExceptedFixed);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.PutAsync("api/gestionmateriel/archive", byteContent);
        var matDatabase = await _client.GetAsync("api/gestionmateriel/materiel/34");

        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        var jsonStringActualData = await matDatabase.Content.ReadAsStringAsync();
        var actualyData = JsonConvert.DeserializeObject<Materiel>(jsonStringActualData);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(requestDtoExceptedFixed.OldIdMat, actualy.IdMat);

        Assert.Equal(HttpStatusCode.OK, matDatabase.StatusCode);
        Assert.Equal(requestDtoExceptedFixed.OldIdMat, actualyData.Id);
        Assert.Equal(requestDtoExceptedFixed.NewIdUser, actualyData.IdUser);
        Assert.Equal(requestDtoExceptedFixed.NewDMService, actualyData.DateMiseEnService);
        Assert.Equal(requestDtoExceptedFixed.NewDFGarantie, actualyData.DateFinGarantie);
        Assert.Equal(requestDtoExceptedFixed.NewNomMat, actualyData.Nom);
        Assert.Equal(requestDtoExceptedFixed.NewIdCMaintenance, actualyData.IdMContrat);


        Assert.Equal(34, actualyData.Id);
        Assert.True(actualyData.Archive);

    }
    //TODO ici
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_Archived_WithRoleAdminAndGoodValues_ShouldBe_OKAndGetByIdNull()
    {
        MaterielGestion ExpectedMaterielGestion_Id_2 = new MaterielGestion() // for DELETE
        {
            IdMat = 2,
            NomMat = "ItemTestDelete",
            DateMiseEnService = DateTime.Parse("2021-02-04 00:00:00.000"),
            DateFinGarantie = DateTime.Parse("2024-02-04 00:00:00.000"),
            IdUser = 96101,
            NomUser = "C. Patterson",
            IdMContrat = 2,
            NomContrat = "ecr_27p_2021",
            Archive = true,
            LastModif = DateTime.Parse("2023-12-11 13:48:07.000")
        };
        //Arrange
        var dtoAddMat = new GestionMatrielRequestDto()
        {
            CRUDtype = CRUDtype.MyType.Add,
            NewIdUser = ExpectedMaterielGestion_Id_2.IdMat,
            NewDMService = ExpectedMaterielGestion_Id_2.DateMiseEnService,
            NewDFGarantie = ExpectedMaterielGestion_Id_2.DateFinGarantie,
            NewNomMat = ExpectedMaterielGestion_Id_2.NomMat,
            NewIdCMaintenance = ExpectedMaterielGestion_Id_2.IdMContrat,
            NewListIdCategories = new List<long>() { 2 },
        };

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var Mat = await _client.GetAsync($"api/gestionmateriel/materiel/2");

        var response = await _client.DeleteAsync($"api/gestionmateriel/delete/2");

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);

        var oldMat = await _client.GetAsync($"api/gestionmateriel/materiel/2");

        // je delete le MaterielDetruit de la table D_MATERIEL_DETRUIT pour éviter les erreurs de forenkeys lors de l'initialisation de la base
        // => implique création d'une route pour supprimer le nouveau materiel détuit de la table D_MATERIEL_DETRUIT => dette technique. Il aurait été plus judicieux de faire une colonne supprimer dans la table D_MATERIEL
        var nbmatdeleted = await _client.DeleteAsync($"api/gestionmateriel/delete/deleted/2");

        HeaderTokenNull(_client);
        HeaderWithTokenRoleUserAdmin(_client);

        //add material for rebase data
        var myContent = JsonConvert.SerializeObject(dtoAddMat);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var addMat = await _client.PostAsync("api/gestionmateriel/add", byteContent);
        HeaderTokenNull(_client);
        //Assert
        Assert.Equal(HttpStatusCode.OK, Mat.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, actualy.IdMat);
        Assert.Equal(HttpStatusCode.NotFound, oldMat.StatusCode);






    }
    [Fact]
    //[InlineData(4)]
    //[InlineData(29)]
    public async Task Delete_NotArchived_WithRoleAdminAndGoodValues_ShouldBe_ErrorMessageNotNull()
    {
        //Arrange

        //Act
        HeaderWithTokenRoleUserAdmin(_client);
        var response = await _client.DeleteAsync($"api/gestionmateriel/delete/4");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        var a = 1;

        Assert.Equal(4, actualy.IdMat);
        Assert.Equal($"Le matériel dont l'identifiant est 4, doit être archivé avant d'être détruit.", actualy.ErrorMessage);
        Assert.Equal(0, actualy.EnumCMaintenance.Count());
        Assert.Equal(0, actualy.EnumCMaintenance.Count());
        Assert.Equal(0, actualy.EnumMaterielsGestion.Count());
        Assert.Equal(0, actualy.EnumUsersTransit.Count());
        Assert.Equal(null, actualy.Exp);
        Assert.Equal(new List<Categorie>(), actualy.CatOfIdMat);
    }


    [Fact]
    public async Task Delete_WithRoleAdminAndNegativeValue_ShouldBe_BadRequest()
    {
        //Arrange
        HeaderWithTokenRoleUserAdmin(_client);
        //Act
        var response = await _client.DeleteAsync($"api/gestionmateriel/delete/-1");
        HeaderTokenNull(_client);

        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        var a = 1;
        // pas logique par rapport au code, erreur de ma part ???
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("L'identifiant matériel ne peut pas être négatif.", actualy.ErrorMessage);
        Assert.Equal(0, actualy.EnumCMaintenance.Count());
        Assert.Equal(0, actualy.EnumCMaintenance.Count());
        Assert.Equal(0, actualy.EnumMaterielsGestion.Count());
        Assert.Equal(0, actualy.EnumUsersTransit.Count());
        Assert.Equal(null, actualy.Exp);
        Assert.Equal(new List<Categorie>(), actualy.CatOfIdMat);
    }

    [Fact]
    public async Task Delete_WithRoleUser_ShouldBe_Forbidden()
    {
        //Arrange
        HeaderWithTokenRoleUser(_client);
        //Act
        var response = await _client.DeleteAsync("api/gestionmateriel/delete/4");
        HeaderTokenNull(_client);
        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);
        //Assert.Equal(response?.StatusCode, HttpStatusCode.Forbidden);//TODO pas la bonne exception
    }

    [Theory]
    [InlineData("(select conv(hex(substr(table_name,1,6)),16,10) FROM information_schema.tables WHERE table_schema=database() ORDER BY table_name ASC limit 0,1)")]
    public async Task Delete_WithSQLValues_ShouldBe_Forbidden(string sqlAttack)
    {
        //Arrange
        //InitDatabaseTest();

        HeaderWithTokenRoleUser(_client);
        //Act
        var response = await _client.DeleteAsync($"api/gestionmateriel/delete/{sqlAttack}");
        HeaderTokenNull(_client);
        var jsonStringActual = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<GestionMaterielResponseDto>(jsonStringActual);
        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response?.StatusCode);
    }
    #endregion
}
