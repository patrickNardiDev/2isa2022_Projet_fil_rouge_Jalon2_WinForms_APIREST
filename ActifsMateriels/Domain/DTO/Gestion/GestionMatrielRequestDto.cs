using FluentValidation;
using static Domain.DTO.Gestion.CRUDtype;

namespace Domain.DTO.Gestion;

public class GestionMatrielRequestDto : DtoAbstract
{
    public MyType CRUDtype { get; set; } = MyType.Read;
    public string NewNomMat { get; set; } = string.Empty;
    public DateTime? NewDMService { get; set; } = null;
    public DateTime? NewDFGarantie { get; set; } = null;
    public long? NewIdUser { get; set; } = null;
    public long? NewIdCMaintenance { get; set; } = null;
    public bool NewArchive { get; set; } = false;
    public List<long> NewListIdCategories { get; set; } = new List<long>();

    public long? OldIdMat { get; set; } = null;

    public DateTime LastModif { get; set; } = DateTime.MinValue;


    public GestionMatrielRequestDto()
    {
    }

    public class GestionMatrielRequestDtoValidator : AbstractValidator<GestionMatrielRequestDto>
    {
        public GestionMatrielRequestDtoValidator()
        {
            RuleFor(x => x.CRUDtype).NotNull().NotEmpty();
            RuleFor(x => x.NewNomMat).NotEmpty().WithMessage("Le nom du matériel ne peut pas être vide ");
            RuleFor(x => x.NewArchive).NotNull();
            RuleFor(x => x.NewListIdCategories).NotNull().WithMessage("Un matériel doit avoir au moins une catégorie associée");
        }
    }
}



