namespace Domain.ValueObjects;

public class EntrepriseGestion : ElmntGestion
{
    public long Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Tel { get; set; } = null;
    public int NbContrat { get; set; }
    public bool Archive { get; set; } = false;
    public DateTime LastModif { get; set; }


    public EntrepriseGestion() { }
}
