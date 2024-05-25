using DAL.Session.Implementation;
using DAL.Session.Interface;
using Domain.Exeption;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

// public static class TypeOfDataBase
// {
//     public static DBType TheDBType;
//     public static DBType DifineDatatbase(this TypeOfDataBase typeOfDataBase, DBType dbtype) => dbtype switch
//     {
//         DBType.MariaDB => DBType.MariaDB,
//         DBType.SQLServer => DBType.SQLServer,
//         DBType.PostgreSQL => DBType.PostgreSQL,
//         DBType.Oracle => DBType.Oracle,
//         _ => throw new ArgumentOutOfRangeException(nameof(dbtype), $"Not expected direction value: {dbtype}"),
//     };
// }
public enum DBType
{
    MariaDB,
    SQLServer,
    PostgreSQL,
    Oracle
}


public class DALOptions
{
    private static string _ConnString;
    //private readonly IParameters _parametersDB;

    public DALOptions()
    {
        //Here you can add your custom options
    }

    //public DBType DBType { get; set; } = DBType.MariaDB;
    public string DBConnectionString { get => _ConnString; set => _ConnString = value; }
}

public static class DALExtension
{
    //private static IConfiguration _configuration { get; }

    public static IServiceCollection AddDAL(this IServiceCollection services, Action<DALOptions> configure = null)
    {

        DALOptions options = new();
        configure?.Invoke(options);
        //options.DBType = dBType; // à supp, employé configure

        //Register your services here
        services.AddScoped<IDBSession>((services) =>
        {
            /// Chose Data type with DataBaseType in appsettings
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();
            int myDbType = 0; // Default MariaDB
            var IsIntDBType = Int32.TryParse(configuration.GetSection("DataBaseType").Value, out myDbType);
            // if (!IsIntDBType) throw new 
            switch ((DBType)myDbType)
            {
                case DBType.MariaDB:
                    options.DBConnectionString = configuration.GetSection("DBConnection:ConnectionString").Value;
                    return new DBSessionMariaDB(options.DBConnectionString);
                case DBType.SQLServer:
                //  return new DBSessionSQLServer(options.DBConnectionString);
                case DBType.PostgreSQL:
                //   return new DBSessionPostgreSQL(options.DBConnectionString);
                case DBType.Oracle:
                //  return new DBSessionOracle(options.DBConnectionString);
                default:
                    //return new DBSessionMariaDB(options.DBConnectionString);
                    throw new SysException($"Le type de connexion à la base de données que vous avez définit en paramètre, n'est pas implémenté.Veuillez contacter le service informatique.");
            }
        });
        services.AddTransient<IUOW, UOW>();
        return services;
    }
}
