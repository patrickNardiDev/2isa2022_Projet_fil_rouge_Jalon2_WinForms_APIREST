using Dapper;
using Domain.ValueObjects;
using GestionMaterielIntExt.Model;
using MySql.Data.MySqlClient;

namespace GestionMaterielIntExt.Elmt.Model
{
    internal class VueMaterielModel
    {
        internal SingletonConnexion connexion = SingletonConnexion.Instance;
        //public async Task<IEnumerable<VueMateriel>> GetVueMaterielAsync()
        //{
        //    try
        //    {
        //        connexion.MyConnect.Open();
        //        var myQuery = "SELECT Nom, DateMiseEnService, DateFinGarantie, User, Id, NomCat, Contrat, Archive,  IdUser,  IdMContrat, NomContrat, IdCat, UserWithId FROM VueMateriel;";
        //        var result = await connexion.MyConnect.QueryAsync<VueMateriel>(myQuery);
        //        return result;
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show("la connexion au serveur est impossible");
        //        return new List<VueMateriel>();
        //    }
        //    finally { connexion.MyConnect.Close(); }
        //}

        public async Task<IEnumerable<MaterielsInfos>> GetVueMaterielAsync(int? idCat)
        {
            try
            {
                var a = connexion.MyConnect;
                connexion.MyConnect.Open();
                var myQuery = "SELECT Nom, DateMiseEnService, DateFinGarantie, User, Id, NomCat, Contrat, Archive,  IdUser,  IdMContrat, NomContrat, IdCat, UserWithId FROM VueMateriel;";
                var result = await connexion.MyConnect.QueryAsync<MaterielsInfos>(myQuery);
                if (idCat is null)
                {
                    return result;
                }
                else
                {
                    result = result.Where(x => x.IdCat == idCat);
                    return result;
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show("la connexion au serveur est impossible");
                return new List<MaterielsInfos>();
            }
            finally { connexion.MyConnect.Close(); }
        }

        //public async Task<IEnumerable<VueMateriel>> GetFiltreMaterielAsync(int idCategorie)
        //{
        //    try
        //{
        //        connexion.MyConnect.Open();
        //        var q = "SELECT Nom, DateMiseEnService, DateFinGarantie, User, Id, NomCat, Contrat, Archive,  IdUser,  IdMContrat, NomContrat, IdCat FROM VueMateriel Where IdCat = @idCategorie;";
        ////var q = "SELECT Id,Nom, DateMiseEnService, IdMContrat, IdCat FROM VueMateriel Where IdCat = @idCategorie;";
        //var result = await connexion.MyConnect.QueryAsync<VueMateriel>(q, new { idCategorie });
        //        return result;
        //}
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show("la connexion au serveur est impossible");
        //        return new List<VueMateriel>();
        //    }
        //    finally { connexion.MyConnect.Close(); }
        //}



    }
}
