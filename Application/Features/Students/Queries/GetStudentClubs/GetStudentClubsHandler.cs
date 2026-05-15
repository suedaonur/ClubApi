using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Queries.GetStudentClubs
{
    public class GetStudentClubsHandler : IRequestHandler<GetStudentClubsQuery, List<StudentClubDto>>
    {
        // DİKKAT: Artık Student değil, ClubMember repository'si inject ediyoruz
        private readonly IGenericRepository<ClubMember> _clubMemberRepository;
        private readonly IMapper _mapper;

        public GetStudentClubsHandler(IGenericRepository<ClubMember> clubMemberRepository, IMapper mapper)
        {
            _clubMemberRepository = clubMemberRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentClubDto>> Handle(GetStudentClubsQuery request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Senin metodunla kulüp üyeliklerini "Club" bilgisiyle birlikte çekiyoruz.
            var allMemberships = await _clubMemberRepository.GetAllWithIncludesAsync(cm => cm.Club);

            // 2. ADIM: Gelen liste içinden sadece isteği atan öğrencinin kayıtlarını filtreliyoruz.
            var studentClubs = allMemberships
                .Where(cm => cm.StudentId == request.StudentId)
                .ToList();

            // Eğer öğrencinin hiç kulübü yoksa boş liste dönüyoruz.
            if (!studentClubs.Any())
            {
                return new List<StudentClubDto>();
            }

            // 3. ADIM: AutoMapper ile ClubMember nesnelerini -> StudentClubDto nesnelerine çeviriyoruz.
            var result = _mapper.Map<List<StudentClubDto>>(studentClubs);

            return result;
        }
    }
}