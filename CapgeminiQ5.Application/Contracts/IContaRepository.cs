namespace CapgeminiQ5.Application.Contracts;

using CapgeminiQ5.Domain.Entities;

public interface IContaRepository
{
    ContaCorrente? ObterConta(Guid id);
    bool ContaAtiva(Guid id);
}