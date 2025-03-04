using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class categoryRepository : IcategoryRepository
    {
        MyShop328306782Context _dbcontext;
        public categoryRepository(MyShop328306782Context context)
        {
            _dbcontext = context;

        }

        public async Task<List<Category>> getAll()
        {
            return await _dbcontext.Categories.ToListAsync();
        } 

    }
}
