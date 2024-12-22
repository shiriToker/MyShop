using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository

    {
        MyShop328306782Context _dbcontext;
        public ProductRepository(MyShop328306782Context context)
        {
            _dbcontext = context;

        }

        public async Task<List<Product>> getAll()
        {
            List<Product> products = await _dbcontext.Products.Include(c=>c.Caregory).ToListAsync();
            return products;

        }
    


    

    }
}
