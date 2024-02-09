using BLL.Implementation;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace BLL;
public class BLLConnexionOptions
{
    //public Dictionary<int, DBType> DataBasesTypeImplemented = new();

    public BLLConnexionOptions()
    {
        //DataBasesTypeImplemented.Add(0, DBType.MariaDB);
    }

    // public DBType DataBaseType { get; set; }
    //Here you can add your custom options
}
public static class BLLExtention
{

    public delegate void DALLOptionsDelegate(DALOptions options);

    //public static IServiceCollection AddBLL(this IServiceCollection services, DBConnection dbconnection, JwToken jwToken, Action<BLLConnexionOptions> configure = null)
    public static IServiceCollection AddBLL(this IServiceCollection services,  Action<BLLConnexionOptions> configure = null)
    {
        BLLConnexionOptions options = new();
        configure?.Invoke(options);


        //Register your BLL services here
        services.AddTransient<IBLLConnexionService, BLLConnexionService>();
        services.AddTransient<IBLLConsultationService, BLLConsultationService>();
        services.AddTransient<IBLLGestionMaterielService, BLLGestionMaterielService>();
        services.AddTransient<IBLLGestionCMaintenanceService, BLLGestionCMaintenanceService>();
        services.AddTransient<IBLLGestionEntrepriseService, BLLGestionEntrepriseService>();

        // implémentation induite par les besoin des tests unitaires de la BLLConnexionService
        services.AddTransient<IBCrypService, BCrypService>();

        //var dallOptions = new DALOptions();
        //dallOptions.DBType = configure.;
        //var dbTypeDelegate = new DALLOptionsDelegate();
        //Register DAL services
        //services.AddDAL(dBType);
        services.AddDAL();


        return services;
    }
}
