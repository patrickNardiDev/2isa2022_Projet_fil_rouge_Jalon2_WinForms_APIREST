using Domain.Entities;
using GestionMaterielIntExt.Elmt.Model;

namespace GestionMaterielIntExt.Elmt.Controller
{
    public class CategorieController
    {

        public List<String> ListStringCategories = new();
        public List<Categorie> ListCategories = new();

        /// <summary>
        /// /// Controler de lecture de la vue matériel asynchrone
        /// </summary>
        /// <returns></returns>
        public async Task<List<String>> ListStringCategoriesAsync()
        {
            try
            {
                ListStringCategories = new();
                // récupération de l'élément sélectionné
                //var myCategoriesModel = new CategorieModel();
                //var myListCategories = await myCategoriesModel.GetCategoriesAsync();
                //if (myListCategories != null)
                //{
                //    // j'ajoute les éléments dans la liste
                //    foreach (var categorie in myListCategories)
                //    {
                //        ListStringCategories.Add($"{categorie.Id}-{categorie.Nom}");
                //    }
                //}
                return ListStringCategories;
            }
            catch
            {
                string msg = "Actualisation impossible, veuillez voir avec le service informatique";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ListStringCategories;
            }
        }

        public async Task<List<Categorie>> ListCategoriesAsync()
        {
            try
            {
                // récupération de l'élément sélectionné
                //var myCategoriesModel = new CategorieModel();
                //var myListCategories = await myCategoriesModel.GetCategoriesAsync();
                //ListCategories = new List<Categorie>(myListCategories);

                return ListCategories;
            }
            catch
            {
                string msg = "Actualisation impossible, veuillez voir avec le service informatique";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ListCategories;
            }
        }
    }
}
