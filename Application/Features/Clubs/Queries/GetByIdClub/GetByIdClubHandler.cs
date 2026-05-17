using Application.Features.Clubs.Queries.GetByIdClub;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetByIdClubHandler : IRequestHandler<GetByIdClubQuery, Club>
{
    private readonly IGenericRepository<Club> _repository;
    public GetByIdClubHandler(IGenericRepository<Club> repository) => _repository = repository;

    public async Task<Club> Handle(GetByIdClubQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(
            c => c.Id == request.Id,
            e => e.Members,
            e => e.Posts,
            e => e.President
        );
    }
}
