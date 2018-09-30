using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DotNetPractice.Data;
using DotNetPractice.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IMuzykRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IMuzykRepository repo, IMapper mapper)
        {
            _repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);
            return Ok(userToReturn);
        }

    }
}