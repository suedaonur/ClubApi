using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClubMembers.Queries.GetClubMembers;

public class GetClubMembersHandler : IRequestHandler<GetClubMembersQuery, List<ClubMember>>
{
    private readonly IGenericRepository<ClubMember> _repository;

    public GetClubMembersHandler(IGenericRepository<ClubMember> repository)
    {
        _repository = repository;
    }

    public async Task<List<ClubMember>> Handle(GetClubMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _repository.GetAllWithIncludesAsync(
        x => x.Student,
        x => x.Role
    );

        return members.Where(x => x.ClubId == request.ClubId).ToList();
    }
}
