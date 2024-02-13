namespace Domain.Entities;

public class Categorie : IEntity, IComparable<Categorie>
{
    public long Id { get; set; }
    public string Nom { get; set; }

    public Categorie() { }

    public Categorie(long id, string nom)
    {
        Id = id;
        Nom = nom;
    }

    public override string ToString()
    {

        return @$"
        Id : {Id}
        Nom : {Nom}";

        //return base.ToString();
    }

    public int CompareTo(Categorie categorie)
    {
        if (categorie == null) return 0;
        if (this.Id != categorie.Id) return -1;
        if (this.Nom != categorie.Nom) return -1;
        return 1;
    }
}
