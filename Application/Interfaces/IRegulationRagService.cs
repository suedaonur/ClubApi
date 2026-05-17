using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IRegulationRagService
{
    Task InitializeAsync();
    Task<string> AskQuestionAsync(string question, CancellationToken cancellationToken = default);
}
