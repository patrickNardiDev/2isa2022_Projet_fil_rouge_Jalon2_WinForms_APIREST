namespace Domain.Exeption;

internal class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException() : base("La connexion n'est pas autorisée") { }

}

