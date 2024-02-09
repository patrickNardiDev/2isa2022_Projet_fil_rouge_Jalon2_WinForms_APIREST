using Domain.Entities;

namespace GestionMaterielIntExt.ObjTransf.Context;

/// <summary>
/// Classe d'encapsulation des données utilisateurs lors de l'ajout ou modification d'un matériel
/// </summary>
public class TrContextTabMateriel : ITrContext
{
    // donnée mini : Nom : non null et non vide,
    // données pouvant être nullable : DateMiseEnService, DateFinGarantie, IdUser, IdMContrat
    // valeur par défaut : Archive = false
    public long? IdMat { get; set; } = null;
    public string NomMat { get; set; } = null;
    public DateTime? DMService { get; set; } = null;
    public DateTime? DFGarantie { get; set; } = null;
    //public string UserShortName { get; set; } //TODO à sup ???
    //public long? IdUser { get; set; } = null; //TODO à sup ???

    public UserShort UserShort { get; set; }
    public ContratMaintenance? CMaintenance { get; set; } = new ContratMaintenance();
    public List<Categorie> Categories { get; set; } = new List<Categorie>();
    public bool Archive { get; set; } = false;
    public DateTime LastModif { get; set; }


    public override string ToString()
    {
        return @$"
    Nom : {NomMat}
    Date de mise en service : {DMService}
    Date de fin de garantie : {DFGarantie}
    Propriétaire : {(UserShort is null? "non attribué" : UserShort.ShortName)} 
        Id : {UserShort?.Id}
    Contrat de maintenance :{CMaintenance?.Nom}
        IdMContrat : {CMaintenance?.Id}
    Catégories : {String.Join(",", Categories)}
    archivé ? {Archive}"
    
    ;
    }
}
