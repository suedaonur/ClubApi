using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.RegisterStudent;

public class RegisterStudentCommand : IRequest<bool>
{
    public string StudentNumber { get; set; }
    public string Password { get; set; }
}

public class RegisterStudentHandler : IRequestHandler<RegisterStudentCommand, bool>
{
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterStudentHandler(IGenericRepository<Student> studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        
        var student = await _studentRepository.GetAsync(x => x.StudentNumber == request.StudentNumber);

        if (student == null)
        {           
            return false;
        }       
        if (student.PasswordHash != null)
        {
            return false;
        }

        HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        student.PasswordHash = passwordHash;
        student.PasswordSalt = passwordSalt;
        student.IsObsVerified = true; 

        _studentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}