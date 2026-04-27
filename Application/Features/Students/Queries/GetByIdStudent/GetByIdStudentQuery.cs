using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.Students.Queries.GetByIdStudent;
public class GetByIdStudentQuery : IRequest<Domain.Entities.Student>
{
    public int Id { get; set; }
}
