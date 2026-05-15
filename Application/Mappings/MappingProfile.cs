using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Student, StudentDto>().ReverseMap();

        CreateMap<ClubMember, StudentClubDto>()
    .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.Name));
    }
}
