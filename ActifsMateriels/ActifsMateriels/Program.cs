using API.Filters;
using BLL;
using DAL;
using Domain.Exeption;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parameters;
using System.Reflection;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Parameters
var parameters = new Parameters.Parameters();
// la m�thode Bind permet de lier les donn�e aux valeurs de configuration en faisant correspondre les noms de propri�t�s aux cl�s de configuration de mani�re r�cursive. https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder.bind?view=dotnet-plat-ext-8.0
builder.Configuration.Bind(parameters);

//var connectionString = parameters.DBConnection.ConnectionString;
//var dbConnexion = parameters.DBConnection;
//var jwToken = parameters.JwToken;



builder.Services.TryAddSingleton<IParameters>(parameters);

// Add services to the container.
builder.Services.AddControllers(options =>
{
#if !DEBUG
    options.Filters.Add<ApiExceptionFilterAttribute>();
#endif
});

//r�cup�re le "DataBaseType" du fichier de configuration appsettings.json pour indiquer � l'UOW quelle base de donn�es emply�e en fonction de l'�num�ration DBType de DALExtension
//builder.Services.AddBLL(dbConnexion, jwToken);

/// GET DATA BASE TYPE
//int myTypeDataBase;
//var DataBaseIsDefine = Int32.TryParse(builder.Configuration["DataBaseType"], out myTypeDataBase);
//if (!DataBaseIsDefine)
//    throw new SysException("base de données non définit dans la configuration");
//var bllConnexionOptions = new BLLConnexionOptions();
////bllConnexionOptions.DataBasesTypeImplemented = new Dictionary<int, DBType>(myTypeDataBase);

//DBType myDataBaseType = DBType.MariaDB;
//switch (myTypeDataBase)
//{
//    case (int)DBType.MariaDB:
//        myDataBaseType = DBType.MariaDB;
//        break;
//    case (int)DBType.SQLServer:
//        myDataBaseType = DBType.SQLServer;
//        break;
//    case (int)DBType.PostgreSQL:
//        myDataBaseType = DBType.PostgreSQL;
//        break;
//    case (int)DBType.Oracle:
//        myDataBaseType = DBType.Oracle;
//        break;
//    default:
//        throw new SysException($"Le type de connexion à la base de données que vous avez définit en paramètre, ne fait pas partie des possibilités.");
//}
/// END GET DATA BASE TYPE


builder.Services.AddBLL();
//builder.Services.AddBLL(myDataBaseType);
//builder.Services.AddBLL(new BLLConnexionOptionsDelegate(bllConnexionOptions));

//Service for JWT Authentication
builder.Services
   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateIssuer = false,
           ValidateAudience = false,//TODO "api" : https://stackoverflow.com/questions/70597009/what-is-the-meaning-of-validateissuer-and-validateaudience-in-jwt

           ValidAudience = parameters.JwToken.JwtIssuer,
           //ValidAudience = builder.Configuration["JwtIssuer"],
           ValidIssuer = parameters.JwToken.JwtIssuer,
           //ValidIssuer = builder.Configuration["JwtIssuer"],
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(parameters.JwToken.JwtKey)),
           //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),

           //Retourne la diff�rence de temps maximale autoris�e entre le client et les param�tres de l'horloge du serveur.
           // temps d'expiration du token : 1j
           ClockSkew = TimeSpan.FromDays(1)// remove delay of token when expire
       };
   });



// validation des Dto de request par le package FluentValidation.AspNetCore
builder.Services.AddFluentValidation();

//builder.Services.AddFluentValidation(); // D�pr�ci�e


// Sawwger documentation
builder.Services.AddSwaggerGen((options) =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, true);

    options.AddSecurityDefinition("jwt", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Flows = new OpenApiOAuthFlows()
        {
            Password = new OpenApiOAuthFlow()
            {
                TokenUrl = new Uri("/api/connexion/loginSwagger", UriKind.Relative)
            }
        },
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "jwt" }
            },
            new string[] {}
        }
    });
});


// Ajout au dictionnaire de l'injecteur de d�pendance
// Point entr�e, la BLL. (une domaine m�tier)
// Elle ajoutera la connexion en fonction de la base de donn�e et le unit of work, UOW, pour la gestion de la session.


// end, ajout au dictionnaire de l'injecteur de d�pendance


var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

//Application for JWT Authentication
app.UseAuthentication();

// Configure the HTTP request pipeline.
app.UseAuthorization();

//middelware swagger
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "AMIO | API Actifs Matériels Informatique");
});

app.MapControllers();

app.Run();


public delegate void BLLConnexionOptionsDelegate(BLLConnexionOptions options);


//réalisé pour pourvoir l'injecté dans la WebApplicationFactory des tests d'intégration
public partial class Program { }

