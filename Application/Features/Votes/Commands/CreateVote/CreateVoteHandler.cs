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
        var existingVotes = await _voteRepository.GetAllAsync();
        var existingVote = existingVotes.FirstOrDefault(x => x.StudentId == request.StudentId && x.PostId == request.PostId && !x.IsDeleted);

        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null) return false;

        if (existingVote != null)
        {
            // Eğer aynı oylama tekrar yapıldıysa, oyu geri al (sil)
            if (existingVote.IsUpvote == request.IsUpvote)
            {
                _voteRepository.Delete(existingVote);

                if (existingVote.IsUpvote)
                    post.VoteScore = Math.Max(0, post.VoteScore - 1); // Beğeni geri alındı, skoru düşür
                else
                    post.VoteScore += 1; // Beğenmeme geri alındı, skoru artır
            }
            else
            {
                // Oylama yönünü değiştir
                existingVote.IsUpvote = request.IsUpvote;
                _voteRepository.Update(existingVote);

                if (request.IsUpvote)
                    post.VoteScore += 2; // Beğenmeme -> Beğenme (+2 fark)
                else
                    post.VoteScore = Math.Max(0, post.VoteScore - 2); // Beğenme -> Beğenmeme (-2 fark)
            }
        }
        else
        {
            // Yeni oy kaydı ekle
            var vote = new Vote
            {
                StudentId = request.StudentId,
                PostId = request.PostId,
                IsUpvote = request.IsUpvote
            };
            await _voteRepository.AddAsync(vote);

            if (request.IsUpvote)
                post.VoteScore += 1;
            else
                post.VoteScore = Math.Max(0, post.VoteScore - 1);
        }

        _postRepository.Update(post);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
