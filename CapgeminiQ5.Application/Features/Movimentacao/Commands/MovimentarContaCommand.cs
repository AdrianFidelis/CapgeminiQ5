using MediatR;
using System;

namespace CapgeminiQ5.Application.Features.Movimentacao.Commands;

public class MovimentarContaCommand : IRequest<Guid>
{
    public string ChaveIdempotencia { get; set; } = string.Empty;
    public Guid IdContaCorrente { get; set; }
    public string TipoMovimento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}


