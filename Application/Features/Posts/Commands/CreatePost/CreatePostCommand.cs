using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities; 

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<int>
{
    public int ClubId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostType Type { get; set; } // Articl e Event  Announcement Form 
}
