using CapgeminiQ5.Application.Features.Movimentacao.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapgeminiQ5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovimentacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]    
    public async Task<IActionResult> Post([FromBody] MovimentarContaCommand command)

    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = "VALIDATION_ERROR", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }
}
