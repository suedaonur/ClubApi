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
        var clubMember = new ClubMember
        {
            StudentId = request.StudentId,
            ClubId = request.ClubId,
            RoleId = request.RoleId
        };

        await _repository.AddAsync(clubMember);
        await _unitOfWork.SaveChangesAsync();

        return clubMember.Id; // Oluşan üyeliğin ID'sini dönecek.
    }
}