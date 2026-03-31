using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IGenericRepository<Student> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentHandler(IGenericRepository<Student> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id);
        if (student == null) return false;

        student.StudentNumber = request.StudentNumber;
        student.FullName = request.FullName;
        student.Email = request.Email;

        _repository.Update(student);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
