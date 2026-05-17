using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.ClubMembers.Commands.JoinClub;

public class JoinClubCommand : IRequest<int>
{
    public int StudentId { get; set; }
    public int ClubId { get; set; }
}
