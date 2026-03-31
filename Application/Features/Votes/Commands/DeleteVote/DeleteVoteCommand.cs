using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Votes.Commands.DeleteVote;

public class DeleteVoteCommand : IRequest<bool>
{
    public int Id { get; set; }
}
