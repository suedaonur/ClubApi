using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.Features.Regulations.Commands.CreateRegulation;

namespace Application.Features.Regulations.Commands.CreateRegulation
{
    public class CreateRegulationCommandValidator : AbstractValidator<CreateRegulationCommand>
    {
        public CreateRegulationCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Yönetmelik başlığı boş olamaz.")
                .MinimumLength(10).WithMessage("Başlık en az 10 karakter olmalıdır.");

            RuleFor(x => x.TextContent)
                .NotEmpty().WithMessage("Yönetmelik içeriği boş olamaz.")
                .MinimumLength(50).WithMessage("Yönetmelik metni çok kısa, lütfen detaylandırın.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategori seçimi zorunludur.");
        }
    }
}
