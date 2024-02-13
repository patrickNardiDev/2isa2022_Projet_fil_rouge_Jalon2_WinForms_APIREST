namespace Parameters;

public class JwToken
{
    public const string JWTOKEN = "JwToken";
    public string JwtKey { get; init; }
    public string JwtIssuer { get; init; }
    public string JwtExpireDays { get; init; }

}
