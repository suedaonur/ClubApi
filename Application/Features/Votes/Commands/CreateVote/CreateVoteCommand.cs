using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Votes.Commands.CreateVote;

public class CreateVoteCommand : IRequest<bool>
{
    public int StudentId { get; set; }
    public int PostId { get; set; }
    public bool IsUpvote { get; set; } // true = Beğendi, false = Beğenmedi
}
