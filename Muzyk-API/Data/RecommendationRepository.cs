using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Muzyk_API.Models;

namespace Muzyk_API.Data
{
    // abeer
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly DataContext _context;
        public RecommendationRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<Recommendation> GetRecommendation(int id)
        {
            var recommendation = await _context.Recommendations.Include(r => r.RecommendedUsers).FirstOrDefaultAsync(u => u.UserId == id);
            return recommendation;
        }

        public async Task<IEnumerable<Recommendation>> GetRecommendations()
        {
            var recommendations = await _context.Recommendations.Include(r => r.RecommendedUsers).ToListAsync();
            return recommendations;
        }
    }
}