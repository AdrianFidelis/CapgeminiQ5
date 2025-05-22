using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using CapgeminiQ5.Application.Contracts;
using CapgeminiQ5.Domain.Entities;

namespace CapgeminiQ5.Infrastructure.Repositories;

public class ContaRepositoryDapper : IContaRepository
{
    private IDbConnection GetConnection()
        => new SqliteConnection("Data Source=C:\\CapgeminiQ5\\app.db");

    public ContaCorrente? ObterConta(Guid id)
    {
        using var connection = GetConnection();
        return connection.QueryFirstOrDefault<ContaCorrente>(
            "SELECT * FROM contacorrente WHERE idcontacorrente = @Id",
            new { Id = id.ToString() });
    }

    public bool ContaAtiva(Guid id)
    {
        using var connection = GetConnection();
        return connection.ExecuteScalar<int>(
       "SELECT COUNT(1) FROM contacorrente WHERE LOWER(idcontacorrente) = LOWER(@Id) AND ativo = 1",
       new { Id = id.ToString() }) > 0;

        
    }
}
