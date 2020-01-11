using AutoMapper;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Models;

namespace FEWebsite.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            this.CreateMap<User, UserForListDto>();
            this.CreateMap<User, UserForDetailedDto>();
        }
    }
}
