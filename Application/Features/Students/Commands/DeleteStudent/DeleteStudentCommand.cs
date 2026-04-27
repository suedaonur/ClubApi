using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}
