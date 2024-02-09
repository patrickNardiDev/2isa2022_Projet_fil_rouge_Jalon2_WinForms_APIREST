
using Domain.Entities;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf.Context;

namespace GestionMaterielIntExt.Controller.Gestion.Interface;

public interface IControllerGestionMetariel
{
    /// <summary>
    /// Ouverture de la vue de gestion
    /// </summary>
    /// <returns></returns>
    public Task OpenViewGestion();

    public void CloseViewGestion();
    // Get

    /// <summary>
    /// Fournit l'ensemble des éléments pour la mise à jour de la vue
    /// </summary>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> GetMaterielGestion();

    /// <summary>
    /// Fournit l'ensemble des éléments pour la mise à jour de la vue suivant la catégorie recherchée
    /// </summary>
    /// <param name="categorie">Catégorie matériel</param>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> GetMaterielGestion(Categorie categorie);

    /// <summary>
    /// Fournit l'ensemble des éléments pour la mise à jour de la vue suivant l'identifiant d'un matériel
    /// </summary>
    /// <param name="id">identifiant de la catégorie</param>
    /// <returns></returns>
    public Task<Materiel> GetMaterielById(long id);

    // Post new

    /// <summary>
    /// Ajoute un élément suivant les données utilisatuers du context
    /// </summary>
    /// <param name="ValuesContext">données définies par l'utilisateur dans la zone de contexte</param>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> Ajouter(TrContextTabMateriel ValuesContext);

    // Put

    /// <summary>
    /// Mofifie un élément suivant les données utilisatuers du context
    /// </summary>
    /// <param name="ValuesContext">données définies par l'utilisateur dans la zone de contexte</param>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> Modifier(TrContextTabMateriel ValuesContext);

    // Archive

    /// <summary>
    /// Archive un matériel
    /// </summary>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> Archiver(TrContextTabMateriel ValuesContext);

    /// <summary>
    /// Supprime un matériel
    /// </summary>
    /// <param name="ValuesContext">données définies par l'utilisateur dans la zone de contexte</param>
    /// <returns></returns>
    public Task<IMViewGestionMateriel> Supprimer(TrContextTabMateriel ValuesContext);

    /// <summary>
    /// récupère un matériel détruit dans la table D_MATERIEL_DETRUIT
    /// </summary>
    /// <param name="idMat">ancien id du matériel détruit</param>
    /// <returns></returns>
    public Task<MaterielDetruit> GetMaterielSupprimeById(long idMat);



}
