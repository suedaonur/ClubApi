using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Queries.GetClubPosts;

public class GetClubPostsHandler : IRequestHandler<GetClubPostsQuery, List<Post>>
{
    private readonly IGenericRepository<Post> _repository;

    public GetClubPostsHandler(IGenericRepository<Post> repository)
    {
        _repository = repository;
    }

    public async Task<List<Post>> Handle(GetClubPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _repository.GetAllAsync();
       
        return posts.Where(x => x.ClubId == request.ClubId).ToList();  
    }
}
