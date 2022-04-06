using System.Data;
using Dapper;
using EasyTestAPI.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EasyTestAPI.Infrastructure.Data;
public class ApplicationReadDbConnection : IApplicationReadDbConnection, IDisposable
{
  private readonly IDbConnection connection;
  public ApplicationReadDbConnection(IConfiguration configuration)
  {
    connection = new NpgsqlConnection(configuration.GetConnectionString("EasyTestDatabase"));
  }
  public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
  {
    return (await connection.QueryAsync<T>(sql, param, transaction)).AsList();
  }
  public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
  {
    return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
  }
  public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
  {
    return await connection.QuerySingleAsync<T>(sql, param, transaction);
  }
  public void Dispose()
  {
    connection.Dispose();
  }
}
