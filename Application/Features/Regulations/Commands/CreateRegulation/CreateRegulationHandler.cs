using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Regulations.Commands.CreateRegulation;

public class CreateRegulationHandler : IRequestHandler<CreateRegulationCommand, int>
{
    private readonly IGenericRepository<Regulation> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRegulationHandler(IGenericRepository<Regulation> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateRegulationCommand request, CancellationToken cancellationToken)
    {
        var regulation = new Regulation
        {
            Title = request.Title,
            TextContent = request.TextContent,
            Category = request.Category
        };

        await _repository.AddAsync(regulation);
        await _unitOfWork.SaveChangesAsync();

        return regulation.Id;
    }
}
