using Domain.ValueObjects;

namespace GestionMaterielIntExt.Elmt.Controller
{
    internal class VueMaterielController
    {

        internal List<MaterielsInfos> ListMateriels = new();

        /// <summary>
        /// /// Controler de lecture de la vue matériel asynchrone
        /// </summary>
        /// <returns></returns>
        internal async Task<List<MaterielsInfos>> AfficheVueMaterielAsync()
        {
            try
            {
                // récupération de l'élément sélectionné
                // Ok mais classe ancienne
                //var VueMateriel = new VueMaterielModel();
                //var myListVueMateriels = await VueMateriel.GetVueMaterielAsync(null);
                //if (myListVueMateriels != null)
                //{
                //    // j'ajoute les éléments dans la liste
                //    foreach (var materiel in myListVueMateriels)
                //    {
                //        ListMateriels.Add(materiel);
                //    }
                //}
                return ListMateriels;
            }
            catch
            {
                string msg = "Actualisation impossible, veuillez voir avec le service informatique";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ListMateriels;
            }
        }

        internal async Task<List<MaterielsInfos>> AfficheVueMaterielFiltreAsync(int index)
        {
            try
            {
                // récupération de l'élément sélectionné
                //Injection
                //HttpRequestOptionsKey mais classe ancienne
                //var VueMateriel = new VueMaterielModel();
                //var myListVueMateriels = await VueMateriel.GetVueMaterielAsync(index);
                //if (myListVueMateriels != null)
                //{
                //    // j'ajoute les éléments dans la liste
                //    foreach (var materiel in myListVueMateriels)
                //    {
                //        ListMateriels.Add(materiel);
                //    }
                //}
                return ListMateriels;
            }
            catch
            {
                string msg = "Actualisation impossible, veuillez voir avec le service informatique";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ListMateriels;
            }
        }


        internal bool DestroyMateriel(int id)
        {

            return true;
        }


    }
}
