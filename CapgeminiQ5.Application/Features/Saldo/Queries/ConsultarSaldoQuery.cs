using CapgeminiQ5.Application.DTOs;
using MediatR;
using System;

namespace CapgeminiQ5.Application.Features.Saldo.Queries;

public class ConsultarSaldoQuery : IRequest<SaldoResponseDTO>
{
    public Guid IdContaCorrente { get; set; }

    public ConsultarSaldoQuery(Guid idContaCorrente)
    {
        IdContaCorrente = idContaCorrente;
    }
}
