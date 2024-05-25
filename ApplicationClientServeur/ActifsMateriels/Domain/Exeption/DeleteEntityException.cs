
using Domain.Entities;

namespace Domain.Exeption;

public class DeleteEntityException : Exception
{
    public DeleteEntityException(IEntity entity, long id) : base($"La suppression de {nameof(entity)} avec id : {id} dans la base de données a échoué") { }
}
