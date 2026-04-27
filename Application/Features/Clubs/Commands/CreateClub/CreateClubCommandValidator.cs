using Application.Features.Clubs.Commands.CreateClub;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kulüp adı boş olamaz.")
            .MinimumLength(3).WithMessage("Kulüp adı en az 3 karakter olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıklama 500 karakteri geçemez.");
    }
}
