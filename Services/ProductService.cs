using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository repository;

        public ProductService(IProductRepository myRepository)
        {
            repository = myRepository;

        }


        public async Task<List<Product>> getAll(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await repository.getAll(position, skip, name, minPrice, maxPrice, categoryIds);

        }



    }
}
