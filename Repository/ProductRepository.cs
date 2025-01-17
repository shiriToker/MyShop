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

        public async Task<List<Product>> getAll(int position,int skip,string? name,int? minPrice,int? maxPrice, int?[]categoryIds)
        {

            var query = _dbcontext.Products.Where(product =>
            (name == null ? (true) : (product.ProductName.Contains(name)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CaregoryId))))
               .OrderBy(product => product.Price);
            //.skip((position - 1) * skip)
            //.Take(skip);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Include(p => p.Caregory).ToListAsync();
            return products;

        }
    



    

    }
}
