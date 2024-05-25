using Domain.Entities;

namespace Domain.ValueObjects;

public class MaterielsInfos : IEntity
{
    public long IdMat { get; set; }
    public string NomMat { get; set; }
    public long IdCat { get; set; }
    public string NomCat { get; set; }
    public DateTime? DateMiseEnService { get; set; }
    public DateTime? DateFinGarantie { get; set; }
    public long IdUser { get; set; }
    public string NomUser { get; set; }
    public long? IdMContrat { get; set; }
    public string NomContrat { get; set; }
    public string Contrat { get; set; }
    public bool Archive { get; set; } = false;




    public MaterielsInfos() { }

    public MaterielsInfos(long idMat, string nomMat, long idCat, string nomCat, DateTime? dateMiseEnService, DateTime? dateFinGarantie, long idUser, string nomUser, long? idMContrat, string nomContrat, string contrat, bool archive) : base()
    {
        IdMat = idMat;
        NomMat = nomMat;
        IdCat = idCat;
        NomCat = nomCat;
        DateMiseEnService = dateMiseEnService;
        DateFinGarantie = dateFinGarantie;
        IdUser = idUser;
        NomUser = nomUser;
        IdMContrat = idMContrat;
        NomContrat = nomContrat;
        Contrat = contrat;
        Archive = archive;
    }
}
