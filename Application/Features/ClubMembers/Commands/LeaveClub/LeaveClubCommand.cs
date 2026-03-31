using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.ClubMembers.Commands.LeaveClub;

public class LeaveClubCommand : IRequest<bool>
{
    public int Id { get; set; } 
}
