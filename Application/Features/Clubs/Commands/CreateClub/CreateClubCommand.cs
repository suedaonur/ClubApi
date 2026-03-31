using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Clubs.Commands.CreateClub;

public class CreateClubCommand : IRequest<int> // Geriye yeni oluşan Kulüp ID'sini dönecek
{
    public string Name { get; set; }
    public string Description { get; set; }
}
