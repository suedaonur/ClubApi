using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClubMembers.Commands.LeaveClub;

public class LeaveClubHandler : IRequestHandler<LeaveClubCommand, bool>
{
    private readonly IGenericRepository<ClubMember> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveClubHandler(IGenericRepository<ClubMember> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(LeaveClubCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        if (member == null) return false;

        _repository.Delete(member);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
