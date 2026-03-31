using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.ClubMembers.Queries.GetClubMembers;

public class GetClubMembersQuery : IRequest<List<ClubMember>>
{
    public int ClubId { get; set; } 
}
