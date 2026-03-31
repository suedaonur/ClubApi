using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clubs.Commands.UpdateClub
{
    public class UpdateClubHandler : IRequestHandler<UpdateClubCommand, bool>
    {
        private readonly IGenericRepository<Club> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClubHandler(IGenericRepository<Club> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            
            var club = await _repository.GetByIdAsync(request.Id);
            if (club == null) return false;

            club.Name = request.Name;
            club.Description = request.Description;

            _repository.Update(club);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
