using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.ClubMembers.Queries.GetStudentClubs;

public class GetStudentClubsQuery : IRequest<List<ClubMember>>
{
    public int StudentId { get; set; } 
}
