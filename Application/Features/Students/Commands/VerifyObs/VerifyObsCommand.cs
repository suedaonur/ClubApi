using MediatR;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClubUI.Application.Features.Students.Commands;

public class VerifyObsCommand : IRequest<bool>
{
    [JsonPropertyName("studentNumber")]
    public string StudentNumber { get; set; }
}