using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Queries.GetAllPosts;

public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, List<Post>>
{
    private readonly IGenericRepository<Post> _repository;

    public GetAllPostsHandler(IGenericRepository<Post> repository)
    {
        _repository = repository;
    }

    public async Task<List<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();  // Repository'deki hazır metodu kullanıyorum. 
    }
}