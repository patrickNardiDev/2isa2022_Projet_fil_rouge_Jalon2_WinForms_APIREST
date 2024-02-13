using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.DTO.Connexion;


public class ConnexionRequestDto : DtoAbstract
{

    public string Email { get; set; }
    public string Password { get; set; }

    public ConnexionRequestDto() { }
    public ConnexionRequestDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class ConnexionRequestDtoValidator : AbstractValidator<ConnexionRequestDto>
{
    //EnvironmentVariableTarget myregexMail = @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[\w!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$";
    //private string myregexMail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    private Regex regexMailPattern = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
    public ConnexionRequestDtoValidator()
    {
        //Regex re = new Regex(myregexMail);
        //bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Votre courriel ne peut pas être vide\n\r")
            //.EmailAddress().WithMessage("Entrez une adresse de courriel valide\n\r")
            //.Must(Email => re.IsMatch(Email) == true).WithMessage("Entrez une adresse de mail valide\n\r")
            .Must(Email => regexMailPattern.IsMatch(Email) == true).WithMessage("Entrez une adresse de courriel valide\n\r")
            .NotNull().WithMessage("Ne preut pas être nul\n\r")
            .Must(Email => Email.Contains("@amio") == true).WithMessage("Vous n'êtes pas encore un AMIO. Devenez le https://amio-millau.fr/\n\r");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot-de-passe ne peut pas être vide\n\r")
            .MinimumLength(5).WithMessage("Longueur minimun de 5 carractères\n\r")
            .NotNull().WithMessage("Le mot-de-passe-ne peut pas être nul\n\r");
    }
}