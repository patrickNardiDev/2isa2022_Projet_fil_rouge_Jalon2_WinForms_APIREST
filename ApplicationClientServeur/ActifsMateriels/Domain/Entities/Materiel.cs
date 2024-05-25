namespace Domain.Entities;

public class Materiel : IEntity, IComparable<Materiel>
{
    public long Id { get; set; } = -1; // ne peut pas être nul dans les Linq
    public string Nom { get; set; }
    public DateTime? DateMiseEnService { get; set; }
    public DateTime? DateFinGarantie { get; set; }
    public long? IdUser { get; set; }
    public long? IdMContrat { get; set; }
    public bool Archive { get; set; }
    public DateTime LastModif { get; set; } = DateTime.Now;

    public Materiel() { }
    public Materiel(long id, string nom, DateTime? dateMiseEnService, DateTime? dateFinGarantie, long? idUser, long? idMContrat, bool archive, DateTime lastModif)
    {
        Id = id;
        Nom = nom;
        DateMiseEnService = dateMiseEnService;
        DateFinGarantie = dateFinGarantie;
        IdUser = idUser;
        IdMContrat = idMContrat;
        Archive = archive;
        LastModif = lastModif;
    }

    public override string ToString()
    {
        return @$"
Matériel :
    Id : {Id}, 
    Nom : {Nom}, 
    Date de mise en service : {DateMiseEnService},
    Date de fin de garantie : {DateFinGarantie}, 
    IdUser : {IdUser}, 
    IdMContrat : {IdMContrat}, 
    Archivé ? {Archive}

";
        //return base.ToString();
    }

    public int CompareTo(Materiel materiel)
    {
        if (materiel == null) return 0;
        if (this.Id != materiel.Id) return -1;
        if(this.DateFinGarantie != materiel.DateFinGarantie) return -1;
        if(this.IdUser != materiel.IdUser) return -1;
        if(this.Archive != materiel.Archive) return -1;
        if(this.DateMiseEnService != materiel.DateMiseEnService) return -1;
        if(this.IdMContrat !=  materiel.IdMContrat) return -1;
        if(this.Nom != materiel.Nom) return -1;

        return 1;
    }

}
