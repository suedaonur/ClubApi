using Application.Features.ClubMembers.Commands.JoinClub;
using Application.Features.ClubMembers.Commands.LeaveClub;
using Application.Features.ClubMembers.Commands.UpdateMemberRole;
using Application.Features.ClubMembers.Queries.GetClubMembers;
using Application.Features.ClubMembers.Queries.GetStudentClubs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubMembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClubMembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("join")]
    public async Task<IActionResult> JoinClub(JoinClubCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("{clubId}")]
    public async Task<IActionResult> GetMembers(int clubId)
    {
        var result = await _mediator.Send(new GetClubMembersQuery { ClubId = clubId });
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> LeaveClub(int id)
    {
        var result = await _mediator.Send(new LeaveClubCommand { Id = id });
        return Ok(result);
    }

    [HttpPut("update-role")]
    public async Task<IActionResult> UpdateRole(UpdateMemberRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentClubs(int studentId)
    {
        var result = await _mediator.Send(new GetStudentClubsQuery { StudentId = studentId });
        return Ok(result);
    }
}
