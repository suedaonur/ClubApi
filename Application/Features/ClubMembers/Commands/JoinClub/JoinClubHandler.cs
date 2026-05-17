using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClubMembers.Commands.JoinClub;

public class JoinClubHandler : IRequestHandler<JoinClubCommand, int>
{
    private readonly IGenericRepository<ClubMember> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinClubHandler(IGenericRepository<ClubMember> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(JoinClubCommand request, CancellationToken cancellationToken)
    {
        var existingMembers = await _repository.GetAllAsync();
        var isAlreadyMember = existingMembers.Any(x => x.StudentId == request.StudentId && x.ClubId == request.ClubId && !x.IsDeleted);

        if (isAlreadyMember)
        {
            throw new InvalidOperationException("Bu kulübe zaten üyesiniz.");
        }

        var clubMember = new ClubMember
        {
            StudentId = request.StudentId,
            ClubId = request.ClubId,
            IsAdmin = false,
            IsWriteable = false,
            IsRemoveableMember = false
        };

        await _repository.AddAsync(clubMember);
        await _unitOfWork.SaveChangesAsync();

        return clubMember.Id; // Oluşan üyeliğin ID'sini dönecek.
    }
}