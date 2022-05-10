using AutoMapper;
using EverlyExperts.Data.Dtos;
using EverlyExperts.Models;

namespace EverlyExperts.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberCreationDto>();
        }
    }
}
