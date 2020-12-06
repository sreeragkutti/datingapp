using System.Linq;
using API.DTO;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(des => des.PhotoUrl, 
                            opt => opt.MapFrom(
                                src => src.Photos.FirstOrDefault(x=>x.IsMain).Url)
                            )
                .ForMember(des => des.Age,
                        opt => opt.MapFrom(
                            src => src.DateOfBirth.CalculateAge()
                        ));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}