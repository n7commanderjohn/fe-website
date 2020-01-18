using System.Linq;
using AutoMapper;
using FEWebsite.API.DTOs.GameDTOs;
using FEWebsite.API.DTOs.GameGenreDTOs;
using FEWebsite.API.DTOs.MiscDTOs;
using FEWebsite.API.DTOs.PhotoDTOs;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Models;

namespace FEWebsite.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            this.CreateMapForUser();
            this.CreateMap<Gender, GenderDto>();
            this.CreateMap<Photo, PhotoForDetailedDto>();
            this.CreateMap<Game, GameForDetailedDto>();
            this.CreateMap<GameGenre, GameGenreForDetailedDto>();
        }

        private void CreateMapForUser()
        {
            this.CreateMap<User, UserForListDto>()
                .ForMember(destinationMember => destinationMember.PhotoUrl,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destinationMember => destinationMember.Age,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Birthday.CalculateAge()))
                .ForMember(destinationMember => destinationMember.Gender,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Gender.Description));
            this.CreateMap<User, UserForDetailedDto>()
                .ForMember(destinationMember => destinationMember.PhotoUrl,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destinationMember => destinationMember.Age,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Birthday.CalculateAge()))
                .ForMember(destinationMember => destinationMember.Gender,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Gender.Description))
                .ForMember(destinationMember => destinationMember.Games,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.FavoriteGames.ToList().Select(fg => fg.Game)))
                .ForMember(destinationMember => destinationMember.ListOfGames,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.FavoriteGames.ToList().Select(fg => fg.Game.Description)))
                .ForMember(destinationMember => destinationMember.Genres,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.FavoriteGenres.ToList().Select(fg => fg.GameGenre)))
                .ForMember(destinationMember => destinationMember.ListOfGenres,
                    memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.FavoriteGenres.ToList().Select(fg => fg.GameGenre.Description)));
            this.CreateMap<UserForUpdateDto, User>();
        }
    }
}
