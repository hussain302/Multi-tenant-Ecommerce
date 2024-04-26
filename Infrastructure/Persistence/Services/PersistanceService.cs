using System.Data;
using System.Data.Common;
using Application.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Contexts;
using Application.Abstractions.Services;

namespace Infrastructure.Persistence.Services;
public class PersistanceService<T, TDbContext>(
    TDbContext dbContext)
    : IPersistanceService<T> where T
    : class where TDbContext
    : ApplicationDbContext
{
    private readonly TDbContext _dbContext = dbContext;

    public async Task<PaginatedResult<List<T>>> ExecuteCollectionQueryAsync(
    string sql,
    List<SqlParameter> parameters,
    CancellationToken cancellationToken)
    {
        PaginatedResult<List<T>> collection = new PaginatedResult<List<T>>();
        var resultList = new List<T>();
        var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.StoredProcedure;

        foreach (var parameter in parameters)
            command.Parameters.Add(parameter);

        using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            while (await reader.ReadAsync(cancellationToken))
            {
                var entity = PersistanceService<T, TDbContext>.MapFromDataReader<T>(reader);
                resultList.Add(entity);
            }

        var outputParam = command.Parameters.Cast<SqlParameter>().FirstOrDefault(p => p.Direction == ParameterDirection.Output);
        if (outputParam != null && outputParam.Value != DBNull.Value)
            collection.ItemCount = (int)outputParam.Value;

        collection.Items = resultList;

        return collection;
    }
    public async Task<T> ExecuteNonCollectionQueryAsync(
        string sql,
        SqlParameter[] parameters,
        CancellationToken cancellationToken)
    {
        using var command = _dbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.StoredProcedure;

        if (parameters != null)
            command.Parameters.AddRange(parameters);

        await _dbContext.Database.OpenConnectionAsync(cancellationToken);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
            return PersistanceService<T, TDbContext>.MapFromDataReader<T>(reader);

        else
            return default;
    }
    public async Task<dynamic> ExecuteCommandAsync(
        string sql,
        List<SqlParameter> parameters,
        CancellationToken cancellationToken)
    {
        var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.StoredProcedure;

        foreach (var parameter in parameters)
            if (parameter.Direction != ParameterDirection.Output)
                command.Parameters.Add(parameter);

        SqlParameter? outputParam = parameters.FirstOrDefault(p => p.Direction == ParameterDirection.Output);
        if (outputParam != null && !command.Parameters.Contains(outputParam.ParameterName))
        {
            var clonedOutputParam = (SqlParameter)((ICloneable)outputParam).Clone();
            command.Parameters.Add(clonedOutputParam);
        }

        await command.ExecuteNonQueryAsync(cancellationToken);

        outputParam = command.Parameters.Cast<SqlParameter>().FirstOrDefault(p => p.Direction == ParameterDirection.Output);

        if (outputParam != null && outputParam.Value != DBNull.Value)
            return outputParam.Value;

        return default;
    }
    private static X MapFromDataReader<X>(DbDataReader reader)
    {
        var entity = Activator.CreateInstance<X>();

        for (int i = 0; i < reader.FieldCount; i++)
        {
            var columnName = reader.GetName(i);
            var property = typeof(X).GetProperty(columnName);
            if (property != null && !reader.IsDBNull(i))
            {
                var value = reader.GetValue(i);
                property.SetValue(entity, value);
            }
        }
        return entity;
    }
}