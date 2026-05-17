using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClubMembers.Queries.GetStudentClubs;

public class GetStudentClubsHandler : IRequestHandler<GetStudentClubsQuery, List<ClubMember>>
{
    private readonly IGenericRepository<ClubMember> _repository;

    public GetStudentClubsHandler(IGenericRepository<ClubMember> repository)
    {
        _repository = repository;
    }

    public async Task<List<ClubMember>> Handle(GetStudentClubsQuery request, CancellationToken cancellationToken)
    {
        
        var memberships = await _repository.GetAllWithIncludesAsync(
            x => x.Club
        );


        return memberships.Where(x => x.StudentId == request.StudentId).ToList();
    }
}
