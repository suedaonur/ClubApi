using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.CreateStudent;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IGenericRepository<Domain.Entities.Student> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentHandler(IGenericRepository<Domain.Entities.Student> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Domain.Entities.Student
        {
            StudentNumber = request.StudentNumber,
            FullName = request.FullName,
            Email = request.Email,
            IsObsVerified = false // Kayıt anında henüz doğrulanmamış kabul ediyorum devamı getirilecek.
        };

        await _repository.AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return student.Id;
    }
}