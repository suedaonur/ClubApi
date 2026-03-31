using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.Features.Clubs.Commands.DeleteClub
{
    public class DeleteClubHandler : IRequestHandler<DeleteClubCommand, bool>
    {
        private readonly IGenericRepository<Club> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteClubHandler(IGenericRepository<Club> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            
            var club = await _repository.GetByIdAsync(request.Id);
            if (club == null) return false;

            _repository.Delete(club);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
