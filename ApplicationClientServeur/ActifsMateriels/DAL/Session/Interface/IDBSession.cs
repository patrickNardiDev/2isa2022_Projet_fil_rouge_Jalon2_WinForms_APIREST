using System.Data;

namespace DAL.Session.Interface;

internal interface IDBSession : IDisposable
{
    internal IDbConnection Connection { get; }
    internal IDbTransaction Transaction { get; set; }
}
