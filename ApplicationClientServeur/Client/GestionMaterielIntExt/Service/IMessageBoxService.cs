using Domain.Entities;
using GestionMaterielIntExt.ObjTransf.Context;

namespace GestionMaterielIntExt.Service;

public interface IMessageBoxService
{
    /// <summary>
    /// Affiche une MessageBox de type question suivant le type myEnumMessageBoxIcon 
    /// concernant le matériel envoyé dans la context
    /// </summary>
    /// <param name="contextTabMateriel">valeur du context, matériel sélectionné et ou données entrées par l'utilisateur</param>
    /// <param name="myEnumMessageBoxIcon">sujet du message : Ajouter, Modifier, Archiver, Supprimer</param>
    /// <returns></returns>
    public DialogResult QuestionPopup(TrContextTabMateriel contextTabMateriel, MessageBoxIcon.Type myEnumMessageBoxIcon);

    /// <summary>
    /// Affiche une MessageBox d'iformation  suivant le type myEnumMessageBoxIcon
    /// concernant une action sur le matériel suivant les données d'entrée contextTabMateriel
    /// </summary>
    /// <param name="contextTabMateriel">valeur du context, matériel sélectionné et ou données entrées par l'utilisateur</param>
    /// <param name="materiel">matériel après action</param>
    /// <param name="categories">catégories du matériel</param>
    /// <param name="myEnumMessageBoxIcon">sujet du message : Ajouter, Modifier, Archiver, Supprimer</param>
    /// <returns></returns>
    public DialogResult InformationPopupMaterielOK<T>(TrContextTabMateriel contextTabMateriel, T materiel, List<Categorie> categories, MessageBoxIcon.Type myEnumMessageBoxIcon) where T : IEntity;

    /// <summary>
    /// Informe l'utilisateur d'une erreur de sa^part dans lentrées des données
    /// </summary>
    /// <param name="message">messqage d'erreur</param>
    /// <returns></returns>
    public DialogResult InformationErrorUserOK(string message);

    /// <summary>
    /// demande de confirmation avant action 
    /// </summary>
    /// <returns></returns>
    public DialogResult QuestionAnnulerActionYesNo(string typeaction);

}
