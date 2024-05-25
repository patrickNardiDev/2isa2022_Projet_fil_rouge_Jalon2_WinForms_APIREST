using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.ObjTransf.Context;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;

namespace GestionMaterielIntExt.ModelsOfView.Gestion.Implementation;

public class MViewGestionMateriel : IMViewGestionMateriel
{
    #region properties de context
    public bool IsOpen { get; set; }
    public TypeMode Sequence { get; set; }
    public bool SequenceAjouterIsActive { get; set; }
    public bool SequenceModifierIsActive { get; set; }
    public bool ContextIsActive { get; set; }
    public bool BtAjouterIsActive { get; set; }
    public bool BtModifierIsActive { get; set; }
    public bool BtArchiveIsActive { get; set; }
    public bool BtDetruireIsActive { get; set; }
    public bool BtRafraichirIsActive { get; set; }
    public bool BtAnnulerIsActive { get; set; }
    public bool ZoneFiltreIsActive { get; set; }
    public IEnumerable<Categorie> EnumCategories { get; set; } = new List<Categorie>();
    public IEnumerable<MaterielGestion> EnumMaterielsGestion { get; set; } = new List<MaterielGestion>();
    public IEnumerable<CMaintenanceGestion> EnumCMaintenanceGestions { get; set; } = new List<CMaintenanceGestion>();
    public IEnumerable<EntrepriseGestion> EnumEntrepriseGestions { get; set; } = new List<EntrepriseGestion>();

    public IEnumerable<UserShort> EnumUsersShort { get; set; } = new List<UserShort>();
    public IEnumerable<ContratMaintenance> EnumCMaintenances { get; set; } = new List<ContratMaintenance>();

    public IEnumerable<MaterielCategorie> EnumMaterielCategories { get; set; } = new List<MaterielCategorie>();

    public TrContextTabMateriel ValueTrContextTabMateriel { get; set; }
    public SysException Exp { get; set; }
    public long IdMat { get; set; } = -1;
    public long IdCMaintenance { get; set; } = -1;
    public long IdEntreprise { get; set; } = -1;
    //public long Id { get; set; } = -1;

    private static bool _SwitchAdd;
    private static bool _SwitchUpdate;




    #endregion

    // Instanvce unique du moèle de vue... pas réussit
    public static MViewGestionMateriel Instance { get; private set; }


    public MViewGestionMateriel()
    {
    }
    private static MViewGestionMateriel GetInstance()
    {
        if (Instance == null)
        {
            Instance = new MViewGestionMateriel();
            return Instance;
        }
        else
        {
            return Instance;
        }

    }

    public IMViewGestionMateriel FactoMViewGestionMateriels(TypeMode? typeMode) // boucle deux foix sur cette méthode à l'ouvertue de la vue. Au débuggage pas-à-pas
    {
        IMViewGestionMateriel result = null;
        if (typeMode is not null)
            Instance = null;
        result = GetInstance();
        result.ContextIsActive = false;
        result.IsOpen = true;

        MadeStateButtom(result, TypeMode.Init);

        if (typeMode == null) return result;
        else
        {
            switch (typeMode)
            {
                case TypeMode.Ajouter:
                    _SwitchAdd = !_SwitchAdd;
                    MadeStateButtom(result, TypeMode.Ajouter);
                    break;
                case TypeMode.Modifier:
                    _SwitchUpdate = !_SwitchUpdate;
                    MadeStateButtom(result, TypeMode.Modifier);
                    break;
            }
            return result;
        }
    }


    private void MadeStateButtom(IMViewGestionMateriel mview, TypeMode typemode)
    {
        switch (typemode)
        {
            case TypeMode.Init:
                mview.BtAjouterIsActive = true;
                mview.BtModifierIsActive = true;

                mview.BtRafraichirIsActive = true;
                mview.BtArchiveIsActive = true;
                mview.BtDetruireIsActive = true;
                mview.ZoneFiltreIsActive = true;

                mview.BtAnnulerIsActive = false;
                break;
            case TypeMode.Ajouter:
                mview.SequenceAjouterIsActive = _SwitchAdd;
                mview.ContextIsActive = _SwitchAdd;
                mview.BtAjouterIsActive = true;
                mview.BtAnnulerIsActive = _SwitchAdd;
                //mview.BtRafraichirIsActive = true;

                mview.BtModifierIsActive = !_SwitchAdd;
                mview.BtArchiveIsActive = !_SwitchAdd;
                mview.BtDetruireIsActive = !_SwitchAdd;
                mview.ZoneFiltreIsActive = !_SwitchAdd;
                break;
            case TypeMode.Modifier:
                mview.SequenceModifierIsActive = _SwitchUpdate;
                mview.ContextIsActive = _SwitchUpdate;
                mview.BtModifierIsActive = true;
                mview.BtAnnulerIsActive = _SwitchUpdate;
                //mview.BtRafraichirIsActive = true;

                mview.BtAjouterIsActive = !_SwitchUpdate;
                mview.BtArchiveIsActive = !_SwitchUpdate;
                mview.BtDetruireIsActive = !_SwitchUpdate;
                mview.ZoneFiltreIsActive = !_SwitchUpdate;
                break;
            case TypeMode.Inactive:
                mview.BtAjouterIsActive = false;
                mview.BtModifierIsActive = false;

                mview.BtRafraichirIsActive = false;
                mview.BtArchiveIsActive = false;
                mview.BtDetruireIsActive = false;
                mview.ZoneFiltreIsActive = false;

                mview.BtAnnulerIsActive = false;
                break;
        }
    }

    public void Dispose()
    {
        Instance = null;
    }

}
