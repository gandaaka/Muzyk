using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Muzyk_API.Data;
using Muzyk_API.Helpers;

namespace Muzyk_API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/recommendedUsers")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationRepository _repo;
        public RecommendationController(IRecommendationRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getRecommmedations()
        {
            var recommendations = await _repo.GetRecommendations();
            return Ok(recommendations);
        }

        [HttpGet("{id}", Name="GetRecommmendedUser")]
        public async Task<IActionResult> getRecommendation(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recommendedUser = await _repo.GetRecommendation(userId);
            return Ok(recommendedUser);
        }
    }
}