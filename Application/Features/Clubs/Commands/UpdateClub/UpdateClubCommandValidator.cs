using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Application.Features.Clubs.Commands.UpdateClub
{
    public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
    {
        public UpdateClubCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kulüp ID'si girilmelidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kulüp adı boş olamaz.")
                .MinimumLength(3).WithMessage("Kulüp adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama 500 karakteri geçemez.");
        }
    }
}
