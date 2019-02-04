using AutoMapper;
using LogiAppMonitor.API.Dtos;
using LogiAppMonitor.API.Models;

namespace LogiAppMonitor.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {


            // CreateMap<User, UserForDetailedDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //     });
        }
    }
}