using System.Linq;
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
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public IUserService UserService { get; }
        public IMapper Mapper { get; }
        public IOptions<CloudinarySettings> CloudinaryConfig { get; }

        public Cloudinary Cloudinary { get; }

        public PhotoController(IUserService userService,
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

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await this.UserService.GetPhoto(id).ConfigureAwait(false);

            var photo = this.Mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return this.Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForUploadDto photoForUploadDto)
        {
            var unauthorization = this.CheckIfLoggedInUserIsAuthorized(userId,
                "You are not logged in as the user you are trying to upload the photo for.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            var currentUser = await this.UserService.GetUser(userId).ConfigureAwait(false);
            var file = photoForUploadDto.File;
            var uploadResult = new ImageUploadResult();

            bool doesPhotoExist = file == null;
            if (doesPhotoExist)
            {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "The photo to be uploaded was not found."));
            }
            else
            {
                bool doesPhotoHaveData = file.Length > 0;
                if (doesPhotoHaveData)
                {
                    using(var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity(CloudinaryDotNet.Gravity.Face)
                        };

                        uploadResult = this.Cloudinary.Upload(uploadParams);
                    }
                }
            }

            photoForUploadDto.Url = uploadResult.Uri.ToString();
            photoForUploadDto.PublicId = uploadResult.PublicId;

            var photo = this.Mapper.Map<Photo>(photoForUploadDto);
            bool AreNoneOfUserPhotosSetToMain = !currentUser.Photos.Any(u => u.IsMain);
            if (AreNoneOfUserPhotosSetToMain)
            {
                photo.IsMain = true;
            }

            currentUser.Photos.Add(photo);
            bool isDatabaseSaveSuccessful = await this.UserService.SaveAll().ConfigureAwait(false);
            if (isDatabaseSaveSuccessful)
            {
                var photoToReturn = this.Mapper.Map<PhotoForReturnDto>(photo);

                return this.CreatedAtRoute("GetPhoto", new { userId, photo.Id }, photoToReturn);
            }

            return BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                "Photo upload to server failed."));
        }

        [HttpPut("{photoId}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int photoId)
        {
            var user = await this.UserService.GetUser(userId).ConfigureAwait(false);
            var unauthorizedObj = this.IsUserAndPhotoAuthorized(user, photoId);

            bool isRequestAuthorized = unauthorizedObj == null;
            if (isRequestAuthorized)
            {
                var photoToSetAsMain = await this.UserService.GetPhoto(photoId).ConfigureAwait(false);
                if (photoToSetAsMain == null)
                {
                    return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                        "This photo has failed to be retrieved from the database."));
                }
                else if (photoToSetAsMain.IsMain)
                {
                    return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                        "This photo is already the user's main photo."));
                }

                var setPhoto = await this.UserService
                    .SetUserPhotoAsMain(userId, photoToSetAsMain).ConfigureAwait(false);
                if (!setPhoto.IsMain)
                {
                    return this.Problem(detail: "Setting the selected photo as the main photo failed.", statusCode: 500);
                }

                bool isDatabaseSaveSuccessful = await this.UserService.SaveAll().ConfigureAwait(false);
                if (isDatabaseSaveSuccessful)
                {
                    return this.NoContent();
                }
            }
            else
            {
                return unauthorizedObj;
            }

            return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                "Setting the selected photo as the main photo failed."));
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int userId, int photoId)
        {
            const string failureBase = "Failed to delete the selected photo from the ";
            var user = await this.UserService.GetUser(userId).ConfigureAwait(false);
            var unauthorizedObj = this.IsUserAndPhotoAuthorized(user, photoId);

            bool isRequestAuthorized = unauthorizedObj == null;
            if (isRequestAuthorized)
            {
                var photoToDelete = await this.UserService.GetPhoto(photoId).ConfigureAwait(false);
                if (photoToDelete.IsMain)
                {
                    // return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest()){
                    //     Response = "You can not delete your main photo."
                    // }); // for now, deletion will be allowed.
                    var photoToReplaceAsMain = user.Photos.FirstOrDefault(p => !p.IsMain);
                    if (photoToReplaceAsMain != null)
                    {
                        photoToReplaceAsMain.IsMain = true;
                    }
                    photoToDelete.IsMain = false;
                }

                bool isPhotoOnCloudinary = photoToDelete.PublicId != null;
                if (isPhotoOnCloudinary)
                {
                    var deletionParams = new DeletionParams(photoToDelete.PublicId);
                    var result = this.Cloudinary.Destroy(deletionParams);

                    bool isPhotoDestructionOnCloudinarySuccessful = result.Result.Equals("ok");
                    bool photoNotFound = result.Result.Equals("not found");
                    if (isPhotoDestructionOnCloudinarySuccessful || photoNotFound)
                    {
                        this.UserService.Delete(photoToDelete);
                    }
                    else
                    {
                        return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                            failureBase + "cloud server."));
                    }
                }
                else
                {
                    this.UserService.Delete(photoToDelete);
                }

                bool isDatabaseSaveSuccessful = await this.UserService.SaveAll().ConfigureAwait(false);
                if (isDatabaseSaveSuccessful)
                {
                    return this.Ok();
                }
                else
                {
                    return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                        failureBase + "database."));
                }
            }
            else
            {
                return unauthorizedObj;
            }
        }

        private UnauthorizedObjectResult IsUserAndPhotoAuthorized(User user, int photoId)
        {
            var unauthorization = this.CheckIfLoggedInUserIsAuthorized(user.Id,
                "This isn't the currently logged in user.");
            if (unauthorization != null)
            {
                return unauthorization;
            }
            if (!user.DoesPhotoExist(photoId))
            {
                return this.Unauthorized("This photo id doesn't match any of the user's photos.");
            }

            return null;
        }
    }
}
