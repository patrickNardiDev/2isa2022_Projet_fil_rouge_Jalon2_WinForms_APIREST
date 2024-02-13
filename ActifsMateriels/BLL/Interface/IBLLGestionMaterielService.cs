using Domain.DTO.Gestion;
using Domain.Entities;

namespace BLL.Interface;

public interface IBLLGestionMaterielService
{
    /// <summary>
    /// fournit la liste des actifs matériels, MaterielGestion,
    /// la liste des Categorie,
    /// la liste des utilisateur, UserTransit,
    /// la liste des contrat de maintenance, 
    /// la liste des catégorie associées au matériel de l'idmat, MaterielCategorie
    /// la liste des de ces même catérie avec leurs noms, Categorie
    /// </summary>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> GetGestionMaterielsAsync();

    /// <summary>
    /// fournit la liste des actifs matériels, MaterielGestion, suivant la catégorie séléctionné, idcategorie
    /// la liste des Categorie,
    /// la liste des utilisateur, UserTransit,
    /// la liste des contrat de maintenance, 
    /// la liste des catégorie associées au matériel de l'idmat, MaterielCategorie
    /// la liste des de ces même catérie avec leurs noms, Categorie    /// </summary>
    /// <param name="idcategorie"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> GetGestionMaterielsByCatAsync(long idcategorie);

    /// <summary>
    /// fournit la liste des actifs matériels, MaterielGestion,
    /// la liste des Categorie,
    /// la liste des utilisateur, UserTransit,
    /// la liste des contrat de maintenance, 
    /// la liste des catégorie associées au matériel de l'idMateriel, MaterielCategorie
    /// la liste des de ces même catérie avec leurs noms, Categorie 
    /// </summary>
    /// <param name="idMateriel"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> GetGestionMaterielsByMatAsync(long idMateriel);

    /// <summary>
    /// fournit un matériel suivant son identifiant
    /// </summary>
    /// <param name="idMateriel"></param>
    /// <returns>Materiel</returns>
    public Task<Materiel> GetMaterielByIdAsync(long idMateriel);

    /// <summary>
    /// ajout un matériel avec les catégorie associées et renvoie son Id, IDMat, et ses catégorie associée, CatOfIdMat
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> PostGestionMaterielsAddAsync(GestionMatrielRequestDto gestionMatrielRequestDto);

    /// <summary>
    /// modifit un matériel, ses catégories associées, son contrat de maintenance associé
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> PutGestionMaterielsUpdateAsync(GestionMatrielRequestDto gestionMatrielRequestDto);

    /// <summary>
    /// Archive un matériel renvoyé par son id, IdMat
    /// </summary>
    /// <param name="gestionMatrielRequestDto"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> PutGestionMaterielsArchiveAsync(GestionMatrielRequestDto gestionMatrielRequestDto);

    /// <summary>
    /// supprime un matériel suivant son identifiant, idmat
    /// </summary>
    /// <param name="idMat"></param>
    /// <returns>GestionMaterielResponseDto</returns>
    public Task<GestionMaterielResponseDto> DeleteGestionMaterielsAsync(long idMat);

    /// <summary>
    /// fornit le matériel détuit suivant son ancien numéro de marquage, identifiant idmat
    /// </summary>
    /// <param name="idMat"></param>
    /// <returns>MaterielDetruit</returns>
    public Task<MaterielDetruit> GetMatDeletedAsync(long idMat);

    /// <summary>
    /// supprime de la table MaterielDetruit un matériel déjà supprimé
    /// </summary>
    /// <param name="idmatdeleted"></param>
    /// <returns>bool</returns>
    public Task<bool> DeleteMatDeletedGestionMaterielsAsync(long idmatdeleted);
}
