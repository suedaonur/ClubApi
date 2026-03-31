using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.Clubs.Queries.GetAllClubs;

public class GetAllClubsQuery : IRequest<List<Club>> { }
