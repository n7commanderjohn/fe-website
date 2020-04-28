using System.Linq;

using AutoMapper;

using FEWebsite.API.Controllers.DTOs.GameDTOs;
using FEWebsite.API.Controllers.DTOs.GameGenreDTOs;
using FEWebsite.API.Controllers.DTOs.MiscDTOs;
using FEWebsite.API.Controllers.DTOs.PhotoDTOs;
using FEWebsite.API.Controllers.DTOs.UserDTOs;
using FEWebsite.API.Core.Models;
using FEWebsite.API.Core.Models.ManyToManyModels;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            this.CreateMapForUser();
            this.CreateMap<Gender, GenderDto>();
            this.CreateMap<Photo, PhotoForDetailedDto>();
            this.CreateMap<Photo, PhotoForReturnDto>();
            this.CreateMap<PhotoForUploadDto, Photo>();
            this.CreateMap<Game, GameForDetailedDto>();
            this.CreateMap<GameGenre, GameGenreForDetailedDto>();
        }

        private void CreateMapForUser()
        {
            // no way to avoid null references for these, since null propagator operator cannot be used in expression tree
            this.CreateMap<User, UserForLoginResponseDto>()
                .ForMember(dest => dest.PhotoUrl,
                    source => source.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url));
            this.CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl,
                    source => source.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age,
                    source => source.MapFrom(source => source.Birthday.CalculateAge()))
                .ForMember(dest => dest.Gender,
                    source => source.MapFrom(source => source.Gender.Description));
            this.CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl,
                    source => source.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age,
                    source => source.MapFrom(source => source.Birthday.CalculateAge()))
                .ForMember(dest => dest.Gender,
                    source => source.MapFrom(source => source.Gender.Description))
                .ForMember(dest => dest.GenderId,
                    source => source.MapFrom(source => source.Gender.Id))
                .ForMember(dest => dest.Games,
                    source => source.MapFrom(source => source.FavoriteGames.Select(fg => fg.Game)))
                .ForMember(dest => dest.ListOfGames,
                    source => source.MapFrom(source => source.FavoriteGames.Select(fg => fg.Game.Description)))
                .ForMember(dest => dest.Genres,
                    source => source.MapFrom(source => source.FavoriteGenres.Select(fg => fg.GameGenre)))
                .ForMember(dest => dest.ListOfGenres,
                    source => source.MapFrom(source => source.FavoriteGenres.Select(fg => fg.GameGenre.Description)))
                .ForMember(dest => dest.ListOfLikees,
                    source => source.MapFrom(source => source.Likees.Select(l => l.LikeeId)));
            this.CreateMap<UserForUpdateDto, User>()
                .AfterMap(this.MapNewFaveGamesAndGenres);
            this.CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.Gender,
                    source => source.Ignore())
                .ForMember(dest => dest.GenderId,
                    source => source.MapFrom(source => source.Gender));
            this.CreateMap<UserMessageCreationDto, UserMessage>()
                .ReverseMap();
            this.CreateMap<UserMessage, UserMessageToReturnDto>()
                .ForMember(dest => dest.SenderPhotoUrl,
                    source => source.MapFrom(source => source.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl,
                    source => source.MapFrom(source => source.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }

        private void MapNewFaveGamesAndGenres(UserForUpdateDto userForUpdateDto, User currentUser)
        {
            if (currentUser.Name == null)
            {
                currentUser.Name = currentUser.Username;
            }
            currentUser.Username = currentUser.Username.ToLower();
            MapGames(userForUpdateDto, currentUser);
            MapGameGenres(userForUpdateDto, currentUser);

            static void MapGames(UserForUpdateDto userForUpdateDto, User currentUser)
            {
                var currFaveGames = currentUser.FavoriteGames.ToList();
                var updatedFaveGames = userForUpdateDto.Games.Select(game => new UserGame()
                {
                    Game = game,
                    GameId = game.Id,
                    User = currentUser,
                    UserId = currentUser.Id,
                });

                var addedFaveGames = updatedFaveGames
                    .Where(addedGames => !currFaveGames.Any(g => g.GameId == addedGames.GameId));
                foreach (var game in addedFaveGames)
                {
                    currentUser.FavoriteGames.Add(game);
                }

                var removedFaveGames = currFaveGames
                    .Where(removedGames => !updatedFaveGames.Any(g => g.GameId == removedGames.GameId));
                foreach (var game in removedFaveGames)
                {
                    currentUser.FavoriteGames.Remove(game);
                }
            }

            static void MapGameGenres(UserForUpdateDto userForUpdateDto, User currentUser)
            {
                var currFaveGenres = currentUser.FavoriteGenres.ToList();
                var updatedFaveGenres = userForUpdateDto.Genres.Select(genre => new UserGameGenre()
                {
                    GameGenre = genre,
                    GameGenreId = genre.Id,
                    User = currentUser,
                    UserId = currentUser.Id,
                });

                var addedFaveGenres = updatedFaveGenres
                    .Where(addedGenres => !currFaveGenres.Any(g => g.GameGenreId == addedGenres.GameGenreId));
                foreach (var genres in addedFaveGenres)
                {
                    currentUser.FavoriteGenres.Add(genres);
                }

                var removedFaveGenres = currFaveGenres
                    .Where(removedGenres => !updatedFaveGenres.Any(g => g.GameGenreId == removedGenres.GameGenreId));
                foreach (var genre in removedFaveGenres)
                {
                    currentUser.FavoriteGenres.Remove(genre);
                }
            }
        }

        // keeping this for now just in case the other way doesn't work???
        // private void MapNewFaveGamesAndGenres_backup(UserForUpdateDto userForUpdateDto, User currentUser)
        // {
        //     var faveGames = currentUser.FavoriteGames.ToList();
        //     foreach (var game in userForUpdateDto.Games)
        //     {
        //         var isThisANewFavorite = !faveGames.Any(fg => fg.GameId == game.Id);
        //         if (isThisANewFavorite)
        //         {
        //             var newGame = new UserGame()
        //             {
        //                 Game = game,
        //                 GameId = game.Id,
        //                 User = currentUser,
        //                 UserId = currentUser.Id,
        //             };
        //             currentUser.FavoriteGames.Add(newGame);
        //         }
        //     }
        //     foreach (var game in faveGames)
        //     {
        //         var isThisExistingFavoriteNoMore = !userForUpdateDto.Games.Any(g => g.Id == game.GameId);
        //         if (isThisExistingFavoriteNoMore)
        //         {
        //             currentUser.FavoriteGames.Remove(game);
        //         }
        //     }

        //     var faveGenres = currentUser.FavoriteGenres.ToList();
        //     foreach (var genre in userForUpdateDto.Genres)
        //     {
        //         var isThisANewFavorite = !faveGenres.Any(fg => fg.GameGenreId == genre.Id);
        //         if (isThisANewFavorite)
        //         {
        //             var newGenre = new UserGameGenre()
        //             {
        //                 GameGenre = genre,
        //                 GameGenreId = genre.Id,
        //                 User = currentUser,
        //                 UserId = currentUser.Id,
        //             };
        //             currentUser.FavoriteGenres.Add(newGenre);
        //         }
        //     }
        //     foreach (var genre in faveGenres)
        //     {
        //         var isThisExistingFavoriteNoMore = !userForUpdateDto.Genres.Any(g => g.Id == genre.GameGenreId);
        //         if (isThisExistingFavoriteNoMore)
        //         {
        //             currentUser.FavoriteGenres.Remove(genre);
        //         }
        //     }
        // }
    }
}
