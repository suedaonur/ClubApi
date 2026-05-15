using Application.Features.Students.Commands.LoginStudent;
using Application.Features.Students.Commands.RegisterStudent;
using Application.Interfaces;
using ClubUI.Application.Features.Students.Commands;
using MediatR;
using Microsoft.AspNetCore.Http; // Bunun için WebAPI referansı gerekebilir veya sadece logic kurulur
using System.Security.Claims;

namespace Application.Behaviors;

public class PermissionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork; // Veritabanı kontrolü için

    public PermissionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
     
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is LoginStudentCommand || request is RegisterStudentCommand || request is VerifyObsCommand)
        {
            return await next(); // Hiç kontrol etme, direkt geçsin
        }
        // 1. İstek bir yetki gerektiriyor mu? (IPermissionRequest kontrolü)
        if (request is IPermissionRequest permissionRequest)
        {
            var requiredPermission = permissionRequest.RequiredPermission;

            // Şimdilik JWT olmadığı için burayı bir "Simülasyon" gibi düşüneceğiz.
            // JWT yapınca buraya "Giriş yapan kullanıcının ID'sini al" kodu gelecek.

            // ÖRNEK MANTIK: 
            // Veritabanından bu öğrencinin rollerine bak, 
            // o rollerin bu 'requiredPermission' koduna sahip olup olmadığını kontrol et.

            Console.WriteLine($"🔍 Güvenlik Kontrolü: '{requiredPermission}' yetkisi aranıyor...");

            // Eğer yetki yoksa (şimdilik her şeye izin veriyoruz ama yapıyı kuruyoruz):
            // throw new UnauthorizedAccessException("Bu işlem için yetkiniz bulunmamaktadır.");
        }

        return await next();
    }
}