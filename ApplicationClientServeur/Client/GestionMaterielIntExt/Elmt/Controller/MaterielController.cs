using Domain.Entities;
using GestionMaterielIntExt.Elmt.Factory;
using GestionMaterielIntExt.Elmt.Model;

namespace GestionMaterielIntExt.Elmt.Controller
{
    internal class MaterielController
    {
        //TODO pour test
        //internal IEnumerable<Materiel> GetFactos()
        //{
        //    Materiel materiel1 = FactoMateriel.MackFactM();
        //    Materiel materiel2 = FactoMateriel.MackFactM();
        //    Materiel materiel3 = FactoMateriel.MackFactM();
        //    Materiel materiel4 = FactoMateriel.MackFactM();

        //    return new List<Materiel>() { materiel1, materiel2, materiel3, materiel4 };
        //}

        #region Ajouter
        public async Task<Materiel> AjouterMaterielAsync(Materiel materiel)
        {
            //todo vérifi si exite pas déjà en BD ?? NON car ID fait la diférence pas le reste
            // plusieurs mêmes matériels
            Materiel materielResult = new();
            MaterielModel materielModel = new();
            materielResult = await materielModel.PostMaterielAsync(materiel);
            return materielResult;
        }
        #endregion

    }
}

//         DateTime IMaterielFactory.GetDateFinGarantie()
// namespace GestionMaterielIntExt.Elmt.Factory

