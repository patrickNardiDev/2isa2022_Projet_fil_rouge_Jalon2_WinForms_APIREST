namespace Parameters;

public interface IParameters
{
    public DBConnection DBConnection { get; init; }
    public JwToken JwToken { get; init; }
}

public class Parameters : IParameters
{
    public DBConnection DBConnection { get; init; }
    public JwToken JwToken { get; init; }
}
