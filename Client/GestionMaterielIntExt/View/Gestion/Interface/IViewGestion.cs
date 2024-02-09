using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.Service;

namespace GestionMaterielIntExt.View.Gestion.Interface;

public interface IViewGestion
{
    /// <summary>
    /// booléen d'information sur l'ouverture de la vue 
    /// </summary>
    public static bool ViewIsOpen { get; private set; }
    //public static bool ModeAjoutIsOpen { get; private set; }
    //public static bool ModeModifIsOpen { get; private set; }

    /// <summary>
    /// 2ème partie d'instantiation de la classe àprès la facto
    /// </summary>
    /// <param name="controllerGestionMetariel"></param>
    public void ChangeIControllerGestionMateriel(IControllerGestionMetariel controllerGestionMetariel, IControllerGestionCMaintenance controllerGestionCMaintenance, IControllerGestionEntreprise controllerGestionEntreprise);

    /// <summary>
    /// fournit une instantiation de la classe
    /// </summary>
    /// <param name="mviewGestion"></param>
    /// <returns>ViewGestion</returns>
    public IViewGestion FactoViewGestion(IMViewGestionMateriel mviewGestion, IMessageBoxService messageBoxService);
    //public IViewGestion FactoViewGestion(IMViewGestionMateriel mviewGestion, IMessageBoxService messageBoxService);

    public bool GetIsOpen();

    public bool GetModeAjoutIsActif();

    public bool GetModeModifIsActif();

    public void ChangeValueOpen(bool value);

    /// <summary>
    /// permet d'afficher l'instance
    /// </summary>
    /// <param name="viewGestion">instance</param>
    public void Show(IViewGestion viewGestion);

    public void CloseView();

}
