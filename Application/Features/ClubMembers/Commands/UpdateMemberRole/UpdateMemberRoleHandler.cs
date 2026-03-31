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
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberRoleHandler(IGenericRepository<ClubMember> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        if (member == null) return false;

        member.RoleId = request.NewRoleId;

        _repository.Update(member);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}