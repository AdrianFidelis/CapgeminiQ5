using CapgeminiQ5.Application.Contracts;
using CapgeminiQ5.Application.DTOs;
using MediatR;

namespace CapgeminiQ5.Application.Features.Saldo.Queries;

public class ConsultarSaldoQueryHandler : IRequestHandler<ConsultarSaldoQuery, SaldoResponseDTO>
{
    private readonly IContaRepository _contaRepo;
    private readonly IMovimentoRepository _movRepo;

    public ConsultarSaldoQueryHandler(IContaRepository contaRepo, IMovimentoRepository movRepo)
    {
        _contaRepo = contaRepo;
        _movRepo = movRepo;
    }

    public async Task<SaldoResponseDTO> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
    {
        var conta = _contaRepo.ObterConta(request.IdContaCorrente);
        if (conta is null || !conta.Ativa)
            throw new ArgumentException("Conta inexistente ou inativa.");

        var saldo = _movRepo.CalcularSaldo(request.IdContaCorrente);

        return new SaldoResponseDTO
        {
            Numero = conta.Numero,
            Nome = conta.Nome,
            DataConsulta = DateTime.Now,
            Saldo = saldo
        };
    }
}
