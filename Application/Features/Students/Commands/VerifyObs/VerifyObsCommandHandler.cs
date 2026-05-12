using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ClubUI.Application.Features.Students.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Handlers;

public class VerifyObsCommandHandler : IRequestHandler<VerifyObsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly ILogger<VerifyObsCommandHandler> _logger;

    
    public VerifyObsCommandHandler(
        IUnitOfWork unitOfWork,
        IGenericRepository<Student> studentRepository,
        ILogger<VerifyObsCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _studentRepository = studentRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(VerifyObsCommand request, CancellationToken cancellationToken)
    {
        
        _logger.LogInformation("{StudentNumber} numaralı öğrenci için OBS doğrulaması başlatıldı.", request.StudentNumber);

       
        if (string.IsNullOrWhiteSpace(request.StudentNumber))
        {
            _logger.LogWarning("OBS doğrulaması başarısız: Öğrenci numarası boş veya hatalı geldi.");
            return false;
        }

        
        var student = await _studentRepository.GetAsync(x => x.StudentNumber.Trim() == request.StudentNumber.Trim());

        
        if (student == null)
        {
            _logger.LogWarning("{StudentNumber} numaralı öğrenci sistemde kayıtlı değil!", request.StudentNumber);
            return false;
        }

        
        student.IsObsVerified = true;
        _studentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync();

        
        _logger.LogInformation("{StudentNumber} numaralı öğrenci başarıyla doğrulandı.", request.StudentNumber);

        return true;
    }
}