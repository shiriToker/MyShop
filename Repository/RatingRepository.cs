using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        MyShop328306782Context _dbcontext;

        public RatingRepository(MyShop328306782Context context)
        {
            _dbcontext = context;

        }

        public async Task<Rating> createRating(Rating rating)
        {
            await _dbcontext.Ratings.AddAsync(rating);
            await _dbcontext.SaveChangesAsync();
            return rating;
        }

    }
}
