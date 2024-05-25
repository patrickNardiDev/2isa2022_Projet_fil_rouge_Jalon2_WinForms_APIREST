using MySql.Data.MySqlClient;


namespace GestionMaterielIntExt.Model;

public class SingletonConnexion
{
    private static string _Server = "lab004.2isa.org";
    private static string _Database = "GESTION_TICKETS";
    private static string _User = "root";
    private static string _Password = "3265lab004";
    private static string _Port = "33004";
    private static string _ConnString = string.Format("server={0};port={1};user id={2}; password={3}; database={4}", Server, Port, User, Password, Database);

    private MySqlConnection _myConnect;

    public static string Server { get => _Server; private set => _Server = value; }
    public static string Database { get => _Database; private set => _Database = value; }
    public static string User { get => _User; private set => _User = value; }
    public static string Password { get => _Password; private set => _Password = value; }
    public static string Port { get => _Port; private set => _Port = value; }
    public static string ConnString { get => _ConnString; private set => _ConnString = value; }

    public MySqlConnection MyConnect { get => _myConnect; private set => _myConnect = value; }



    private static SingletonConnexion _Instance;
    public SingletonConnexion()
    {
        MyConnect = new MySqlConnection(ConnString);
        try
        {
            MyConnect.Open();
            Console.WriteLine("Connection Successful"); // utile OUI !

            MyConnect.Close();
        }
        catch (MySqlException e)
        {
            MessageBox.Show("la connexion au serveur est impossible");
        }
    }
    public static SingletonConnexion Instance
    {
        get
        {
            if (_Instance is null)
            {
                _Instance = new SingletonConnexion();

            }
            return _Instance;
        }
        private set => _Instance = value;

    }

}