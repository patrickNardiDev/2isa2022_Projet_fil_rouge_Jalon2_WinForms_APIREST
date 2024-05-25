using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Gestion;

public class GestionEntrepriseController : APIBaseController
{
    private readonly IBLLGestionEntrepriseService _GestionEntrepriseService;

    public GestionEntrepriseController(IBLLGestionEntrepriseService entrepriseService)
    {
        _GestionEntrepriseService = entrepriseService;
    }

    #region Get
    //End point
    // URI api/gestionentreprise

    /// <summary>
    /// Fornit un GestionEntrepriseResponseDto avec l'ensemble des entreprise de maintenance
    /// </summary>
    /// <returns>GestionEntrepriseResponseDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        // 1- J'appelle la BLL est sa fonction GetGestionEntreprises()
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionEntrepriseService.GetGestionEntrepriseAsync();
        return Ok(reponse);
    }


    #endregion
}
