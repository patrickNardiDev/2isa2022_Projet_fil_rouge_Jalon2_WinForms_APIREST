using Dapper;
using Domain.Entities;
using GestionMaterielIntExt.Controller;
using GestionMaterielIntExt.Model;
using MySql.Data.MySqlClient;

namespace GestionMaterielIntExt.Elmt.Model
{
    internal class UserModel
    {
        internal SingletonConnexion connexion = SingletonConnexion.Instance;


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                connexion.MyConnect.Open();
                var myQuery = "SELECT id, name, firstname FROM users;";
                var result = await connexion.MyConnect.QueryAsync<User>(myQuery);
                return result;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("la connexion au serveur est impossible");
                return new List<User>();
            }
            finally { connexion.MyConnect.Close(); }
        }
    }
}
