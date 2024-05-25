using Domain.DTO.Connexion;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using TestIntegrations.Fixtures;
using Xunit.Abstractions;

namespace TestIntegrations.TestController.Connexion;
public class ConnexionControllerTest : AbstractIntegrationTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public ConnexionControllerTest(APIWebApplicationFactory fixture, ITestOutputHelper testOutputHelper) : base(fixture)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task TestConnection_ShouldBeRetrieve_Welcome()
    {
        //Arrange
        //Act
        HttpResponseMessage response = await _client.GetAsync("api/connexion/testco");

        string stringResponse = await response.Content.ReadAsStringAsync();
        //Assert
        var a = 1;
        Assert.Contains("Welcome", stringResponse);
    }

    [Fact]
    public async Task Login_ShouldBeRetrieve_ConnexionResponseDto_whith_RoleGestion()
    {
        //Arrange 
        ConnexionRequestDto connexionRequestDto = new ConnexionRequestDto()
        {
            Email = "115@amio.com",
            Password = "+1Mot@Passe",
        };
        var myContent = JsonConvert.SerializeObject(connexionRequestDto);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _client.PostAsync("api/connexion", byteContent);
        var jsonString = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConnexionResponseDto>(jsonString);

        //Assert
        var ExpectedConnexionResponseDto = new ConnexionResponseDto()
        {
            Id = 96101,
            NomPrenom = "C. Patterson",
            Role = Domain.Entities.Role.RoleEnum.Gestion,
            IsConnected = true,
            Exeption = null,
        };
        Assert.Equal(ExpectedConnexionResponseDto.IsConnected, actualy.IsConnected);
        Assert.Equal(ExpectedConnexionResponseDto.Id, actualy.Id);
        Assert.Equal(ExpectedConnexionResponseDto.Exeption, actualy.Exeption);
        Assert.Equal(Convert.ToInt32(ExpectedConnexionResponseDto.Role), Convert.ToInt32(actualy.Role));
        Assert.Equal(ExpectedConnexionResponseDto.NomPrenom, actualy.NomPrenom);


    }

    [Fact]
    public async Task Login_ShouldBeRetrieve_ConnexionResponseDto_whith_RoleConsultation()
    {

        //Arrange 
        ConnexionRequestDto connexionRequestDto = new ConnexionRequestDto()
        {
            Email = "125@amio.com",
            Password = "+1Mot@Passe",
        };
        var myContent = JsonConvert.SerializeObject(connexionRequestDto);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _client.PostAsync("api/connexion", byteContent);
        var jsonString = await response.Content.ReadAsStringAsync();
        var actualy = JsonConvert.DeserializeObject<ConnexionResponseDto>(jsonString);

        //Assert
        var ExpectedConnexionResponseDto = new ConnexionResponseDto()
        {
            Id = 96111,
            NomPrenom = "P. Stéphanie",
            Role = Domain.Entities.Role.RoleEnum.Consultation,
            IsConnected = true,
            Exeption = null,
        };
        var a = 1;
        Assert.Equal(true, actualy.IsConnected);
        Assert.Equal(96111, actualy.Id);
        Assert.Equal(null, actualy.Exeption);
        Assert.Equal(1, Convert.ToInt32(actualy.Role));
        Assert.Equal("P. Stéphanie", actualy.NomPrenom);
    }

    [Fact]
    public async Task Login_ShouldBeRetrieve_ConnexionResponseDto_whith_NoRole()
    {
        //Arrange 

        ConnexionRequestDto connexionRequestDto = new ConnexionRequestDto()
        {
            Email = "555@amio.com",
            Password = "+1Mot@Passe",
        };
        var myContent = JsonConvert.SerializeObject(connexionRequestDto);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _client.PostAsync("api/connexion", byteContent);
        var jsonString = await response.Content.ReadAsStringAsync();
        ConnexionResponseDto actualy = null;
        try
        {
            actualy = JsonConvert.DeserializeObject<ConnexionResponseDto>(jsonString);
        }
        catch (Exception ex)
        {
            _testOutputHelper.WriteLine(jsonString);
        }

        //Assert
        var ExpectedConnexionResponseDto = new ConnexionResponseDto()
        {
            Id = 96128,
            NomPrenom = "D. DUNORD",
            Role = Domain.Entities.Role.RoleEnum.noRole,
            IsConnected = false,
            Exeption = null,
        };

        var a = 1;
        Assert.Equal(ExpectedConnexionResponseDto.IsConnected, actualy.IsConnected);
        Assert.Equal(ExpectedConnexionResponseDto.Id, actualy.Id);
        Assert.Equal(ExpectedConnexionResponseDto.Exeption, actualy.Exeption);
        Assert.Equal(Convert.ToInt32(ExpectedConnexionResponseDto.Role), Convert.ToInt32(actualy.Role));
        Assert.Equal(ExpectedConnexionResponseDto.NomPrenom, actualy.NomPrenom);
    }

    [Fact]
    public async Task Login_WithFalseLogin_ShouldBeRetrieve_FluentValidationBadRequest401()
    {
        //Arrange 
        ConnexionRequestDto connexionRequestDto = new ConnexionRequestDto()
        {
            Email = "lol@lol.com",
            Password = "1234567890",
        };
        var myContent = JsonConvert.SerializeObject(connexionRequestDto);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _client.PostAsync("api/connexion", byteContent);

        var jsonString = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(jsonString);

        //Assert
        Assert.Equal(response?.StatusCode, HttpStatusCode.Unauthorized);


    }
}
