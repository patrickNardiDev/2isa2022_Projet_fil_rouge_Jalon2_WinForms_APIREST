using Domain.Entities;
using GestionMaterielIntExt.ObjTransf.Context;

namespace GestionMaterielIntExt.Service;

/// <summary>
/// Service qui fournit des popup en Singleton
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    #region properties et constructor
    private static string _Message;
    private static MyEnumCaption _Caption;
    private static System.Windows.Forms.MessageBoxIcon _Icon;
    private static IEntity _Elmt;
    private static List<Categorie> _ListCategories;
    private static ITrContext _NewElmnt;

    private enum MyEnumCaption
    {
        Error,
        Information,
        Question,
        Stop,
        Warning
    }

    private static string Message { get => _Message; set => _Message = value; }
    private static MyEnumCaption Caption { get => _Caption; set => _Caption = value; }
    private static ITrContext NewElmnt { get => _NewElmnt; set => _NewElmnt = value; }
    private static IEntity Elmt { get => _Elmt; set => _Elmt = value; }
    private static System.Windows.Forms.MessageBoxIcon Icon { get => _Icon; set => _Icon = value; }
    private static List<Categorie> ListCategories { get => _ListCategories; set => _ListCategories = value; }

    private static MessageBoxService _Instance;

    public MessageBoxService() { }
    //public MessageBoxForms(MessageBoxIcon icon, IEntity elmt) : base()
    //{
    //    _Icon = icon;
    //    _Elmt = elmt;
    //    ListCategories = new List<Categorie>();

    //}

    //public MessageBoxForms(MessageBoxIcon icon, IEntity elmt, List<Categorie> listCategories) : base()
    //{
    //    _Icon = icon;
    //    _Elmt = elmt;
    //    ListCategories = listCategories;

    //}

    public MessageBoxService FactoMessageBoxService() => Instance;

    private static MessageBoxService Instance
    {
        get
        {
            if (_Instance is null)
                _Instance = new MessageBoxService();
            return _Instance;
        }
        set => _Instance = value;
    }
    #endregion

    #region méthodes privée
    private static DialogResult QuestionShow(MessageBoxIcon.Type typeModification)
    {
        //MessageBoxService myMessageBox = new();
        Message = "Voulez-vous";
        Caption = MyEnumCaption.Question;
        Icon = System.Windows.Forms.MessageBoxIcon.Question;
        switch (typeModification)
        {
            case MessageBoxIcon.Type.Ajouter:
                {
                    Message += $" ajouter cet élément\n{NewElmnt}";
                    break;
                }
            case MessageBoxIcon.Type.Modifier:
                {
                    Message += $" modifier cet élément dont l'identifiant est {NewElmnt?.IdMat} en \n{NewElmnt}";
                    break;
                }
            case MessageBoxIcon.Type.Archiver:
                {
                    Message += $" archiver cet élément\n{NewElmnt}";
                    break;
                }
            case MessageBoxIcon.Type.Supprimer:
                {
                    Message += $" supprimer cet élément\n{NewElmnt}";
                    break;
                }
            default:
                //if (ListCategories is not null)
                //    this.Message += $"\n{ListCategories}";
                break;
        }
        return MessageBox.Show(Message, Caption.ToString(), MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
    }

    private static DialogResult InformationShow(MessageBoxIcon.Type typeModification)
    {
        //MessageBoxService myMessageBox = new();
        //var myMessageBox = _Instance;

        Message = "Vous avez";
        Caption = MyEnumCaption.Information;
        Icon = System.Windows.Forms.MessageBoxIcon.Information;

        switch (typeModification)
        {
            case MessageBoxIcon.Type.Ajouter:
                {
                    Message += $" ajouter cet élément :\n{Elmt} \nAvec ces information :\n{NewElmnt}";
                    break;
                }
            case MessageBoxIcon.Type.Modifier:
                {
                    Message += $" modifier cet élément :\n{Elmt} \nAvec ces information :\n{NewElmnt}";
                    break;
                }
            case MessageBoxIcon.Type.Archiver:
                {
                    Message += $" archiver cet élément :\n{Elmt}";
                    break;
                }
            case MessageBoxIcon.Type.Supprimer:
                {
                    Message += $" supprimer cet élément :\n{Elmt}";
                    break;
                }

            default:
                {
                    var qlistNomCat = from categorie in ListCategories
                                      select new { categorie.Nom };
                    if (ListCategories is null)
                        Message += $"\nCatégories :\n{string.Join(",", qlistNomCat)}";
                    break;
                }
        }
        //var a  = MessageBox.Show(Message, Caption.ToString(), MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        return MessageBox.Show(Message, Caption.ToString(), MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
    }
    #endregion

    #region implémentation
    public DialogResult QuestionPopup(TrContextTabMateriel contextTabMateriel, MessageBoxIcon.Type myEnumMessageBoxIcon)
    {
            NewElmnt = contextTabMateriel;
            return QuestionShow(myEnumMessageBoxIcon);
    }

    public DialogResult InformationPopupMaterielOK<T>(TrContextTabMateriel contextTabMateriel, T? materiel, List<Categorie> categories, MessageBoxIcon.Type myEnumMessageBoxIcon) where T : IEntity
    {
        NewElmnt = contextTabMateriel;
        Elmt = materiel;
        ListCategories = categories;
        return InformationShow(myEnumMessageBoxIcon);
    }

    public DialogResult InformationErrorUserOK(string message) 
    {
        Message = message;
        Caption = MyEnumCaption.Information;
        Icon = System.Windows.Forms.MessageBoxIcon.Information;

        return MessageBox.Show(Message, Caption.ToString(), MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
    }

    public DialogResult QuestionAnnulerActionYesNo(string typeaction)
    {
        Message = $"Voulez-vous annuler votre action ?  \n{typeaction}";
        Caption = MyEnumCaption.Information;
        Icon = System.Windows.Forms.MessageBoxIcon.Information;

        return MessageBox.Show(Message, Caption.ToString(), MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
    }
    #endregion
}
