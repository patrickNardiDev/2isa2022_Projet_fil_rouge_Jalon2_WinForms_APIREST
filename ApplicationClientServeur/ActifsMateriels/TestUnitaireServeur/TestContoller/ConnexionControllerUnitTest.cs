using API.Controllers.Connexion;
using BLL.Interface;
using Domain.DTO.Connexion;
using Domain.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Domain.Entities.Role;

namespace TestUnitaireServeur.TestContoller
{
    public class ConnexionControllerUnitTest
    {
        [Fact]
        public async void Login_With_DTORequestIsNull_Should_Throw_ValidationException()
        {
            // Arrange (arranger)
            ConnexionRequestDto connexionResquestDto = null;

            IBLLConnexionService bLLConnexionService = Mock.Of<IBLLConnexionService>();

            ConnexionController connexionController = new ConnexionController(bLLConnexionService);

            // Act (r�aliser)
            await Assert.ThrowsAsync<ValidationException>(async () => await connexionController.LoginAsync(connexionResquestDto));

            // Asset (v�rifier) => d�j� pr�sent dans le Assert ci-dessus
        }

        [Fact]
        public async void PostConnexion_With_TransianUserIsNull_Should_BadRequest()
        {
            // Arrange (arranger)
            ConnexionRequestDto connexionResquestDto = new ConnexionRequestDto()
            {
                Email = "115@amio.com",
                Password = "+1Mot@Passe",
            };

            IBLLConnexionService bLLConnexionService = Mock.Of<IBLLConnexionService>();

            ConnexionController connexionController = new ConnexionController(bLLConnexionService);

            UserTransit trUser = null;
            // Act (r�aliser)
            var result = await connexionController.LoginAsync(connexionResquestDto);

            // Asset (v�rifier)
            Assert.NotNull(result as BadRequestResult);
        }



        [Fact]
        public async void PostConnexion_Should_OKResponse()
        {
            // Arrange (arranger)
            // je construit le DTO de request
            ConnexionRequestDto connexionResquestDto = new()
            {
                Email = "115@amio.com",
                Password = "+1Mot@Passe",
            };
            UserTransit userTransit = new()
            {
                Id = 1,
                NameFormatted = "test",
                Role = RoleEnum.Gestion,
                AccessToken = "superToken",
                Email = "115@amio.com",
                IsConnected = true,
            };
            ConnexionResponseDto expectedReponse = new()
            {
                Id = userTransit.Id,
                NomPrenom = userTransit.NameFormatted,
                Role = userTransit.Role.GetValueOrDefault(),
                AccessToken = userTransit.AccessToken,
                IsConnected = userTransit.IsConnected,
            };

            // Je simule la BLL
            IBLLConnexionService bLLConnexionService = Mock.Of<IBLLConnexionService>();

            // je simule l'appel � ConnexionUser() du service 
            // var trUser = await _connexionServive.ConnexionUser(connexionResquestDto.Email, connexionResquestDto.Password);
            Mock.Get(bLLConnexionService)
                .Setup(cs => cs.ConnexionUserAsync(connexionResquestDto.Email, connexionResquestDto.Password))
                .ReturnsAsync(userTransit);
            //End BLL setup

            //Construction de l'instance � tester
            var connexionController = new ConnexionController(bLLConnexionService);


            // Act (r�aliser)
            var result = (await connexionController.LoginAsync(connexionResquestDto));

            // Asset (v�rifier)
            // 1- je v�rifi le test qui
            // le r�sultat ne doit pas �tre nulle et �tre du type OkObjectResult
            Assert.NotNull(result as OkObjectResult);

            // 2- je r�cup�r la r�ponse du test
            //var actualResponseDTO = ((OkObjectResult)result).Value as ConnexionResponseDto;
            //Assert.NotNull(actualResponseDTO);

            //// 3- je v�rifie que cette r�ponse et celle suppos�e sont �gales
            //Assert.Equal(expectedReponse.Role, actualResponseDTO.Role);
        }

    }
}