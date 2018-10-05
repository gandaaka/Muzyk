
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DotNetPractice.Data;
using DotNetPractice.DTOS;
using DotNetPractice.Helpers;
using DotNetPractice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotNetPractice.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    //[EnableCors("AllowSpecificOrigin")]
    public class PhotosController : ControllerBase
    {
        private readonly IMuzykRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IMuzykRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            var photo = _mapper.Map<PhotoFromReturnDto>(photoFromRepo);
            return Ok(photo);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var file = photoForCreationDto.File;
            var uploadResults = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(700).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResults = _cloudinary.Upload(uploadParams);
                }
            }
            photoForCreationDto.PhotoUrl = uploadResults.Uri.ToString();
            photoForCreationDto.PublicId = uploadResults.PublicId;


            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(u => u.isProfilePhoto))
                photo.isProfilePhoto = true;

            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoFromReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo!");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> setMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.isProfilePhoto)
                return BadRequest("This is already the profile photo");

            var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);

            currentMainPhoto.isProfilePhoto = false;

            photoFromRepo.isProfilePhoto = true;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not set photo as profile photo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.isProfilePhoto)
                return BadRequest("You cannot delete your main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                    _repo.Delete(photoFromRepo);

            }
            else if (photoFromRepo.PublicId == null)
            {
                _repo.Delete(photoFromRepo);
            }

            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}