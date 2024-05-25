using GestionMaterielIntExt.Controller.Connexion.Implementation;
using GestionMaterielIntExt.Controller.Connexion.Interface;
using GestionMaterielIntExt.Controller.Consultation.Implementation;
using GestionMaterielIntExt.Controller.Consultation.Interface;
using GestionMaterielIntExt.Controller.Gestion.Implementation;
using GestionMaterielIntExt.Controller.Gestion.Interface;
using GestionMaterielIntExt.Model.Connexion.Implementation;
using GestionMaterielIntExt.Model.Connexion.Interface;
using GestionMaterielIntExt.Model.Consultation.Implementation;
using GestionMaterielIntExt.Model.Consultation.Interface;
using GestionMaterielIntExt.Model.Gestion.Implementation;
using GestionMaterielIntExt.Model.Gestion.Interface;
using GestionMaterielIntExt.ModelsOfView.Connexion.Implementation;
using GestionMaterielIntExt.ModelsOfView.Connexion.Interface;
using GestionMaterielIntExt.ModelsOfView.Consultation.Implementation;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;
using GestionMaterielIntExt.ModelsOfView.Gestion.Implementation;
using GestionMaterielIntExt.ModelsOfView.Gestion.Interface;
using GestionMaterielIntExt.Proxy.Connexion.Implementation;
using GestionMaterielIntExt.Proxy.Connexion.Interface;
using GestionMaterielIntExt.Proxy.Consultation.Implementation;
using GestionMaterielIntExt.Proxy.Consultation.Interface;
using GestionMaterielIntExt.Proxy.Gestion.Implementation;
using GestionMaterielIntExt.Proxy.Gestion.Interface;
using GestionMaterielIntExt.Service;
using GestionMaterielIntExt.View.Connexion.Implementation;
using GestionMaterielIntExt.View.Consultation.Implementation;
using GestionMaterielIntExt.View.Consultation.Interface;
using GestionMaterielIntExt.View.Gestion.Interface;
using Microsoft.Extensions.DependencyInjection;  // provides DI

namespace GestionMaterielIntExt;

internal static class Program
{
    // Soltion pour l'DI : 
    // https://stackoverflow.com/questions/47516409/using-microsoft-extension-dependency-injection-on-winforms-in-c-sharp

    public static IServiceProvider ServiceProvider { get; set; }

    static void ConfigureServices()
    {
        var services = new ServiceCollection();
        //Connexion
        services.AddTransient<ViewConnexion>();
        services.AddTransient<IMViewConnexion, MViewConnexion>(); // Sigleton
        services.AddTransient<IControllerConnexion, ControllerConnexion>();
        services.AddTransient<IModelConnexion, ModelConnexion>();
        services.AddTransient<IProxiConnexion, ProxiConnexion>();
        //Consultation Marériel
        //services.AddTransient<ViewConsultation>();
        services.AddTransient<IViewConsultation, ViewConsultation>(); // Sigleton
        services.AddTransient<IMViewConsultation, MViewConsultation>();
        services.AddTransient<IControllerConsultation, ControllerConsultation>();
        services.AddTransient<IModelConsultation, ModelConsultation>();
        services.AddTransient<IProxiConsultation, ProxiConsultation>();
        //Gestion Matériel
        //services.AddTransient<ViewGestion>();
        services.AddTransient<IViewGestion, ViewGestion>(); // Sigleton 
        services.AddTransient<IMViewGestionMateriel, MViewGestionMateriel>();
        services.AddTransient<IControllerGestionMetariel, ControllerGestionMetariel>();
        services.AddTransient<IModelGestionMateriels, ModelGestionMateriels>();
        services.AddTransient<IProxiGestionMateriel, ProxiGestionMateriel>();
        services.AddTransient<IMessageBoxService, MessageBoxService>();
        // Gestion Contrat maintenance
        services.AddTransient<IControllerGestionCMaintenance, ControllerGestionCMaintenance>();
        services.AddTransient<IModelGestionCMaintenance, ModelGestionCMaintenance>();
        services.AddTransient<IProxiGestionCMaintenance, ProxiGestionCMaintenance>();
        // Gestion Entreprise
        services.AddTransient<IControllerGestionEntreprise, ControllerGestionEntreprise>();
        services.AddTransient<IModelGestionEntreprise, ModelGestionEntreprise>();
        services.AddTransient<IProxiGestionEntreprise, ProxiGestionEntreprise>();

        ServiceProvider = services.BuildServiceProvider();
    }
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        ConfigureServices();
        // point d'entrée des injections de dépendance, ne peut être une interface
        // dette technique, pas interfac possible
        Application.Run(ServiceProvider.GetRequiredService<ViewConnexion>());
    }
}