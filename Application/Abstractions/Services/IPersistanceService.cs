using Application.Responses;
using Microsoft.Data.SqlClient;

namespace Application.Abstractions.Services;
public interface IPersistanceService<T> where T : class
{
    Task<PaginatedResult<List<T>>> ExecuteCollectionQueryAsync(
    string sql,
    List<SqlParameter> parameters,
    CancellationToken cancellationToken);

    Task<T> ExecuteNonCollectionQueryAsync(
        string sql,
        SqlParameter[] parameters,
        CancellationToken cancellationToken);

    Task<dynamic> ExecuteCommandAsync(string sql,
        List<SqlParameter> parameters,
        CancellationToken cancellationToken);
}