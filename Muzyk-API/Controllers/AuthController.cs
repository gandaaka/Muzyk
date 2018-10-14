using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using Muzyk_API.Data;
using Muzyk_API.DTOS;
using Muzyk_API.Models;

namespace Muzyk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper _mapper)
        {
            this._mapper = _mapper;
            _repo = repo;
            _config = config;
        }

        //register method
        [EnableCors("AllowSpecificOrigin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            userToRegisterDto.Username = userToRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userToRegisterDto.Username))
                return BadRequest("Username already exists !");

            var userToCreate = _mapper.Map<User>(userToRegisterDto);

            var createdUser = await _repo.Register(userToCreate, userToRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailDto>(createdUser);

            return CreatedAtRoute("GetUser", new {controller ="Users", id = createdUser.Id}, userToReturn);
        }

        //login method
        [EnableCors("AllowSpecificOrigin")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(0.2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
    }
}