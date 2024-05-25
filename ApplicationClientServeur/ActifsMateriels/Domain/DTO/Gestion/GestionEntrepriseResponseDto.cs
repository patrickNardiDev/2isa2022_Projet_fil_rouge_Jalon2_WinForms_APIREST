using Domain.Exeption;
using Domain.ValueObjects;

namespace Domain.DTO.Gestion;

public class GestionEntrepriseResponseDto
{
    #region properties

    public long Id { get; set; } = -1;

    public IEnumerable<EntrepriseGestion> EnumEntrepriseInfos { get; set; } = new List<EntrepriseGestion>();

    public string ErrorMessage { get; set; } = null;
    public SysException Exp { get; set; }
    #endregion


    #region constructor
    public GestionEntrepriseResponseDto() { }

    public GestionEntrepriseResponseDto(SysException exeption) : base()
    {
        Exp = exeption;
    }
    #endregion
}
