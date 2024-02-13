namespace Parameters;

public class DBConnection
{
    public const string DBCONNEXION = "DBConnexion";
    public string ConnectionString { get; init; }
    public string ProviderName { get; init; }
}
