using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Commands.DeletePost;

public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IGenericRepository<Post> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostHandler(IGenericRepository<Post> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.Id);
        if (post == null) return false;

        _repository.Delete(post);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}