using GestionMaterielIntExt.ObjTransf.Context;
using static GestionMaterielIntExt.ObjTransf.Gestion.TrCRUDtype;

namespace GestionMaterielIntExt.ObjTransf.Gestion.GestionMateriel;

public class TrGestionMaterielRequest
{
    public TrCRUDtype.MyType CRUDtype { get; set; } = MyType.Read;
    public string NewNomMat { get; set; } = string.Empty;
    public DateTime? NewDMService { get; set; } = DateTime.MinValue;
    public DateTime? NewDFGarantie { get; set; } = DateTime.MinValue;
    public long? NewIdUser { get; set; } = null;
    public long? NewIdCMaintenance { get; set; } = null;
    public bool NewArchive { get; set; } = false;
    public List<long> NewListIdCategories { get; set; } = new List<long>();

    public long? OldIdMat { get; set; } = null;

    public DateTime LastModif {  get; set; } = DateTime.MinValue;

    public TrContextTabMateriel ValueContext { get; set; } = null;

    public TrGestionMaterielRequest()
    {
    }
}
