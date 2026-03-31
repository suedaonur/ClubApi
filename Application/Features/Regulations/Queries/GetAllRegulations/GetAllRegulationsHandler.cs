using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Regulations.Queries.GetAllRegulations;

public class GetAllRegulationsHandler : IRequestHandler<GetAllRegulationsQuery, List<Regulation>>
{
    private readonly IGenericRepository<Regulation> _repository;

    public GetAllRegulationsHandler(IGenericRepository<Regulation> repository)
    {
        _repository = repository;
    }

    public async Task<List<Regulation>> Handle(GetAllRegulationsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
