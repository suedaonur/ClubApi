using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetByIdStudent;

public class GetByIdStudentHandler : IRequestHandler<GetByIdStudentQuery, Domain.Entities.Student>
{
    private readonly IGenericRepository<Domain.Entities.Student> _repository;

    public GetByIdStudentHandler(IGenericRepository<Domain.Entities.Student> repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Student> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
