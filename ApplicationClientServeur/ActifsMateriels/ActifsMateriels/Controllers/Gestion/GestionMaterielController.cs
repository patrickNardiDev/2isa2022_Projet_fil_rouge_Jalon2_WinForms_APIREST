using BLL.Interface;
using Domain.DTO.Gestion;
using Domain.Exeption;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.DTO.Gestion.GestionMatrielRequestDto;

namespace API.Controllers.Gestion;

public class GestionMaterielController : APIBaseController
{
    private readonly IBLLGestionMaterielService _GestionMaterielService;

    public GestionMaterielController(IBLLGestionMaterielService gestionMaterielService)
    {
        _GestionMaterielService = gestionMaterielService;
    }

    #region Get et Get par catégorie
    //End point
    // URI api/gestionmateriel

    /// <summary>
    /// Fournit un GestionMaterielResponseDto avec l'ensemble des actifs matériels, 
    /// leurs catégories et si ils sont sous contrat de maintenance
    /// </summary>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        // 1- J'appelle la BLL est sa fonction GetGestionMateriels()
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.GetGestionMaterielsAsync();
        return Ok(reponse);
    }

    //End point
    // URI api/gestionmateriel/materiel/idmat

    /// <summary>
    /// fornit un Matériel en fonction de son identifiant
    /// et rétourne un GestionMaterielResponseDto 
    /// </summary>
    /// <returns>Materiel</returns>
    [HttpGet("materiel/{idMat}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long idMat)
    {
        //if (idMat <= 0)
        //    throw new NotFoundEntitiesExeption();
        // 1- J'appelle la BLL est sa fonction GetGestionMateriels()
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.GetMaterielByIdAsync(idMat);
        return Ok(reponse);
    }

    //End point
    // URI api/gestionmateriel/categorie/id

    /// <summary>
    /// Fournit un GestionMaterielResponseDto avec les actifs matériels 
    /// en fonction de l'identifint d'une catégorie
    /// </summary>
    /// <param name="idCategorie"> id de la catégorie</param>
    /// <returns>code 201 avec le GestionMaterielResponseDto</returns>
    /// <exception cref="SysException"></exception>
    [HttpGet("categorie/{idCategorie}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByIdCatAsync([FromRoute] long idCategorie)
    {

        //if (idCategorie <= 0)
        //    throw new NotFoundEntitiesExeption();
        // 1- J'appelle la BLL est sa fonction GetConsultationMateriels(idcategorie)
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.GetGestionMaterielsByCatAsync(idCategorie);
        return Ok(reponse);
    }


    #endregion

    #region Post
    //End point
    // URI api/gestionmateriel/add

    /// <summary>
    /// Ajoute un matériel avec sa selection de catégorie et rétourne un GestionMaterielResponseDto 
    /// comprenant l'id et les catégories du nouveau matériel
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostAsync([FromBody] GestionMatrielRequestDto gestionMatrielRequestDto)
    {
        // 0- Validation du Dto de requête par la bibliothèque FluentValidation
        GestionMatrielRequestDtoValidator validator = new();
        var validatorResult = validator.Validate(gestionMatrielRequestDto, options => options.ThrowOnFailures());

        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.PostGestionMaterielsAddAsync(gestionMatrielRequestDto);
        return Ok(reponse);
    }
    #endregion


    #region Put
    //End point
    // URI api/gestionmateriel/update

    /// <summary>
    /// Mise à jour d'un matériel suivant son identifiant gestionMatrielRequestDto.OldIdMat 
    /// et rétourne un GestionMaterielResponseDto comprenant l'id et les catégories du matériel modifié
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutAsync([FromBody] GestionMatrielRequestDto gestionMatrielRequestDto)
    {
        // 0- Validation du Dto de requête par la bibliothèque FluentValidation
        GestionMatrielRequestDtoValidator validator = new();
        var validatorResult = validator.Validate(gestionMatrielRequestDto, options => options.ThrowOnFailures());
        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.PutGestionMaterielsUpdateAsync(gestionMatrielRequestDto);
        return Ok(reponse);
    }
    #endregion

    #region Put Archive
    //End point
    // URI api/gestionmateriel/archive

    /// <summary>
    /// Archivage d'un matériel suivant son identifiant gestionMatrielRequestDto.OldIdMat
    /// et rétourne un GestionMaterielResponseDto comprenant l'id et les catégories du matériel modifié
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpPut("archive")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutArchiveAsync([FromBody] GestionMatrielRequestDto gestionMatrielRequestDto)
    {
        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        var reponse = await _GestionMaterielService.PutGestionMaterielsArchiveAsync(gestionMatrielRequestDto);
        return Ok(reponse);
    }
    #endregion

    #region Delete
    //End point
    // URI api/gestionmateriel/delete

    /// <summary>
    /// Supprime un matériel suivant son identifiant gestionMatrielRequestDto.OldIdMat
    /// et rétourne un GestionMaterielResponseDto comprenant le matériel supprimé
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpDelete("delete/{idmat}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long idmat)
    {
        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        //var reponse = await _GestionMaterielService.DeleteGestionMateriels(idmat);
        var reponse = await _GestionMaterielService.DeleteGestionMaterielsAsync(idmat);
        return Ok(reponse);
        //return Ok(reponse);
    }

    //End point
    // URI api/gestionmateriel/deleted

    /// <summary>
    /// Fornit un MatérielDeleted en fonction de son ancien identifiant dans la route
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpGet("deleted/{idmat}")]
    // [HttpDelete("deleted/{idmat}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetMatDeletedAsync([FromRoute] long idmat)
    {
        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        //var reponse = await _GestionMaterielService.DeleteGestionMateriels(idmat);
        var reponse = await _GestionMaterielService.GetMatDeletedAsync(idmat);
        return Ok(reponse);
        //return Ok(reponse);
    }


    //End point
    // URI api/gestionmateriel/delete/deleted

    /// <summary>
    /// Supprime un matériel détruit suivant son identifiant dans la route
    /// et rétourne un GestionMaterielResponseDto comprenant le matériel supprimé
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    [HttpDelete("delete/deleted/{idmat}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMatDeletedAsync([FromRoute] long idmat)
    {
        // 1- J'appelle la BLL est sa fonction PostGestionMaterielsAdd(gestionMatrielRequestDto)
        // 2- je retourne la réponse avec ActionResult OK
        //var reponse = await _GestionMaterielService.DeleteGestionMateriels(idmat);
        var reponse = await _GestionMaterielService.DeleteMatDeletedGestionMaterielsAsync(idmat);
        return Ok(reponse);
        //return Ok(reponse);
    }
    #endregion

}
