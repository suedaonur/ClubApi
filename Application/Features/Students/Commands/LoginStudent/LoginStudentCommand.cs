using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Security;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Students.Commands.LoginStudent;

public class LoginStudentCommand : IRequest<LoginResponseDto>
{
    public string StudentNumber { get; set; }
    public string Password { get; set; }
}

public class LoginStudentHandler : IRequestHandler<LoginStudentCommand, LoginResponseDto>
{
    private readonly IGenericRepository<Domain.Entities.Student> _studentRepository;
    private readonly JwtProvider _jwtProvider;

    public LoginStudentHandler(IGenericRepository<Domain.Entities.Student> studentRepository, JwtProvider jwtProvider)
    {
        _studentRepository = studentRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginResponseDto> Handle(LoginStudentCommand request, CancellationToken cancellationToken)
    {
        
        var student = await _studentRepository.GetAsync(x => x.StudentNumber == request.StudentNumber);

        if (student == null || student.PasswordHash == null) return null;

       
        var isPasswordMatch = HashingHelper.VerifyPasswordHash(request.Password, student.PasswordHash, student.PasswordSalt);

        if (!isPasswordMatch) return null;

        
        var token = _jwtProvider.CreateToken(student);

        return new LoginResponseDto
        {
            Token = token,
            FullName = student.FullName,
            Email = student.Email
        };
    }
}
