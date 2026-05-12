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
        // Student -> StudentDto dönüşümüne izin ver
        CreateMap<Student, StudentDto>().ReverseMap();

        // Buraya ileride Club -> ClubDto gibi diğer eşleştirmeleri de ekleyeceğiz
    }
}
