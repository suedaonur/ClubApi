using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.Posts.Queries.GetClubPosts;

public class GetClubPostsQuery : IRequest<List<Post>>
{
    public int ClubId { get; set; }
}
