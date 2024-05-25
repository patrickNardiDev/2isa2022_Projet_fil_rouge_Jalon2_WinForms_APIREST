
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace TestIntegrations.Fixtures;

/// <summary>
/// permet de créer un serveur de test
/// </summary>
public class APIWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; set; }

    // Gives a fixture with new configuration for the application before it gets built.
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Integrations.json")
                .Build();
            config.AddConfiguration(Configuration);
        });
    }
}
