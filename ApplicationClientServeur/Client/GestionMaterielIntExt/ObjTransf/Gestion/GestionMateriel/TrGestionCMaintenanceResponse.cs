using Domain.Exeption;
using Domain.ValueObjects;

namespace GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

public class TrGestionCMaintenanceResponse
{
    public long Id { get; set; } = -1;
    public IEnumerable<CMaintenanceGestion> EnumCMaintenanceGestions { get; set; } = new List<CMaintenanceGestion>();


    public SysException Exp { get; set; }


    public string ErrorMessage { get; set; } = null;

    public TrGestionCMaintenanceResponse(SysException exeption) : base()
    {
        Exp = exeption;
    }
    public TrGestionCMaintenanceResponse() { }
}
