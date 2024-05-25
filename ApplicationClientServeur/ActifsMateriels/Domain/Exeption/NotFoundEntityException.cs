namespace Domain.Exeption;
public class NotFoundEntityException : Exception
{
    public NotFoundEntityException(string entityName, long id) : base($"L'entité {entityName} avec l'id: {id} est introuvable")
    {

    }
}
