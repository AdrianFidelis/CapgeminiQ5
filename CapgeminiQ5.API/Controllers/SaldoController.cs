using CapgeminiQ5.Application.DTOs;
using CapgeminiQ5.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CapgeminiQ5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaldoController : ControllerBase
{
    [HttpGet("{idconta}")]
    public IActionResult Get(Guid idconta)
    {
        var contaRepo = new ContaRepositoryDapper();
        var conta = contaRepo.ObterConta(idconta);

        if (conta is null || !conta.Ativa)
            return BadRequest(new { error = "INVALID_ACCOUNT", message = "Conta não encontrada ou inativa." });

        var movRepo = new MovimentoRepositoryDapper();
        var saldo = movRepo.CalcularSaldo(idconta);

        var response = new SaldoResponseDTO
        {
            Numero = conta.Numero,
            Nome = conta.Nome,
            DataConsulta = DateTime.Now,
            Saldo = saldo
        };

        return Ok(response);
    }
}
