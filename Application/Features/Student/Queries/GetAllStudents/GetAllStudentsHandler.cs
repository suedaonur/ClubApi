using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetAllStudents;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, List<Student>>
{
    private readonly IGenericRepository<Student> _repository;

    public GetAllStudentsHandler(IGenericRepository<Student> repository)
    {
        _repository = repository;
    }

    public async Task<List<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
