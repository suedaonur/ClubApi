using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper; 
using Application.DTOs; 

namespace Application.Features.Students.Queries.GetByIdStudent;

public class GetByIdStudentHandler : IRequestHandler<GetByIdStudentQuery, StudentDto>
{
    private readonly IGenericRepository<Student> _repository;
    private readonly IMapper _mapper;

    public GetByIdStudentHandler(IGenericRepository<Student> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id);

        if (student == null) return null;

        
        return _mapper.Map<StudentDto>(student);
    }
}
