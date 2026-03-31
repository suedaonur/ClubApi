using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<bool>
{
    public int Id { get; set; }
}
