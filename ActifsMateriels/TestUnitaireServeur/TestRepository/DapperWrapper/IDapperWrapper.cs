using System.Data;

namespace TestUnitaireServeur.TestRepository.DapperWrapper;

/// <summary>
/// Iterface permettant de Mocker les requête avec Dapper
/// https://makolyte.com/csharp-how-to-unit-test-code-that-uses-dapper/#1_-_Wrap_the_Query_method
/// </summary>
public interface IDapperWrapper
{
    public Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param);
}
