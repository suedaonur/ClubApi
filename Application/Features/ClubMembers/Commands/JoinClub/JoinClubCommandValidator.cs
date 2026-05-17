using Application.Features.ClubMembers.Commands.JoinClub;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JoinClubCommandValidator : AbstractValidator<JoinClubCommand>
{
    public JoinClubCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Geçerli bir öğrenci seçilmelidir.");
        RuleFor(x => x.ClubId).GreaterThan(0).WithMessage("Geçerli bir kulüp seçilmelidir.");
    }
}
