using Domain.Entities;
using Domain.ValueObjects;
using GestionMaterielIntExt.Elmt.Controller;
using System.ComponentModel;

namespace GestionMaterielIntExt
{
    internal class FuncMaterielForms
    {
        #region Actualiser
        /// <summary>
        /// Actualiser un BindingSorce t suivant une BindingList<VueMateriel>
        /// </summary>
        /// <typeparam name="T">BindingSorce</typeparam>
        /// <typeparam name="U">BindingList<VueMateriel></typeparam>
        /// <param name="t">BindingSorce</param>
        /// <param name="u">BindingList<VueMateriel></param>
        /// <returns></returns>
        public async Task ActualiserVueMaterielGAsync<T, U>(T t, U u) where T : BindingSource where U : BindingList<MaterielsInfos>
        {
            // je récupère la position actuelle de sélection
            MaterielsInfos current = t.Current as MaterielsInfos;

            // je remplie ma liste
            VueMaterielController myVueMaterielControler = new();
            List<MaterielsInfos> liste = await myVueMaterielControler.AfficheVueMaterielAsync();
            u.Clear();
            foreach (var item in liste)
            {
                u.Add(item);
            }

            // On se repositionne sur le current
            if (current is not null)
                t.Position = u.IndexOf(u.Where(u => u.IdMat == current.IdMat).FirstOrDefault());
        }
        #endregion

        #region Remplir Combobox
        public delegate Task<List<string>> DCbxUserWithId();
        public delegate Task<List<string>> DCbxCategorie();



        /// <summary>
        /// Remplir soit une ou deux ComboBox t et u avec la liste des Catégories matériels
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="dCbxUserWithId"></param>
        /// <param name="dCbxCategorie"></param>
        public async Task RemplirComboboxCategorieAsync<T>(T t) where T : ComboBox
        {
            CategorieController myCategorieControler = new();
            List<Categorie> liste = await myCategorieControler.ListCategoriesAsync();
            t.ResetText();
            foreach (var item in liste)
            {
                t.Items.Add(item);
            }
            t.DisplayMember = "Nom"; // définit quoi afficher
        }

        public async Task RemplirComboboxUserAsync<T>(T t) where T : ComboBox
        {
            UserController myUserControler = new();
            List<string> liste = await myUserControler.ListUsersAsync();
            t.ResetText();
            foreach (var item in liste)
            {
                t.Items.Add(item);
            }
        }

        public async Task RemplirListboxCategoriesAsync<T>(T t) where T : ListBox
        {
            CategorieController myCategorieController = new();
            List<string> liste = await myCategorieController.ListStringCategoriesAsync();
            t.ResetText();
            foreach (var item in liste)
            {
                t.Items.Add(item);
            }
        }

        #endregion



        #region Rechercher
        /// <summary>
        /// Met à jour une BindingList<VueMateriel> suivant un critère de recherche issu d'une ComboBox t
        /// </summary>
        /// <typeparam name="T">ComboBox</typeparam>
        /// <typeparam name="U">BindingList<VueMateriel></typeparam>
        /// <param name="t">ComboBox</param>
        /// <param name="u">BindingList<VueMateriel></param>
        /// <returns></returns>
        public async Task RechercheParCategorieGAsync<T, U>(T t, U u) where T : ComboBox where U : BindingList<MaterielsInfos>
        {
            if (t.SelectedItem is not null)
            {
                int myIdCat = 0;
                var myCats = (t.SelectedItem as string).Split('-');
                myIdCat = int.Parse(myCats[0]);

                VueMaterielController vueMaterielControler = new();
                var ListFiltree = await vueMaterielControler.AfficheVueMaterielFiltreAsync(myIdCat);
                u.Clear();
                foreach (var item in ListFiltree)
                {
                    u.Add(item);
                }
            }
        }
        #endregion


    }
}
