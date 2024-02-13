using Domain.Entities;

namespace Domain.ValueObjects;

public class ValueContextMateriel
{
    // donnée mini : Nom : non null et non vide,
    // données pouvant être nullable : DateMiseEnService, DateFinGarantie, IdUser, IdMContrat
    // valeur par défaut : Archive = false
    public string NomMat { get; set; } = null;
    public DateTime? DMService { get; set; } = null;
    public DateTime? DFGarantie { get; set; } = null;
    public string UserShortName { get; set; }
    public long? IdUser { get; set; } = null;
    public long? IdMat { get; set; } = null;

    public UserTransit UserShort { get; set; } = null;
    public ContratMaintenance CMaintenance { get; set; } = new ContratMaintenance();
    public List<Categorie> Categories { get; set; } = new List<Categorie>();
    public bool Archive { get; set; } = false;

    public DateTime LastModif { get; set; }

}
