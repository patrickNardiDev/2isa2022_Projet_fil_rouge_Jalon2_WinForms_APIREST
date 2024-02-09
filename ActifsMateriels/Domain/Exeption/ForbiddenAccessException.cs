namespace Domain.Exeption;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base($"Accès interdit") { }

}
