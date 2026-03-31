using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Regulations.Commands.UploadWord;

public class UploadRegulationWordCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Category { get; set; }
    public IFormFile File { get; set; } 
}
