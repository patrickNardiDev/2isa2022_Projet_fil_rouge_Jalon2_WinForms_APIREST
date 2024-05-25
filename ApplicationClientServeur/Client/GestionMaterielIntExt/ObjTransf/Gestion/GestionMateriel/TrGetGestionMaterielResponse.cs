using Domain.Entities;
using Domain.ValueObjects;
using GestionMaterielIntExt.ObjTransf;

namespace GestionActifsMateriels.ObjTransf.Gestion.GestionMateriel;

internal class TrGetGestionMaterielResponse
{
    public TrGetGestionMaterielResponse()
    {
    }

    internal IEnumerable<UserShort> UsersShort { get; set; }
    internal IEnumerable<Categorie> Categories { get; set; }
    internal IEnumerable<ContratMaintenance> ContratsMaintenance { get; set; }
    internal IEnumerable<MaterielGestion> MaterielsGestions { get; set; }
    internal IEnumerable<MaterielCategorie> MaterielsCategories { get; set; }
}
