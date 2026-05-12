using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Students.Queries.GetAllStudents;
public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IGenericRepository<Student> _repository;
    private readonly IMapper _mapper;

    public GetAllStudentsHandler(IGenericRepository<Student> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetAllAsync();

        
        return _mapper.Map<List<StudentDto>>(students);
    }
}