using DAL.Repository.Implementation.MariaDB;
using DAL.Repository.Interface;
using DAL.Session.Implementation;
using DAL.Session.Interface;
using Domain.Exeption;

namespace DAL;
internal class UOW : IUOW
{
    // injection de dépendance par constructeur
    private readonly IDBSession db;

    // injection de dépendance par mutateur
    public IUserRepository User { get; init; }// => new UserRepositoryMariaDB(db);
    public IMaterielsInfosRepository MaterielsInfos { get; init; }// => new MaterielsInfosRepositoryMariaDB(db);
    public ICategorieRepository Categorie { get; init; }// => new CategorieRepositoryMariaDB(db);

    public IMaterielRepository Materiel { get; init; }// => new MaterielRepositoryMariaDB(db);

    public IEntrepriseRepository Entreprise { get; init; }// => new EntrepriseRepositoryMariaDB(db);

    public IContratMaintenanceRepository ContratMaintenance { get; init; }// => new ContratMaintenanceRepositoryMariaDB(db);

    public IMaterielCategorieRepository MaterielCategorie { get; init; }//=> new MaterielCategorieRepositoryMariaDB(db);

    public IMaterielDetruitRepository MaterielDetruit { get; init; }// => new MaterielDetruitRepositoryMariaDB(db);

    public UOW(IDBSession dBSession)
    {
        db = dBSession;
        if (dBSession.GetType() == typeof(DBSessionMariaDB)) 
        {

            User = new UserRepositoryMariaDB(db);
            MaterielsInfos = new MaterielsInfosRepositoryMariaDB(db);
            Categorie = new CategorieRepositoryMariaDB(db);
            Materiel = new MaterielRepositoryMariaDB(db);
            Entreprise = new EntrepriseRepositoryMariaDB(db);
            ContratMaintenance = new ContratMaintenanceRepositoryMariaDB(db);
            MaterielCategorie = new MaterielCategorieRepositoryMariaDB(db);
            MaterielDetruit = new MaterielDetruitRepositoryMariaDB(db);
        }
        else
        {
            throw new SysException("Le type de connexion à la base de données que vous avez définit en paramètre, n'est pas implémenté.");
        }
    }


    public void BeginTransaction()
    {
        var a = typeof(DBSessionMariaDB);
        if (db.Transaction is null)
        {
            db.Transaction = db.Connection.BeginTransaction();
        }
    }

    public void Commit()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Commit();
            db.Transaction = null;
        }
    }

    public void Dispose()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Dispose();
            db.Transaction = null;
        }
        db.Connection.Dispose();
    }

    public void Rollback()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Rollback();
            db.Transaction = null;
        }
    }
}
