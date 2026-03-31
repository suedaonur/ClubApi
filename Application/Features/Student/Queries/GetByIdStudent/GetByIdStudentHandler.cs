using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetByIdStudent;

public class GetByIdStudentHandler : IRequestHandler<GetByIdStudentQuery, Student>
{
    private readonly IGenericRepository<Student> _repository;

    public GetByIdStudentHandler(IGenericRepository<Student> repository)
    {
        _repository = repository;
    }

    public async Task<Student> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
