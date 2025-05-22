namespace CapgeminiQ5.Application.Contracts;

using CapgeminiQ5.Application.DTOs;

public interface IMovimentoRepository
{
    bool IdempotenciaExiste(string chave);
    string ObterIdPorChave(string chave);
    Guid RegistrarMovimento(MovimentacaoRequestDTO dto);
    decimal CalcularSaldo(Guid idConta);
}