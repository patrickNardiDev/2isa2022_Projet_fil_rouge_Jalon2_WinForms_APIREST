using Dapper;
using System.Data;

namespace TestUnitaireServeur.TestRepository.DapperWrapper;

public class DapperWrapper : IDapperWrapper
{
    public async Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }
}
