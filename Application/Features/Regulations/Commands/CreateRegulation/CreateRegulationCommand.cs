using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Regulations.Commands.CreateRegulation;

public class CreateRegulationCommand : IRequest<int>
{
    public string Title { get; set; }
    public string TextContent { get; set; }
    public string Category { get; set; } 
}
