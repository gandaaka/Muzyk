using System.Collections.Generic;
using System.Threading.Tasks;
using Muzyk_API.Models;

namespace Muzyk_API.Data
{
    //Abeer
    public interface IRecommendationRepository
    {
        Task<IEnumerable<Recommendation>> GetRecommendations();
        Task<Recommendation> GetRecommendation(int id);
    }
}