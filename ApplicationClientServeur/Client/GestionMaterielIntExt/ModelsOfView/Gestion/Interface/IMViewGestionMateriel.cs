using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;
using GestionMaterielIntExt.ModelsOfView.Gestion.Implementation;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.ObjTransf.Context;

namespace GestionMaterielIntExt.ModelsOfView.Gestion.Interface;

/// <summary>
/// la réalisation de cette 
/// </summary>
public interface IMViewGestionMateriel : IMViewGestion, IDisposable
{
    #region Properties

    public bool IsOpen { get; set; }
    public bool SequenceAjouterIsActive { get; set; }
    public bool SequenceModifierIsActive { get; set; }
    public IEnumerable<MaterielGestion> EnumMaterielsGestion { get; set; }
    public IEnumerable<CMaintenanceGestion> EnumCMaintenanceGestions { get; set; }
    public IEnumerable<EntrepriseGestion> EnumEntrepriseGestions { get; set; }

    public IEnumerable<Categorie> EnumCategories { get; set; }
    public IEnumerable<UserShort> EnumUsersShort { get; set; }
    public IEnumerable<ContratMaintenance> EnumCMaintenances { get; set; }

    /// <summary>
    /// Id matériel pour l'ajout et la mise-à-jours
    /// </summary>
    //public long Id { get; set; }
    public long IdMat { get; set; }
    public long IdCMaintenance { get; set; }
    public long IdEntreprise { get; set; }
    /// <summary>
    /// liste des catégorie de l'IdMat
    /// </summary>
    public IEnumerable<MaterielCategorie> EnumMaterielCategories { get; set; }

    public SysException? Exp { get; set; }

    public bool ContextIsActive { get; set; }
    public bool BtAjouterIsActive { get; set; }
    public bool BtModifierIsActive { get; set; }
    public bool BtRafraichirIsActive { get; set; }
    public bool BtAnnulerIsActive { get; set; }
    public bool BtArchiveIsActive { get; set; }
    public bool BtDetruireIsActive { get; set; }
    public bool ZoneFiltreIsActive { get; set; }

    public TrContextTabMateriel ValueTrContextTabMateriel { get; set; }


    public static MViewGestionMateriel Instance { get; private set; } //TODO utile ? non...

    #endregion
    /// <summary>
    /// formit une instance du modèle en fonction du type de modification ajout ou modifier. si null juste l'instance avec toutes ses données
    /// </summary>
    /// <param name="typeMode">ajout/modifier</param>
    /// <returns>IMViewGestionMateriel</returns>
    public IMViewGestionMateriel FactoMViewGestionMateriels(TypeMode? typeMode);

    /// <summary>
    /// Pour supprimer l'instance
    /// </summary>
    public void Dispose();

}
