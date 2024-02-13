namespace Domain.Entities;

/// <summary>
/// Classe des matériels détruits.
/// Si détruit alors suppression des tables D_MAETRIEL et D_MATERIEL_CATEGORIE
/// et ajout dans la table D_MATERIEL_DETRUIT
/// </summary>
public class MaterielDetruit : IEntity
{
    public long Id { get; set; } = -1;
    public string Nom { get; set; }

    public long OldNumMarquage { get; set; } = -1;

    public DateTime? DateMiseEnService {  get; set; }

    public DateTime DateDestruction { get; set; }

    public long? IdMContrat { get; set; }

    public string ListCategories { get; set; }

    public MaterielDetruit(){}

    public override string ToString() //TODO ici
    {
        return @$"
Matériel :
    Id : {Id}, 
    Nom : {Nom}, 
    Ancien numéro de marquage : {OldNumMarquage},
    Date de mise en service : {DateMiseEnService},
    Date de fin de garantie : {DateDestruction}, 
    Id du Contrat de maintenance : {IdMContrat}
";
    }

}
