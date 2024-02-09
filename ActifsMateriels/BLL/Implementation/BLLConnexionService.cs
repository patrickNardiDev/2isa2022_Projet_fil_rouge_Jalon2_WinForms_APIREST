using BLL.Interface;
using DAL;
using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Entities.Role;

namespace BLL.Implementation;

internal class BLLConnexionService : IBLLConnexionService
{
    // injection de dépendance
    private readonly IUOW _dbContext;

    private readonly IBCrypService _bcrypService;
    private IConfiguration _configuration { get; }

    public BLLConnexionService(IUOW dbContext, IConfiguration configuration, IBCrypService bcrypService)
    {
        this._dbContext = dbContext;
        this._configuration = configuration;
        this._bcrypService = bcrypService;
    }

    #region connexion
    public async Task<UserTransit> ConnexionUserAsync(string email, string password)
    {
        // 1- j'appel la fonction GetByEmail du _dbContext en lui passant en argument l'email pour récupérer un user
        // 2- je vérifie si le user n'est pas null
        //  2.1 si non nule alors :
        //      2.1.1 je vérifie la correspondance entre le password fournit et celui reçu par la fonction GetByEmail du _dbContext. 4
        //              Le hashage du password est réalisé par la fonction Verify()
        //          2.1.2.1 si vrai alors :
        //               2.1.2.1.1 je définis son role suivant l'enum et l'id de role
        //               2.1.2.1.3 je construits un UserTransit
        //               2.1.2.1.3 je retourne le UserTransit
        //          2.1.2.2 si non
        //               2.1.2.2.1 je lève une exeption personnelle

        var user = await _dbContext.User.GetByEmailAsync(email);
        if (user is null) throw new UnauthorizedAccessException(); ;
        bool verified = _bcrypService.Verify(password, user.password);

        // aucune correspondance trouvée 
        if (!verified) throw new UnauthorizeException();// 1 TestU Flase => UnauthorizeException

        // définition du role suivant l'énum, + est connecté ou non
        bool UserIsConnected = false;
        Role.RoleEnum myRole;
        //var a = Role._List.ToList().Contains((Role.RoleEnum)(user.idSIRole));
        if (user.idSIRole is null) { user.idSIRole = 3; } // 3 TestU si Role null =>  UserTransit correspondant
        if (user.idSIRole == Convert.ToInt32(RoleEnum.Consultation) || user.idSIRole == Convert.ToInt32(RoleEnum.Gestion)) // 2 TestU si Role Consultation => UserTransit correspondant
        {
            UserIsConnected = true;
            myRole = (Role.RoleEnum)user.idSIRole.Value;
        }
        else // 3 TestU si ni consultation ni gestion (noRole et ou null ?) => UserTransit correspondant
        {
            UserIsConnected = false;
            myRole = RoleEnum.noRole;
        }


        string myToken = String.Empty;
        if (myRole == Role.RoleEnum.Gestion)
            myToken = await Task.FromResult(GenerateJwtToken(user.id.ToString(), new List<string>() { "Admin", "User" })); // 5 TestU si role gestion => Token avec les role admin et user

        else if (myRole == Role.RoleEnum.Consultation)
            myToken = await Task.FromResult(GenerateJwtToken(user.id.ToString(), new List<string>() { "User" })); // 6 TestU si role consultation => Token avec les role user

        else
            myToken = await Task.FromResult(GenerateJwtToken(user.id.ToString(), new List<string>() { })); // 7 TestU si ni role gestion ni role consultaion (noRole et ou null ?) => Token sans role

        return new UserTransit() // 8 TestU => correspondance entre UserTransit supposé et réalisé
        {
            Id = user.id,
            NameFormatted = user.firstname[0] + ". " + user.name,
            Email = user.email,
            Role = myRole,
            AccessToken = myToken,
            IsConnected = UserIsConnected,
            SettingType = _configuration["SettingType"],
        };
    }


    public virtual string GenerateJwtToken(string idUser, List<string> roles) // virtualisée pour être Mockée dans les TestU
    {
        //var a = Parameters;
        //Add User Infos
        var claims = new List<Claim>(){
               new Claim(JwtRegisteredClaimNames.Sub, idUser), //ID User (Subject)
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //ID Token
               new Claim(ClaimTypes.NameIdentifier, idUser) //ID User (NameIdentifier)  == (Subject)
           };

        //Add Roles
        roles.ForEach(role =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        });

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwToken:JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Expiration time
        //var expires = DateTime.Now.AddDays(Convert.ToDouble(myJwToken.JwtExpireDays));
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwToken:JwtExpireDays"]));

        //Create JWT Token Object
        var token = new JwtSecurityToken(
            _configuration["JwToken:JwtIssuer"],
            _configuration["JwToken:JwtIssuer"],
            claims,
            expires: expires,
            signingCredentials: creds
        );
        //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion

}
