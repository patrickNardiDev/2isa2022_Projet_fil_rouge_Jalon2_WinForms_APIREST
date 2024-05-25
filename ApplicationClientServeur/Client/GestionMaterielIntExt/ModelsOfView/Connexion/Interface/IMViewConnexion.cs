using Domain.Exeption;
using GestionMaterielIntExt.ObjTransf;

namespace GestionMaterielIntExt.ModelsOfView.Connexion.Interface
{
    public enum EnumConnexion
    {
        notConnected,
        connected
    }
    public interface IMViewConnexion
    {

        /// <summary>
        /// Renvoie la valeur de visibilité du Flow layout panel comprenant les boutons consultation et gestion ?
        /// </summary>
        /// <returns></returns>
        public bool GetflpBtnGestionVisible();

        /// <summary>
        /// Renvoie la valeur de visibilité du Flow layout panel comprenant la zone de connexion
        /// </summary>
        /// <returns></returns>
        public bool GetflpBtnConnexionVisible();

        /// <summary>
        /// Activer le bouton de gestion ?
        /// </summary>
        /// <returns></returns>
        public bool GetbtGestionsEnabled();

        /// <summary>
        /// Renvoie la valeur d'activation du bouton de consultation matériel
        /// </summary>
        /// <returns></returns>
        public bool GetbtCMaterielsEnabled();

        /// <summary>
        /// Renvoie la le nom formaté de l'utilisateur connecté
        /// </summary>
        /// <returns></returns>
        public string GetbtUserName();

        public bool UserIsConnected();

        public string GetMsgExeption();
        public bool GetFlpMessageError();

        public void SetBtConsultation(bool bConsultation);
        public void SetBtGestion(bool bGestion);

        public bool GetBtConsultation();
        public bool GetBtGestionVisible();


        public void SetUser(UserConnected userConnected);

        public IMViewConnexion FactoMViewConnection();
        /// <summary>
        /// Fabrique un modèle de vue connexion en fonction de l'enumération, sans la zone erreur visible
        /// </summary>
        /// <param name="enumConnexion"></param>
        /// <returns></returns>
        public IMViewConnexion FactoMViewConnection(bool isconnected);
        /// <summary>
        /// Crée un modèle de vue avec la zone erreur visible
        /// </summary>
        /// <param name="connexionExeption"></param>
        /// <returns></returns>
        public IMViewConnexion GetMViewExption(SysException sysExeption);


    }
}
