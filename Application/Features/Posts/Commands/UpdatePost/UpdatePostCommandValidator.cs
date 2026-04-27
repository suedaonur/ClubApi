using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir gönderi ID'si girilmelidir.");
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
