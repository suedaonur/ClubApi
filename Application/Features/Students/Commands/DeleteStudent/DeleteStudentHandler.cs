using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IGenericRepository<Domain.Entities.Student> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudentHandler(IGenericRepository<Domain.Entities.Student> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id);
        if (student == null) return false;

        _repository.Delete(student);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
