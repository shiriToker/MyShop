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

        public async Task<List<Product>> getAll(int position,int skip,string? desc,int? minPrice,int? maxPrice, int?[]categoryIds)
        {
     
            var query=_dbcontext.Products.Where(product=>
            (desc==null?(true):(product.Description.Contains(desc)))
            &&((minPrice==null)?(true):(product.Price>=minPrice))
            &&((maxPrice==null)?(true)

        }
    



    

    }
}
