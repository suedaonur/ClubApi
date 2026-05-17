using MediatR;
using Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Regulations.Queries.AskRegulation;

public class AskRegulationHandler : IRequestHandler<AskRegulationQuery, string>
{
    private readonly IRegulationRagService _ragService;

    public AskRegulationHandler(IRegulationRagService ragService)
    {
        _ragService = ragService;
    }

    public async Task<string> Handle(AskRegulationQuery request, CancellationToken cancellationToken)
    {
        return await _ragService.AskQuestionAsync(request.Question, cancellationToken);
    }
}
