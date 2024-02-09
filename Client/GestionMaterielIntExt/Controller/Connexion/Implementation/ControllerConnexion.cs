using Domain.Entities;
using Domain.Exeption;
using GestionMaterielIntExt.Controller.Connexion.Interface;
using GestionMaterielIntExt.Controller.Consultation.Interface;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.Model.Connexion.Interface;
using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;
using GestionMaterielIntExt.ObjTransf;
using GestionMaterielIntExt.ObjTransf.Connexion;
using GestionMaterielIntExt.Service;



namespace GestionMaterielIntExt.Controller.Connexion.Implementation;

internal class ControllerConnexion : IControllerConnexion
{
    #region properties et constructeur
    //avec ID
    private readonly IModelConnexion _mc;
    private readonly IMViewConnexion _mvc;
    private readonly IControllerConsultation _cconsult;
    private readonly IControllerGestionMetariel _cgm;
    private readonly IMessageBoxService _messageBoxService;


    private string AccessToken; // de connexion

    public ControllerConnexion(IModelConnexion mConnexion, IMViewConnexion mViewForm1, IControllerConsultation controllerConsultation, IControllerGestionMetariel controllerGestionMetariel, IMessageBoxService messageBoxService)
    {
        _mvc = mViewForm1;
        _mc = mConnexion;
        _cconsult = controllerConsultation;
        _cgm = controllerGestionMetariel;
        _messageBoxService = messageBoxService;
    }
    #endregion

    #region Connexion
    /// <summary>
    /// Intance de contrôle qui demande au modèle de lui fournir le modèle de vue en fonction de des valeurs (inputs) de connexion.
    /// Le modèle de vue est renvoyé à la vue pour qu'elle se mette à jour en fonction du rôle de l'utilisateur
    /// Rôle issu de l'énumération du Domain.Entities.Role.RoleEnum
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IMViewConnexion> ConnexionUser(string email, string password)
    {
        try
        {
            // 1- reçois les données entrée par l'utilisateur,
            // 2- crée la Dto de requête
            // 2- appel le modèle et sa fonction connexion pour recevoir la Dto de réponse
            // 3- retourne le modèle de vue correspondant

            // création de l'object correspondant à la DtoRequest de l'api
            //var myInputsConnexion = new ConnexionRequestDto(email, password);

            // appel de la fonction du modèle pour l'obtention de la DtoResponse de l'api 
            //var UserConnexionResult = await _mc.ConnexionUser(myInputsConnexion); // idem avec et sans ID
            //Transian
            var myInputsConnexion = new TrConnexionRequest(email, password);
            var UserConnexionResult = await _mc.ConnexionUser(myInputsConnexion); // idem avec et sans ID

            if (UserConnexionResult.Role == Role.RoleEnum.Gestion)
            {
                // Dépendance à Oject de Transfert (ObjTraf)
                // création d'un l'utilisateur administrateur connecté
                UserConnected userConnected = new UserConnected(UserConnexionResult.IsConnected, UserConnexionResult.NomPrenom, UserConnexionResult.Role);
                // Création du modèle de vue en fonction du role
                //var myMViewConnexion = _mvc.GetMView(true, true, false, true, true, userConnected);
                var myMViewConnexion = _mvc.FactoMViewConnection(UserConnexionResult.IsConnected);
                myMViewConnexion.SetBtConsultation(true);
                myMViewConnexion.SetBtGestion(true);
                myMViewConnexion.SetUser(userConnected);
                return myMViewConnexion;

            }
            else if (UserConnexionResult.Role == Role.RoleEnum.Consultation) // role de consultation
            {
                UserConnected userConnected = new UserConnected(UserConnexionResult.IsConnected, UserConnexionResult.NomPrenom, UserConnexionResult.Role);
                var myMViewConnexion = _mvc.FactoMViewConnection(UserConnexionResult.IsConnected);
                myMViewConnexion.SetBtConsultation(true);
                myMViewConnexion.SetBtGestion(false);
                myMViewConnexion.SetUser(userConnected);
                return myMViewConnexion;
            }
            else
            {
                //UserConnected userConnected = new UserConnected(false, UserConnexionResult.NomPrenom, UserConnexionResult.Role);
                _messageBoxService.InformationErrorUserOK("vous n’avez pas les droits d’accès à cette application");
                return _mvc.GetMViewExption(new SysException("Accès interdit"));
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            //return _mvc.GetMViewExption(new SysException(e.Message));
            return _mvc.GetMViewExption(new SysException(e.Message));
        }

    }

    public IMViewConnexion DeConnexionUser()
    {
        bool result = _mc.DeConnexionUser();

        _cconsult.CloseViewConsultation();
        _cgm.CloseViewGestion();
        var myMViewConnexion = _mvc.FactoMViewConnection(false);
        return myMViewConnexion;
    }
    #endregion

    #region Consultation et Gestion Open
    public async Task<bool> AccessConsultation()
    {
        try
        {
            // 1- j'appel le constructeur consultaion
            return await _cconsult.OpenViewConsultation();

        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            return false;
        }

    }

    public async Task AccessGestion()
    {
        try
        {
            await _cgm.OpenViewGestion();

        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
        }
    }


    #endregion
}







