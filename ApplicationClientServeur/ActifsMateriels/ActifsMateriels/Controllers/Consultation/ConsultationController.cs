using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Consultation;

//TODO sup les try catch
public class ConsultationController : APIBaseController
{

    // Ajout de la dépendance concrète de IBLLConnexionServive par le constructeur via l'ID
    private readonly IBLLConsultationService _ConsultationService;

    public ConsultationController(IBLLConsultationService bLLconsultaionservice)
    {
        _ConsultationService = bLLconsultaionservice;
    }

    #region Get et Get par catégorie
    //End point
    // URI api/consultation

    /// <summary>
    /// Fournit un Dto de consultation avec l'ensemble des actifs matériels, 
    /// leurs catégories et si ils sont sous contrat de maintenance
    /// </summary>
    /// <returns>code 200 avec un ConsultationResponseDto ou 400 </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        // 1- J'appelle la BLL est sa fonction GetConsultationMateriels(categorie)
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _ConsultationService.GetConsultationMaterielsAsync();
        return Ok(reponse);
    }

    //End point
    // URI api/consultation

    /// <summary>
    /// Fournit un Dto de consultation avec les actifs matériels 
    /// en fonction de l'identifiant d'une catégorie
    /// </summary>
    /// <returns>code 200 avec un ConsultationResponseDto ou 400 </returns>
    [HttpGet("{idCategorie}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetByIdCategorieAsync([FromRoute] long idCategorie)
    {
        var reponse = await _ConsultationService.GetConsultationMaterielsAsync(idCategorie);
        return Ok(reponse);
    }
    #endregion
}
