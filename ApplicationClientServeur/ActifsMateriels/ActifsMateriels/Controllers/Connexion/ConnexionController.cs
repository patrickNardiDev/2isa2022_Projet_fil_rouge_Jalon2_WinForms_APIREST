using BLL.Interface;
using Domain.DTO.Connexion;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Connexion;

public class ConnexionController : APIBaseController
{
    // Ajout de la dépendance concrète de IBLLConnexionServive par le constructeur via l'ID
    private readonly IBLLConnexionService _connexionServive;


    public ConnexionController(IBLLConnexionService bLLConnexionServive)
    {
        _connexionServive = bLLConnexionServive;
    }


    #region Swagger https://swagger.io/ - http://localhost:5294/swagger/index.html
    /// <summary>
    /// Connexion à la page sagger pour lister les routes de l'API.
    /// Login 115@amio.com et son mdp
    /// </summary>
    /// <param name="connexionRequestSwaggerDto">Dto de connexion swagger avec un username et un password</param>
    /// <returns>Code 201 ou 400</returns>
    [HttpPost("loginSwagger")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginDocSwagger([FromForm] ConnexionRequestSwaggerDto connexionRequestSwaggerDto)
    {

        var result = await _connexionServive.ConnexionUserAsync(connexionRequestSwaggerDto.username, connexionRequestSwaggerDto.password);
        //if (result is null) return BadRequest(); // 1 TestU
        //else return Ok(new { access_token = result });
        return Ok(new { access_token = result });
    }
    #endregion

    #region test connexion
    /// <summary>
    /// DEV Route. Test de connexion
    /// </summary>
    /// <param name="connexionRequestSwaggerDto"></param>
    /// <returns>Welcom</returns>
    [HttpGet("testco")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Testconnexion([FromForm] ConnexionRequestSwaggerDto connexionRequestSwaggerDto)
    {
        return Ok(new { Welcome = "Welcome" });
    }
    #endregion

    #region Login - POST
    //End point
    // URI api/Connexion/

    /// <summary>
    /// Connexion à l'API pour les utilisateurs 
    /// </summary>
    /// <param name="connexionResquestDto">Dto de connexion</param>
    /// <returns>Code 201 with UserTransit ou 400</returns>
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous] // Jwt
    public async Task<IActionResult> LoginAsync([FromBody] ConnexionRequestDto connexionResquestDto)
    {
        // 0- Le package validator vérifie le Dto et lève une exception si besoin
        ConnexionRequestDto requestDto = new(connexionResquestDto?.Email, connexionResquestDto?.Password);
        ConnexionRequestDtoValidator validator = new();
        var validatorResult = validator.Validate(requestDto, options => options.ThrowOnFailures()); // 1 TestU Throw ValidationException

        // 1- j'appelle la BLL et sa fonction ConnexionUser en découposant le Dto dans les arguments
        // 2- je crée le Dto de réponse
        // 3- je retourne le Dto de réponse sérialisé dans l'ActionResult OK

        var trUser = await _connexionServive.ConnexionUserAsync(connexionResquestDto.Email, connexionResquestDto.Password);

        if (trUser is null) return BadRequest(); // 2 TestU 

        var reponse = new ConnexionResponseDto() // 3 TestU
        {
            Id = trUser.Id,
            NomPrenom = trUser.NameFormatted,
            Role = (Domain.Entities.Role.RoleEnum)trUser.Role,
            AccessToken = trUser.AccessToken,
            IsConnected = trUser.IsConnected,
            SettingType = trUser.SettingType,
        };

        return Ok(reponse); // Testunitaire 
    }
    #endregion
}