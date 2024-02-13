using GestionMaterielIntExt.Elmt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMaterielIntExt.Elmt.Controller
{
    internal class UserController
    {
        internal List<String> ListUsers = new();

        public UserController() { }
        public async Task<List<String>> ListUsersAsync() 
        { 
            try
            {
                // récupération de l'élément sélectionné
                var myUserModel = new UserModel();
                var myListUsers = await myUserModel.GetUsersAsync();
                if (myListUsers != null)
                {
                    // j'ajoute les éléments dans la liste
                    foreach (var user in myListUsers)
                    {
                        ListUsers.Add($"{user.id} - {user.firstname[0]}. {user.name}");
                    }
                }
                return ListUsers;
            }
            catch
            {
                string msg = "Actualisation impossible, veuillez voir avec le service informatique";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ListUsers;
            }
        }
    }
}
