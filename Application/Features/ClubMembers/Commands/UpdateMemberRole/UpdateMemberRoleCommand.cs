using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.ClubMembers.Commands.UpdateMemberRole;

public class UpdateMemberRoleCommand : IRequest<bool>
{
    public int Id { get; set; } // Güncellenecek üyeliğin ID'si
    public int NewRoleId { get; set; } // Atanacak yeni rolün ID'si
}
