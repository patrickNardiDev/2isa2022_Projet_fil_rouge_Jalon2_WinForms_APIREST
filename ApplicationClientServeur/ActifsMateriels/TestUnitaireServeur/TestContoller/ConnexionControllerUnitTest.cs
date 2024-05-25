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

            // Act (réaliser)
            await Assert.ThrowsAsync<ValidationException>(async () => await connexionController.LoginAsync(connexionResquestDto));

            // Asset (vérifier) => déjà présent dans le Assert ci-dessus
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
            // Act (réaliser)
            var result = await connexionController.LoginAsync(connexionResquestDto);

            // Asset (vérifier)
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

            // je simule l'appel à ConnexionUser() du service 
            // var trUser = await _connexionServive.ConnexionUser(connexionResquestDto.Email, connexionResquestDto.Password);
            Mock.Get(bLLConnexionService)
                .Setup(cs => cs.ConnexionUserAsync(connexionResquestDto.Email, connexionResquestDto.Password))
                .ReturnsAsync(userTransit);
            //End BLL setup

            //Construction de l'instance à tester
            var connexionController = new ConnexionController(bLLConnexionService);


            // Act (réaliser)
            var result = (await connexionController.LoginAsync(connexionResquestDto));

            // Asset (vérifier)
            // 1- je vérifi le test qui
            // le résultat ne doit pas être nulle et être du type OkObjectResult
            Assert.NotNull(result as OkObjectResult);

            // 2- je récupèr la réponse du test
            //var actualResponseDTO = ((OkObjectResult)result).Value as ConnexionResponseDto;
            //Assert.NotNull(actualResponseDTO);

            //// 3- je vérifie que cette réponse et celle supposée sont égales
            //Assert.Equal(expectedReponse.Role, actualResponseDTO.Role);
        }

    }
}