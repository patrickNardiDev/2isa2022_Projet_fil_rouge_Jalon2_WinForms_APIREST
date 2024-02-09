using Domain.Entities;
using Domain.Exeption;
using GestionMaterielIntExt.Controller.Consultation.Interface;
using GestionMaterielIntExt.Model.Consultation.Interface;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Consultation.Interface;

namespace GestionMaterielIntExt.Controller.Consultation.Implementation;

/// <summary>
/// Contrôleur de consultaion du matériel, pour la vue Consultation
/// </summary>
internal class ControllerConsultation : IControllerConsultation
{
    //ID
    private readonly IModelConsultation _mconsult;
    private readonly IMViewConsultation _mviewConsult;
    private readonly IMessageBoxService _messageBoxService;


    // DI circulaire entre IViewConsultation et ControllerConsultation
    private readonly IViewConsultation _viewConsult;


    public ControllerConsultation(IModelConsultation mconsult, IMViewConsultation mviewConsult, IViewConsultation viewConsultation, IMessageBoxService messageBoxService) // TODO dépendance circulaire
    {
        _mconsult = mconsult;
        _mviewConsult = mviewConsult;
        _viewConsult = viewConsultation;
        _messageBoxService = messageBoxService;
    }

    #region Open Close vue Consultation
    /// <summary>
    /// ouverture de la vue consultation avec attribution des données
    /// </summary>
    /// <returns>vrai si les réponse du serveur n'est pas nule</returns>
    public async Task<bool> OpenViewConsultation()
    {
        // 0- je vérifie si la vu n'est pas ouverte
        // 1- j'appel le modèle consultaion et sa fonction GetConsultationMateriels() pour récupérer la réponse Dto
        // 2- en fonction de la réponse
        //  2.1- si la réponse n'est pas nule alors : 
        //      2.1.1 je fabrique le modèle de vue Constulation par défaut
        //      2.1.2 je change la valeur des properties du modèle de vue, les catégories et le tableau  (réponse 1) et les catégories (réponse 1)
        //      2.1.3 je change la valeur de l'exeption TODO exeption utile ?
        //      2.1.4 je fabrique la vue avec le modèle de vue
        //      2.1.5 j'affiche la vue
        //      2.1.6 je retourne vrai
        //  2.2- si réponse nule alors :
        //      2.2.1 je retourne faux
        if (_viewConsult.GetIsOpen())
            return false;
        else
        {
            //var responseConsultationDto = await _mconsult.GetConsultationMateriels();
            var trResponse = await _mconsult.GetConsultationMateriels();

            if (trResponse is null)
                return false;
            else
            {
                var myIMViewConsultation = _mviewConsult.FactoMViewConsultation();

                myIMViewConsultation.IenumMaterielsInfo = trResponse.IenumMaterielsInfos; ;
                myIMViewConsultation.IenumCategories = trResponse.IenumCategories;
                myIMViewConsultation.IenumMaterielCategories = trResponse.IenummaterielCategories;
                myIMViewConsultation.Exp = trResponse.Exp;

                // a envoyer dans un contrôleur d'ouverture de vue qui recevra le modele de vue
                var viewConsultation = _viewConsult.FactoViewConsultation(myIMViewConsultation);
                _viewConsult.ChangeIControllerConsultation(this);
                //var viewConsultation = _viewConsult.FactoViewConsultation(myIMViewConsultation, this);

                //var viewConsultation = _viewConsultLazy.Value.FactoViewConsultation(myIMViewConsultation);
                viewConsultation.Show();
                _viewConsult.ChangeValueOpen(true);
                return true;
            }
        }
        
    }

    public void CloseViewConsultation()
    {
        _viewConsult.CloseView();
    }
    #endregion


    #region GetMaterielsInfos filtre par catégorie

    /// <summary>
    /// Recherche par catégorie 
    /// </summary>
    /// <param name="categorie">catégorie recherché</param>
    /// <returns>le modèle de vue consultation</returns>
    public async Task<IMViewConsultation> GetMaterielsInfos(Categorie categorie)
    {
        try
        {
            // 1 - j'appelle le modèle consultaion et sa function GetMaterielsInfos(catégorie) pour récupérer sa réponse Dto
            // 2- en fonction des réponses
            //  2.1- si les réponses ne sontpas nule alors : 
            //      2.1.1 je fabrique le modèle de vue Constulation par défaut
            //      2.1.2 je change la valeur des properties du modèle de vue, le tableau (réponse 1) et les catégories (réponse 1)
            //      2.1.3 je change la valeur de l'exeption TODO exeption utile ?
            //      2.1.4 je retourne le modèle de vue
            //      2.1.5 j'affiche la vue
            //  2.2- si réponse nule alors :
            //      2.2.1 je fabrique le modèle de vue Constulation par défauts
            //      2.2.2 je retourne le modèle de vue
            if (categorie is not null)
            {
                //var responseConsultationDto = await _mconsult.GetConsultationMateriels(categorie.Id);
                var responseTransian = await _mconsult.GetConsultationMateriels(categorie.Id);


                if (responseTransian is not null)
                {
                    var myIMViewConsultation = _mviewConsult.FactoMViewConsultation();

                    myIMViewConsultation.ChangeIenumMaterielInfos(responseTransian.IenumMaterielsInfos);
                    myIMViewConsultation.ChangeIenumCategories(responseTransian.IenumCategories);
                    myIMViewConsultation.IenumMaterielCategories = responseTransian.IenummaterielCategories;
                    myIMViewConsultation.ChangeExeption(responseTransian.Exp);
                    return myIMViewConsultation;
                }
                else
                {
                    var myIMViewConsultationError = _mviewConsult.FactoMViewConsultation();
                    var exp = new SysException("Erreur système de réponse");
                    return myIMViewConsultationError;
                }
            }
            else
            {
                var myIMViewConsultationError = _mviewConsult.FactoMViewConsultation();
                var exp = new SysException("Erreur système");
                return myIMViewConsultationError;
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            return _mviewConsult.FactoMViewConsultation();

        }
        

    }

    #endregion

    #region GetMaterielsInfos --------------------------------------------
    public async Task<IMViewConsultation> GetMaterielsInfos()
    {
        try
        {
            //var responseConsultationDto = await _mconsult.GetConsultationMateriels();
            var trResponse = await _mconsult.GetConsultationMateriels();

            if (trResponse is not null)
            {
                var myIMViewConsultation = _mviewConsult.FactoMViewConsultation();

                myIMViewConsultation.IenumMaterielsInfo = trResponse.IenumMaterielsInfos;
                myIMViewConsultation.IenumCategories = trResponse.IenumCategories;
                myIMViewConsultation.Exp =trResponse.Exp;
                myIMViewConsultation.IenumMaterielCategories = trResponse.IenummaterielCategories;
                return myIMViewConsultation;
            }
            else
            {
                var myIMViewConsultationError = _mviewConsult.FactoMViewConsultation();
                var exp = new SysException("Erreur système de réponse");
                return myIMViewConsultationError;
            }
        }
        catch (Exception e)
        {
            _messageBoxService.InformationErrorUserOK(e.Message);
            return _mviewConsult.FactoMViewConsultation();

        }
    }

    #endregion



}
