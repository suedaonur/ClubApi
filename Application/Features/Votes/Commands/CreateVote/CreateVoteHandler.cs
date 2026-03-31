using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Votes.Commands.CreateVote;

public class CreateVoteHandler : IRequestHandler<CreateVoteCommand, bool>
{
    private readonly IGenericRepository<Vote> _voteRepository;
    private readonly IGenericRepository<Post> _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    // İki farklı tabloyla (Vote ve Post) işlemi 
    public CreateVoteHandler(
        IGenericRepository<Vote> voteRepository,
        IGenericRepository<Post> postRepository,
        IUnitOfWork unitOfWork)
    {
        _voteRepository = voteRepository;
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        // 1. Yeni oyu veritabanına ekle
        var vote = new Vote
        {
            StudentId = request.StudentId,
            PostId = request.PostId,
            IsUpvote = request.IsUpvote
        };
        await _voteRepository.AddAsync(vote);

        // 2. Post'un toplam skorunu güncelle
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post != null)
        {
            if (request.IsUpvote)
                post.VoteScore += 1; // Beğenildiyse skoru artır
            else
                post.VoteScore -= 1; // Beğenilmediyse skoru düşür

            _postRepository.Update(post);
        }

        // 3. Değişiklikleri kaydet
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
