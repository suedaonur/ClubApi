using Application.Features.Regulations.Queries.AskRegulation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskQuestion([FromBody] AskRegulationQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.Question))
        {
            return BadRequest("Soru boş olamaz.");
        }

        var result = await _mediator.Send(query);
        return Ok(new { Answer = result });
    }
}
