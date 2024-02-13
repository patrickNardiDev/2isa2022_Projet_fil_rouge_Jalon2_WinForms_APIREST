using Domain.DTO.Connexion;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Consultation
{
    public class ConsultationRequestDto : DtoAbstract
    {
        public long Categorie { get; set; }
        public ConsultationRequestDto(){}
        public ConsultationRequestDto(long categorie) => Categorie = categorie;

    }

    public class ConsultationRequestDtoValidator : AbstractValidator<ConsultationRequestDto>
    {
        public ConsultationRequestDtoValidator()
        {
            RuleFor(x => x.Categorie)
                .NotNull().WithMessage("la catégorie ne peut être nulle\n\r")
                .NotEmpty().WithMessage("la catégorie ne peut être vide\n\r");
        }
    }
}
