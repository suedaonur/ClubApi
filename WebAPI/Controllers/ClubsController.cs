using Application.Features.Clubs.Commands.CreateClub;
using Application.Features.Clubs.Commands.DeleteClub;
using Application.Features.Clubs.Commands.UpdateClub;
using Application.Features.Clubs.Queries.GetAllClubs;
using Application.Features.Clubs.Queries.GetByIdClub;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClubsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllClubsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _mediator.Send(new GetByIdClubQuery { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

       

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClubCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteClubCommand { Id = id }));
    }
}
