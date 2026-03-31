using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Commands.UpdatePost;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IGenericRepository<Post> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostHandler(IGenericRepository<Post> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.Id);
        if (post == null) return false;

        post.Title = request.Title;
        post.Content = request.Content;
        post.Type = request.Type;

        _repository.Update(post);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
