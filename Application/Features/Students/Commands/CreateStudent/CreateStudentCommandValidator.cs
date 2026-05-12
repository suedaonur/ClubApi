using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.Features.Students.Commands.CreateStudent;

namespace Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.StudentNumber)
                .NotEmpty().WithMessage("Öğrenci numarası boş olamaz.")
                .Length(9, 15).WithMessage("Öğrenci numarası 9 ile 15 karakter arasında olmalıdır.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad alanı zorunludur.")
                .MinimumLength(3).WithMessage("Ad soyad en az 3 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
