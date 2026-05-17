using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ClubMembers.Commands.UpdateMemberRole;

public class UpdateMemberRoleHandler : IRequestHandler<UpdateMemberRoleCommand, bool>
{
    private readonly IGenericRepository<ClubMember> _repository;
    private readonly IGenericRepository<Club> _clubRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberRoleHandler(
        IGenericRepository<ClubMember> repository, 
        IGenericRepository<Club> clubRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        if (member == null) return false;

        // 1. Güncelleyen kişinin yetkisi var mı kontrol et (IsAdmin == true veya Kulüp Başkanı ise)
        var allClubMembers = await _repository.GetAllAsync();
        var updaterMembership = allClubMembers.FirstOrDefault(x => x.StudentId == request.UpdaterStudentId && x.ClubId == member.ClubId && !x.IsDeleted);

        var isPresident = false;
        var club = await _clubRepository.GetByIdAsync(member.ClubId);
        if (club != null)
        {
            isPresident = club.PresidentId == request.UpdaterStudentId;
        }

        if ((updaterMembership == null || !updaterMembership.IsAdmin) && !isPresident)
        {
            throw new UnauthorizedAccessException("Bu kulüpte üyelerin yetkilerini sadece kulüp yöneticisi/sahibi güncelleyebilir.");
        }

        // 2. Yetkiler geçerliyse güncelle
        member.IsAdmin = request.IsAdmin;
        member.IsWriteable = request.IsWriteable;
        member.IsRemoveableMember = request.IsRemoveableMember;

        _repository.Update(member);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}