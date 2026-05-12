using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using MediatR;
using Application.DTOs; 

namespace Application.Features.Students.Queries.GetAllStudents;


public class GetAllStudentsQuery : IRequest<List<StudentDto>>
{
}
