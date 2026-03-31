using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<int>
{
    public string StudentNumber { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}