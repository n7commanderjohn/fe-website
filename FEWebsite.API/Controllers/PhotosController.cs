using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.DTOs.PhotoDTOs;
using FEWebsite.API.Helpers;
using FEWebsite.API.Models;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        public IUserInfoRepositoryService UserService { get; }
        public IMapper Mapper { get; }
        public IOptions<CloudinarySettings> CloudinaryConfig { get; }

        public Cloudinary Cloudinary { get; }

        public PhotosController(IUserInfoRepositoryService userService,
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.UserService = userService;
            this.Mapper = mapper;
            this.CloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            this.Cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForUploadDto photoForUploadDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return this.Unauthorized();
            }

            var currentUser = await this.UserService.GetUser(userId).ConfigureAwait(false);

            var file = photoForUploadDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity(CloudinaryDotNet.Gravity.Auto)
                    };

                    uploadResult = this.Cloudinary.Upload(uploadParams);
                }
            }

            photoForUploadDto.Url = uploadResult.Uri.ToString();
            photoForUploadDto.PublicId = uploadResult.PublicId;

            var photo = this.Mapper.Map<Photo>(photoForUploadDto);

            if (!currentUser.Photos.Any(u => u.IsMain))
            {
                photo.IsMain = true;
            }

            currentUser.Photos.Add(photo);

            if (await this.UserService.SaveAll().ConfigureAwait(false))
            {
                return this.Ok();
            }

            return BadRequest("Photo upload failed.");
        }
    }
}
