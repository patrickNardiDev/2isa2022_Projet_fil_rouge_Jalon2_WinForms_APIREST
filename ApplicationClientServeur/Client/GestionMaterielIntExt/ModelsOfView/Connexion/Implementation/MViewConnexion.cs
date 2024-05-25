using Domain.Exeption;
using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;
using GestionMaterielIntExt.ObjTransf;

namespace GestionMaterielIntExt.ModelsOfView.Connexion.Implementation;


public class MViewConnexion : IMViewConnexion
{
    #region Information d'affichage de zone de boutons
    private bool _FlpBtnGestionVisible = false;
    private bool _FlpBtnConnexionVisible = true;
    private bool _BtCMaterielsEnabled = false;
    private bool _btGestionsEnabled = false;
    #endregion

    private UserConnected? _User = null;
    private bool _IsConnected = false;

    private SysException? _exeption = null;

    private bool _flpMessageError = false;


    public bool FlpBtnGestionVisible { get => _FlpBtnGestionVisible; set => _FlpBtnGestionVisible = value; }
    public bool FlpBtnConnexionVisible { get => _FlpBtnConnexionVisible; set => _FlpBtnConnexionVisible = value; }
    public UserConnected? User { get => _User; set => _User = value; }
    public bool BtCMaterielsEnabled { get => _BtCMaterielsEnabled; set => _BtCMaterielsEnabled = value; }
    public bool BtGestionsEnabled { get => _btGestionsEnabled; set => _btGestionsEnabled = value; }
    public bool IsConnected { get => _IsConnected; private set => _IsConnected = value; }
    public SysException? Exeption { get => _exeption; set => _exeption = value; }
    public bool FlpMessageError { get => _flpMessageError; set => _flpMessageError = value; }

    #region Constructor
    public MViewConnexion()
    {
    }



    //public MViewForm1() { }
    /// <summary>
    /// Modèle de vue pour la mise à jour de la vue IForm1
    /// </summary>
    /// <param name="flpBtnGestionVisible">Visibilité de la zone des boutons de consultation et gestion</param>
    /// <param name="flpBtnConnexionVisible">Visibilité de la zone connexion, email et mot de passe</param>
    /// <param name="btCMaterielsEnabled">Activation du bouton de consultation</param>
    /// <param name="btGestionsEnabled">Activation du bouton de gestion</param>
    /// <param name="user">Classe d'information de l'utilisateur connecté</param>
    private MViewConnexion(bool isConnected, bool flpBtnGestionVisible, bool flpBtnConnexionVisible, bool btCMaterielsEnabled, bool btGestionsEnabled, UserConnected? user) : this()
    {
        IsConnected = isConnected;
        FlpBtnGestionVisible = flpBtnGestionVisible;
        FlpBtnConnexionVisible = flpBtnConnexionVisible;
        BtGestionsEnabled = btGestionsEnabled;
        BtCMaterielsEnabled = btCMaterielsEnabled;
        User = user;
    }
    /// <summary>
    /// Constructeur pour informer d'une exeption
    /// </summary>
    /// <param name="e"></param>
    public MViewConnexion(SysException e)
    {
        Exeption = e;
        FlpMessageError = true;
    }
    #endregion


    public bool GetflpBtnGestionVisible()
    {
        return FlpBtnGestionVisible;
    }

    public bool GetflpBtnConnexionVisible()
    {
        return FlpBtnConnexionVisible;
    }

    public bool GetbtGestionsEnabled()
    {
        return BtGestionsEnabled;
    }

    public bool GetbtCMaterielsEnabled()
    {
        return BtCMaterielsEnabled;
    }

    public bool GetBtConsultation() => BtCMaterielsEnabled;
    public bool GetBtGestionVisible() => BtGestionsEnabled;

    public string GetbtUserName()
    {
        if (User == null)
            return string.Empty;
        return User.Name;
    }

    public bool UserIsConnected()
    {
        return IsConnected;
    }



    public string? GetMsgExeption()
    {
        if (Exeption is null) return string.Empty;
        //var a = new MViewConnexion();


        return Exeption.Message.ToString();
    }

    public bool GetFlpMessageError()
    {
        return FlpMessageError;
    }

    public void SetBtConsultation(bool bConsultation)
    {
        this.BtCMaterielsEnabled = bConsultation;
    }

    public void SetBtGestion(bool btGestion)
    {
        this.BtGestionsEnabled = btGestion;
    }

    public IMViewConnexion GetMViewExption(SysException sysExeption)
    {
        return new MViewConnexion(sysExeption);
    }

    public void SetUser(UserConnected userConnected)
    {
        this.User = userConnected;
    }

    public IMViewConnexion FactoMViewConnection(bool isconnected)
    {

        if (!isconnected)
        {
            return new MViewConnexion();

        }
        else
        {
            var Result = new MViewConnexion();
            Result.FlpBtnConnexionVisible = false;
            Result.FlpBtnGestionVisible = true;
            Result.BtCMaterielsEnabled = true;
            Result.BtGestionsEnabled = true;
            Result.IsConnected = true;
            Result.FlpMessageError = false;
            return Result;
        }

    }

    public IMViewConnexion FactoMViewConnection()
    {
        this.BtCMaterielsEnabled = !this.BtCMaterielsEnabled;
        return new MViewConnexion();

    }



}

