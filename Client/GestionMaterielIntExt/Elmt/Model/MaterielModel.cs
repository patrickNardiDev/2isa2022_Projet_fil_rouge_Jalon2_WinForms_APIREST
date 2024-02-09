using Dapper;
using Domain.Entities;
using GestionMaterielIntExt.Model;
using MySql.Data.MySqlClient;

// using GestionMaterielIntExt.Elmt.Entity;

namespace GestionMaterielIntExt.Elmt.Model
{
    internal class MaterielModel
    {

        internal SingletonConnexion connexion = SingletonConnexion.Instance;
        internal MySqlTransaction transaction;

        #region Get --------------------------------------------
        public async Task<IEnumerable<Materiel>> GetMaterielAsync()
        {
            try
            {
                connexion.MyConnect.Open();
                var myQuery = "SELECT Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive  FROM D_MATERIEL;";
                var result = await connexion.MyConnect.QueryAsync<Materiel>(myQuery);
                return result;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("la connexion au serveur est impossible");
                return new List<Materiel>();
            }
            finally { connexion.MyConnect.Close(); }
        }

        public async Task<IEnumerable<Materiel>> GetMaterielAsync(int id)
        {
            try
            {
                connexion.MyConnect.Open();
                var myQuery = "SELECT Id, Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat, Archive  FROM D_MATERIEL WHERE Id = @id LIMIT 1;";
                var result = await connexion.MyConnect.QueryAsync<Materiel>(myQuery, new { id });
                return result;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("la connexion au serveur est impossible");
                return new List<Materiel>();
            }
            finally { connexion.MyConnect.Close(); }
        }
        #endregion

        #region POST --------------------------------------------
        public async Task<Materiel> PostMaterielAsync(Materiel materiel)
        {
            //(string nom, DateTime dateMiseEnService, DateTime dateFinGarantie, int? idUser, int? idMContrat)
            var connexion = new SingletonConnexion();
            try
            {
                connexion.MyConnect.Open();
                transaction = connexion.MyConnect.BeginTransaction();
                var q = "INSERT INTO D_MATERIEL (Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat) VALUES (@nom, @dateMiseEnService, @dateFinGarantie, @idUser, @idMContrat); SELECT LAST_INSERT_ID()";
                var resultId = await connexion.MyConnect.QueryFirstAsync<int>(q, materiel);
                var resultListMateriel = await GetMaterielAsync(resultId);
                //var result = await connexion.MyConnect.QueryAsync<int>(q, new { nom, dateMiseEnService, dateFinGarantie, idUser, idMContrat });
                transaction.Commit();
                return resultListMateriel.ToArray()[0];
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                //MessageBox.Show("la connexion au serveur est impossible");
                MessageBox.Show("la connexion au serveur est impossible, message : {0}", e.Message.ToString());
                return new Materiel();
            }
            finally { connexion.MyConnect.Close(); }
        }
        #endregion


        // Id bigint(20) unsigned not null unique auto_increment,
        // Nom varchar(50) not null CHECK (Nom <> ''),
        // DateMiseEnService Datetime,
        // DateFinGarantie Datetime,
        // IdUser bigint(20) unsigned,
        // IdMContrat bigint(20) unsigned,
        // Archive boolean not null default false,

        #region PUT --------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="dateMiseEnService"></param>
        /// <param name="dateFinGarantie"></param>
        /// <param name="idUser"></param>
        /// <param name="idMContrat"></param>
        /// <returns></returns>
        public async Task<int> PutMateriel(string nom, DateTime dateMiseEnService, DateTime dateFinGarantie, int? idUser, int? idMContrat)
        {
            var connexion = new SingletonConnexion();

            try
            {
                connexion.MyConnect.Open();
                var q = "UPDATE D_MATERIEL SET Nom = @nom, DateMiseEnService = @dateMiseEnService, DateFinGarantie = @dateFinGarantie, IdUser = @idUser, IdMContrat = @idMContrat, Archive = @archive   WHERE Id = @id";
                var result = await connexion.MyConnect.ExecuteAsync(q, new { nom, dateMiseEnService, dateFinGarantie, idUser, idMContrat });
                return result;
            }
            finally { connexion.MyConnect.Close(); }
        }
        #endregion

        #region Delete --------------------------------------------

        #endregion


        //       table D_MATERIEL_DETRUIT(
        //           Id bigint(20) unsigned not null unique auto_increment,
        //           Nom varchar(50) not null CHECK(Nom<> ''),
        //   OldNumMarquage bigint(20) unsigned not null,
        //   DateMiseEnService Datetime,
        //DatedESTRUCTION Datetime default sysdate(),
        //   IdMContrat bigint(20) unsigned,

        //public async Task<int> DeleteArticleIfNotComposition(int id)
        //{
        //var connexion = new SingletonConnexion();

        //try //TODO
        //{
        //    connexion.MyConnect.Open();
        //    var myQuery = "SELECT Id,Nom, NomCat, DateMiseEnService, DateFinGarantie, IdUser, User, IdMContrat, NomContrat, Archive, IdCat FROM VueMateriel;";

        //    var result = connexion.MyConnect.QueryAsync<VueMateriel>(myQuery);


        //    var q = "DELETE from D_MATERIEL WHERE Id = @id AND NOT EXISTS (SELECT Id FROM Materiel where Id = @id)";
        //    var result = await connexion.MyConnect.ExecuteAsync(q, new { id });

        //    q = "INSERT INTO D_MATERIEL_DETRUIT (Nom, DateMiseEnService, DateFinGarantie, IdUser, IdMContrat) VALUES (@nom, @dateMiseEnService, @dateFinGarantie, @idUser, @idMContrat); SELECT LAST_INSERT_ID()";
        //    var result2 = await connexion.MyConnect.QueryAsync<int>(q, new { nom, dateMiseEnService, dateFinGarantie, idUser, idMContrat });
        //    return result;
        //}
        //finally { connexion.MyConnect.Close(); }
        //}


    }
}









