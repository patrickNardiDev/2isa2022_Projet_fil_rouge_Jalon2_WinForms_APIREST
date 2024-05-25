namespace Domain.Entities;

public class ContratMaintenance : IEntity
{
    public long Id { get; set; }
    public string Nom { get; set; }
    public string? Info { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public DateTime? DateDerniereIntervention { get; set; }
    public DateTime? DateProfaineIntervention { get; set; }
    public long IdEntreprise { get; set; }
    public bool Archive { get; set; }
    public DateTime LastModif { get; set; }


    public ContratMaintenance() { }

    public ContratMaintenance(long id, string nom, string? info, DateTime dateDebut, DateTime dateFin, DateTime? dateDerniereIntervention, DateTime? dateProfaineIntervention, long idEntreprise, bool achive, DateTime lastModif)
    {
        Id = id;
        Nom = nom;
        Info = info;
        DateDebut = dateDebut;
        DateFin = dateFin;
        DateDerniereIntervention = dateDerniereIntervention;
        DateProfaineIntervention = dateProfaineIntervention;
        IdEntreprise = idEntreprise;
        Archive = achive;
        LastModif = lastModif;

    }

    public override string ToString()
    {
        return @$"
Contrat de maintenance :
    Id : {Id},
    Nom : {Nom},
    Info : {Info},
    Date de début : {DateDebut},
    Date de fin : {DateFin},
    Date de dernière intervention : {DateDerniereIntervention},
    Date de profaine intervention : {DateProfaineIntervention},
    Id entreprise : {IdEntreprise},
    Archivé ? {Archive}"

;

        //return base.ToString();
    }
}

