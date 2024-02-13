using Domain.Exeption;
using Domain.ValueObjects;

namespace GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

public class TrGestionEntrepriseResponse
{
    public long Id { get; set; } = -1;
    public IEnumerable<EntrepriseGestion> EnumEntrepriseGestions { get; set; } = new List<EntrepriseGestion>();


    public SysException Exp { get; set; }


    public string ErrorMessage { get; set; } = null;

    public TrGestionEntrepriseResponse(SysException? exeption) : base()
    {
        Exp = exeption;
    }
    public TrGestionEntrepriseResponse() { }
}
