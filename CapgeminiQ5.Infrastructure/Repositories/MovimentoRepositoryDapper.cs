using CapgeminiQ5.Application.Contracts;
using CapgeminiQ5.Application.DTOs;
using CapgeminiQ5.Infrastructure.Database;
using Dapper;

namespace CapgeminiQ5.Infrastructure.Repositories;

public class MovimentoRepositoryDapper : IMovimentoRepository
{
    public bool IdempotenciaExiste(string chave)
    {
        using var connection = DbConnectionFactory.CreateConnection();

        return connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM idempotencia WHERE chave_idempotencia = @Chave",
            new { Chave = chave }) > 0;
    }

    public string ObterIdPorChave(string chave)
    {
        using var connection = DbConnectionFactory.CreateConnection();

        return connection.ExecuteScalar<string>("SELECT idmovimento FROM movimento WHERE chaveIdempotencia = @Chave",
            new { Chave = chave });
    }

    public Guid RegistrarMovimento(MovimentacaoRequestDTO dto)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        var id = Guid.NewGuid();

        connection.Execute(@"
    INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
    VALUES (@Id, @IdContaCorrente, @DataMovimento, @Tipo, @Valor)",
    new
    {
        Id = id.ToString(),
        IdContaCorrente = dto.IdContaCorrente.ToString().ToUpper(),
        Tipo = dto.TipoMovimento,
        Valor = dto.Valor,
        DataMovimento = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
    }); 

        return id;
    }

    public decimal CalcularSaldo(Guid idConta)
    {
        using var connection = DbConnectionFactory.CreateConnection();

        var creditos = connection.ExecuteScalar<decimal>(
            "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'C'",
            new { Id = idConta.ToString() });

        var debitos = connection.ExecuteScalar<decimal>(
            "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'D'",
            new { Id = idConta.ToString() });

        return creditos - debitos;
    }
}
