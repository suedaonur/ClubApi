using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clubs.Commands.CreateClub;

public class CreateClubHandler : IRequestHandler<CreateClubCommand, int>
{
    private readonly IGenericRepository<Club> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClubHandler(IGenericRepository<Club> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateClubCommand request, CancellationToken cancellationToken)
    {
        var club = new Club
        {
            Name = request.Name,
            Description = request.Description
        };

        await _repository.AddAsync(club);
        await _unitOfWork.SaveChangesAsync(); 

        return club.Id;
    }
}