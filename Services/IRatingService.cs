using Entity;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> createRating(Rating rating);
    }
}