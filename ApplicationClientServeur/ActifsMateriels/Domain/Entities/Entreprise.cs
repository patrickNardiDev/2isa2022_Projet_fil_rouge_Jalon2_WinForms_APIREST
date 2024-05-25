//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Domain.Entities;

public class Entreprise : IEntity
{
    public long? Id { get; set; }
    public string Nom { get; set; }
    public string? Tel { get; set; }
    public string Email { get; set; }
    public bool Archive { get; set; }
    public DateTime LastModif { get; set; }


    public Entreprise() { }
    public Entreprise(long? id, string nom, string? tel, string email, bool archive, DateTime lastModif)
    {
        Id = id;
        Nom = nom;
        Tel = tel;
        Email = email;
        Archive = archive;
        LastModif = lastModif;

    }


    public override string ToString()
    {
        return @$"
Entreprise :
    Id : {Id}, 
    Nom : {Nom}, 
    Téléphone : {Tel}, 
    courriel : {Email}, 
    archivé ? {Archive}

";
        //return base.ToString();
    }
}
