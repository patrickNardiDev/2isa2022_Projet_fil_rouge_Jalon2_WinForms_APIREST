using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Gestion;

public class GestionCMaintenanceController : APIBaseController
{
    private readonly IBLLGestionCMaintenanceService _GestionCMaintenanceService;

    public GestionCMaintenanceController(IBLLGestionCMaintenanceService cMaintenanceService)
    {
        _GestionCMaintenanceService = cMaintenanceService;
    }

    #region Get
    //End point
    // URI api/gestioncmaintenance

    /// <summary>
    /// Fornit un GestionCMaintenanceResponseDto avec l'ensemble des contrats de maintenance
    /// </summary>
    /// <returns>GestionCMaintenanceResponseDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        // 1- J'appelle la BLL est sa fonction GetGestionCMaintenances()
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionCMaintenanceService.GetGestionCMaintenanceAsync();
        return Ok(reponse);
    }
    #endregion
}
