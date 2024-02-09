using DAL.Session.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Session.Implementation
{

    internal class DBSessionMariaDB : IDBSession, IDisposable
    {
        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; set; }

        public DBSessionMariaDB(string connString)
        {
            Connection = new MySqlConnection(connString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
