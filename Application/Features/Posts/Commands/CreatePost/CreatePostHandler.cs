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
    private readonly IGenericRepository<ClubMember> _clubMemberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostHandler(
        IGenericRepository<Post> repository, 
        IGenericRepository<ClubMember> clubMemberRepository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _clubMemberRepository = clubMemberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        // Yetki kontrolü: Öğrenci bu kulübün üyesi mi ve paylaşım yetkisi (IsWriteable) var mı?
        var allClubMembers = await _clubMemberRepository.GetAllAsync();
        var membership = allClubMembers.FirstOrDefault(x => x.StudentId == request.StudentId && x.ClubId == request.ClubId && !x.IsDeleted);

        if (membership == null || !membership.IsWriteable)
        {
            throw new UnauthorizedAccessException("Bu kulüpte paylaşım (post) yapma yetkiniz bulunmamaktadır.");
        }

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
