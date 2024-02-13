using DAL.Repository.Interface;

namespace DAL
{
    public interface IUOW : IDisposable
    {
        // injection de dépendance par mutateur
        public IUserRepository User { get; }
        public IMaterielsInfosRepository MaterielsInfos { get; }
        public ICategorieRepository Categorie { get; }

        public IMaterielRepository Materiel { get; }

        public IEntrepriseRepository Entreprise { get; }

        public IContratMaintenanceRepository ContratMaintenance { get; }

        public IMaterielCategorieRepository MaterielCategorie { get; }

        public IMaterielDetruitRepository MaterielDetruit { get; }


        //Transactions
        /// <summary>
        /// Begin a transaction on the current connection
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit the current transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        void Rollback();
    }
}
