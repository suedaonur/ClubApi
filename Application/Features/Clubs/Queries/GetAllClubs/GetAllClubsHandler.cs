using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Clubs.Queries.GetAllClubs;

public class GetAllClubsHandler : IRequestHandler<GetAllClubsQuery, List<Club>>
{
    private readonly IGenericRepository<Club> _repository;

    public GetAllClubsHandler(IGenericRepository<Club> repository)
    {
        _repository = repository;
    }

    public async Task<List<Club>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
    {
      
        return await _repository.GetAllAsync();
    }
}
