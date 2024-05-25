namespace Domain.Exeption;

public class NotFoundEntitiesExeption : Exception
{
    public NotFoundEntitiesExeption() : base($"La sélection des données a échouée") { }
}
