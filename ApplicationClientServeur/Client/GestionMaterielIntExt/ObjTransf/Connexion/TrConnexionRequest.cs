namespace GestionMaterielIntExt.ObjTransf.Connexion;

public class TrConnexionRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public TrConnexionRequest() { }
    public TrConnexionRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
