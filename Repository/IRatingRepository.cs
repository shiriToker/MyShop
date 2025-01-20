using Entity;

namespace Repository
{
    public interface IRatingRepository
    {
        Task<Rating> createRating(Rating rating);
    }
}