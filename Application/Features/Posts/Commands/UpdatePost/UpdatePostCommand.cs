using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostType Type { get; set; }
}
