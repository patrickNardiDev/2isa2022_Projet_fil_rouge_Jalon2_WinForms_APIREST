using FluentValidation;

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
    //private Regex regexMailPattern = new Regex(patternMail);
    public ConnexionRequestDtoValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("votre courriel ne peut pas être vide")
            .EmailAddress().WithMessage("entrez une adresse de courriel valide")
            .NotNull().WithMessage("Ne preut pas être nul");
        //.Must(Email => Email.Contains("@amio") == true);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("le mot-de-passe ne peut pas être vide")
            .MinimumLength(5).WithMessage("Longueur minimun de 5 carractères")
            .NotNull().WithMessage("le mot-de-passe-ne peut pas être nul");
    }
}