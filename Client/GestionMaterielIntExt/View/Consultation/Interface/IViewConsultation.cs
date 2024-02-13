using Domain.ValueObjects;
using GestionMaterielIntExt.Controller.Consultation.Interface;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;
using GestionMaterielIntExt.View.Consultation.Implementation;
using System.ComponentModel;

namespace GestionMaterielIntExt.View.Consultation.Interface;

public interface IViewConsultation
{
    /// <summary>
    /// booléen d'information sur l'ouverture de la vue 
    /// </summary>
    public static bool ViewIsOpen;

 
    /// <summary>
    /// 2ème partie d'instantiation de la classe àprès la facto
    /// </summary>
    /// <param name="controllerGestionMetariel"></param>
    public void ChangeIControllerConsultation(IControllerConsultation controllerConsultation);

    /// <summary>
    /// fournit une instantiation de la classe
    /// </summary>
    /// <param name="mviewGestion"></param>
    /// <returns>ViewConsultation</returns>
    public ViewConsultation FactoViewConsultation(IMViewConsultation mviewConsultation);

    /// <summary>
    /// Retourne le booléen d'information d'ouverture de la vue
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsOpen();

    /// <summary>
    /// Modifie le booléen d'information d'ouverture de la vue
    /// </summary>
    /// <param name="value"></param>
    public void ChangeValueOpen(bool value);

    public void CloseView();

}
