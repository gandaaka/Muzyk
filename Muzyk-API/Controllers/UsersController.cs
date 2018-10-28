using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Muzyk_API.Data;
using Muzyk_API.DTOS;
using Muzyk_API.Helpers;
using Muzyk_API.Models;

namespace Muzyk_API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
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
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody]UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}/follow/{recepientId}")]
        public async Task<IActionResult> FollowUser(int id, int recepientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var follow = await _repo.GetFollow(id, recepientId);

            if (follow != null)
                return BadRequest("you have already followed this user");

            if (await _repo.GetUser(recepientId) == null)
                return NotFound();

            follow = new Follow
            {
                FollowerId = id,
                FolloweeId = recepientId
            };

            _repo.Add<Follow>(follow);

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Failed to follow the user");
        }

        [HttpGet("{id}/Booking")]
        public IActionResult UserBookings(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var booking = _repo.GetUserBooking(id);
            var bookingForReturn = _mapper.Map<IEnumerable<BookingsToReturnDto>>(booking);
            return Ok(bookingForReturn);
        }

        [HttpPost("{id}/Booking/{recipientId}")]
        public async Task<IActionResult> BookMeetingWithUser(int id, [FromBody]BookingToCreateDto bookingToCreateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (bookingToCreateDto.Date == null)
                return BadRequest("Please specify booking date");

            if (bookingToCreateDto.Date <= DateTime.Now)
                return BadRequest("Date is required and you can book meeting on past date");

            var booking = await _repo.GetBooking(id, bookingToCreateDto.RecipientId);

            if (booking != null)
            {
                if (DateTime.Equals(booking.BookingDate, bookingToCreateDto.Date))
                {
                    return BadRequest("You have a booking with this user on same date");
                }
            }


            if (await _repo.GetUser(bookingToCreateDto.RecipientId) == null)
                return NotFound();

            booking = new Booking
            {
                BookerId = id,
                BookeeId = bookingToCreateDto.RecipientId,
                Title = bookingToCreateDto.Title,
                Desc = bookingToCreateDto.Desc,
                BookingDate = bookingToCreateDto.Date
            };

            _repo.Add<Booking>(booking);

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Failed to do booking the user");
        }
    }
}