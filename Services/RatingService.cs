using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {

        IRatingRepository repository;

        public RatingService(IRatingRepository myRepository)
        {
            repository = myRepository;

        }

        public async Task<Rating> createRating(Rating rating)
        {
            return await repository.createRating(rating);
        }
    }
}
