namespace Domain.ValueObjects;


public class MaterielGestion : ElmntGestion, IComparable<MaterielGestion>, IDisposable//TODO à remplacer par MaterielInfos
{

    public long IdMat { get; set; } //TODO à sup et remplacer par Id héritage pour transformation des fonctions update de la vue Gestion en génériques 
    public string NomMat { get; set; }

    public DateTime? DateMiseEnService { get; set; }
    public DateTime? DateFinGarantie { get; set; }
    public long? IdUser { get; set; }
    public string NomUser { get; set; }
    public long? IdMContrat { get; set; }
    public string NomContrat { get; set; }
    public bool Archive { get; set; } = false;
    public DateTime LastModif { get; set; }


    public MaterielGestion() { }

    public MaterielGestion(long idMat, string nomMat, DateTime? dateMiseEnService, DateTime? dateFinGarantie, long? idUser, string nomUser, long? idMContrat, string nomContrat, bool archive, DateTime lastModif) : base()
    {
        IdMat = idMat;
        NomMat = nomMat;
        DateMiseEnService = dateMiseEnService;
        DateFinGarantie = dateFinGarantie;
        IdUser = idUser;
        NomUser = nomUser;
        IdMContrat = idMContrat;
        NomContrat = nomContrat;
        Archive = archive;
        LastModif = lastModif;
    }

    public int CompareTo(MaterielGestion materielGestion)
    {
        if (materielGestion == null) return 0;

        if (this.IdMat != materielGestion.IdMat) return -1;
        if (this.NomMat != materielGestion.NomMat) return -1;
        if (this.DateMiseEnService != materielGestion.DateMiseEnService) return -1;
        if (this.IdUser != materielGestion.IdUser) return -1;
        if (this.NomUser != materielGestion.NomUser) return -1;
        if (this.IdMContrat != materielGestion.IdMContrat) return -1;
        if (this.NomContrat != materielGestion.NomContrat) return -1;
        if (this.Archive != materielGestion.Archive) return -1;
        if (this.LastModif != materielGestion.LastModif) return -1;

        return 1;
    }

    public void Dispose()
    {
        this.Dispose();
    }
}
