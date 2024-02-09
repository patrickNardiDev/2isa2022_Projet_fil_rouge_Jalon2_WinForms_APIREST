using Domain.Exeption;
using Domain.ValueObjects;

namespace Domain.DTO.Gestion;

public class GestionCMaintenanceResponseDto
{
    #region properties
    public long Id { get; set; } = -1;
    /// <summary>
    /// liste de contrat infos
    /// </summary>
    public IEnumerable<CMaintenanceGestion> EnumCMaintenanceInfos { get; set; } = new List<CMaintenanceGestion>();

    public string ErrorMessage { get; set; } = null;
    public SysException Exp { get; set; } = null;
    #endregion


    #region constructor
    public GestionCMaintenanceResponseDto() { }

    public GestionCMaintenanceResponseDto(SysException exeption) : base()
    {
        Exp = exeption;
    }
    #endregion
}
