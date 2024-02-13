namespace Domain.Exeption;

public class UnauthorizeException : Exception
{
    public UnauthorizeException() : base($"La connexion a échoué") { }

}
