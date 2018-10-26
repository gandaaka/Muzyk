using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Muzyk_API.Data;
using Muzyk_API.Helpers;

namespace Muzyk_API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class BookingController
    {
        private readonly IMuzykRepository _repo;
        private readonly IMapper _mapper;

        public BookingController(IMuzykRepository repo, IMapper mapper)
        {
            _repo = repo;
            this._mapper = mapper;
        }
    }
}