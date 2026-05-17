using MediatR;

namespace Application.Features.Regulations.Queries.AskRegulation;

public class AskRegulationQuery : IRequest<string>
{
    public string Question { get; set; } = string.Empty;
}
