namespace Domain.Entities;

public class MaterielCategorie : IEntity
{
    public long IdMateriel { get; set; }
    public long IdCategorie { get; set; }

    public MaterielCategorie()
    {
    }

    public MaterielCategorie(long idMateriel, long idCategorie)
    {
        IdMateriel = idMateriel;
        IdCategorie = idCategorie;
    }


}
