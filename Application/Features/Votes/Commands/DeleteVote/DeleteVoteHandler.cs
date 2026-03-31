using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Votes.Commands.DeleteVote;

public class DeleteVoteHandler : IRequestHandler<DeleteVoteCommand, bool>
{
    private readonly IGenericRepository<Vote> _voteRepository;
    private readonly IGenericRepository<Post> _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVoteHandler(
        IGenericRepository<Vote> voteRepository,
        IGenericRepository<Post> postRepository,
        IUnitOfWork unitOfWork)
    {
        _voteRepository = voteRepository;
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
    {
        // Silinecek oyu buldum
        var vote = await _voteRepository.GetByIdAsync(request.Id);
        if (vote == null) return false;

        //  skoru tersine çevirdim
        var post = await _postRepository.GetByIdAsync(vote.PostId);
        if (post != null)
        {
            if (vote.IsUpvote)
                post.VoteScore -= 1; 
            else
                post.VoteScore += 1; 

            _postRepository.Update(post);
        }

        //veritabanından silme
        _voteRepository.Delete(vote);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
