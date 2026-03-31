using Application.Features.Posts.Commands.CreatePost;
using Application.Features.Posts.Commands.DeletePost;
using Application.Features.Posts.Commands.UpdatePost;
using Application.Features.Posts.Queries.GetAllPosts;
using Application.Features.Posts.Queries.GetClubPosts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("club/{clubId}")]
    public async Task<IActionResult> GetClubPosts(int clubId)
    {
        var result = await _mediator.Send(new GetClubPostsQuery { ClubId = clubId });
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatePostCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletePostCommand { Id = id });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPostsQuery());
        return Ok(result);
    }
}
