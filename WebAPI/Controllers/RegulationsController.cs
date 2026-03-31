using Application.Features.Regulations.Commands.CreateRegulation;
using Application.Features.Regulations.Commands.UploadWord;
using Application.Features.Regulations.Queries.GetAllRegulations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegulationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegulationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRegulationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllRegulationsQuery());
        return Ok(result);
    }

    [HttpPost("upload-word")]
    public async Task<IActionResult> UploadWord([FromForm] UploadRegulationWordCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
