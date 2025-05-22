using CapgeminiQ5.Application.Contracts;
using CapgeminiQ5.Application.DTOs;
using MediatR;

namespace CapgeminiQ5.Application.Features.Movimentacao.Commands;

public class MovimentarContaCommandHandler : IRequestHandler<MovimentarContaCommand, Guid>
{
    private readonly IContaRepository _contaRepo;
    private readonly IMovimentoRepository _movRepo;

    public MovimentarContaCommandHandler(IContaRepository contaRepo, IMovimentoRepository movRepo)
    {
        _contaRepo = contaRepo;
        _movRepo = movRepo;
    }

    public async Task<Guid> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
    {
        if (request.Valor <= 0)
            throw new ArgumentException("Valor deve ser positivo.");

        if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            throw new ArgumentException("Tipo deve ser 'C' ou 'D'.");

        if (!_contaRepo.ContaAtiva(request.IdContaCorrente))
            throw new ArgumentException("Conta inativa ou inexistente.");

        if (_movRepo.IdempotenciaExiste(request.ChaveIdempotencia))
        {
            var idExistente = _movRepo.ObterIdPorChave(request.ChaveIdempotencia);
            return Guid.Parse(idExistente);
        }

        var id = _movRepo.RegistrarMovimento(new MovimentacaoRequestDTO
        {
            ChaveIdempotencia = request.ChaveIdempotencia,
            IdContaCorrente = request.IdContaCorrente,
            TipoMovimento = request.TipoMovimento,
            Valor = request.Valor
        });

        return id;
    }
}
