using Application.Interfaces; 
using Domain.Entities;      
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ClubUI.Application.Features.Students.Commands;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Features.Students.Handlers;

public class VerifyObsCommandHandler : IRequestHandler<VerifyObsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Student> _studentRepository;

    public VerifyObsCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository)
    {
        _unitOfWork = unitOfWork;
        _studentRepository = studentRepository;
    }

    public async Task<bool> Handle(VerifyObsCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.StudentNumber))
        {
            Console.WriteLine("DİKKAT: Numaraya ulaşılamadı, veri null geldi!");
            return false;
        }

        Console.WriteLine($"Gelen Numarayı Yakaladık: {request.StudentNumber}");
        var student = await _studentRepository.GetAsync(x => x.StudentNumber.Trim() == request.StudentNumber.Trim());

        if (student == null) return false;

        student.IsObsVerified = true;
        _studentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}