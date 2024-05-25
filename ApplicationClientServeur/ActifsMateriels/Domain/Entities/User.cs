namespace Domain.Entities;

public class User : IEntity
{
    //Non respect des conventions d'écriture pour respecter celles de la bibliothèque Fortyfy de Laravel qui crée la table "user"
    public long id { get; set; }
    public string name { get; set; }
    public string firstname { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int? idSIRole { get; set; }

    public User() { }
    public User(long Id, string Name, string Firstname, string Email, string Password, int? IdRole)
    {
        id = Id;
        name = Name;
        firstname = Firstname;
        email = Email;
        password = Password;
        idSIRole = IdRole;
    }

 

    public override string ToString()
    {
        return @$"
Propriétaire :
    Id : {id}, 
    Nom : {name}, 
    Prénom : {firstname}

";
    }
}

