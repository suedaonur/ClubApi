using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IGenericRepository<Post> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostHandler(IGenericRepository<Post> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            ClubId = request.ClubId,
            Title = request.Title,
            Content = request.Content,
            Type = request.Type,
            VoteScore = 0    //  oylama sıfırdan başlıcak

        };

        await _repository.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return post.Id;
    }
}
