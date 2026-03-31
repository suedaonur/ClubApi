using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string StudentNumber { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
