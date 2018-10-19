using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Muzyk_API.Data;
using Muzyk_DTOS;
using Muzyk_API.Helpers;
using Muzyk_API.Models;

namespace Muzyk_API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/videos")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IMuzykRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public VideosController(IMuzykRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
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

        [HttpGet("{id}", Name="GetVideo")]
        public async Task<IActionResult> GetVideo(int id){
            var videoFromRepo = await _repo.GetVideo(id);
            var video = _mapper.Map<VideoForReturnDto>(videoFromRepo);
            return Ok(video);
        }

        [HttpPost]
        public async Task<IActionResult> AddVideoForUser(int userId, [FromForm]VideoForCreationDto videoForCreationDto){
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var file = videoForCreationDto.File;
            var uploadResults = new  VideoUploadResult();

            if (file.Length > 0) {
                using (var stream = file.OpenReadStream()){
                    var uploadParams = new VideoUploadParams(){
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };
                    uploadResults = _cloudinary.Upload(uploadParams);
                }
            }
            videoForCreationDto.VideoUrl = uploadResults.Uri.ToString();
            videoForCreationDto.PublicId = uploadResults.PublicId;

            var video = _mapper.Map<Models.Video>(videoForCreationDto);
            userFromRepo.Videos.Add(video);

            if (await _repo.SaveAll()){
                var videoToReturn = _mapper.Map<VideoForReturnDto>(video);
                return CreatedAtRoute("GetVideo", new VideoForReturnDto{Id = video.Id}, videoToReturn);
            }

            return BadRequest("Could not add the video!");
        }
    }
}