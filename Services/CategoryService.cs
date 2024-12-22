using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        IcategoryRepository repository;

        public CategoryService(IcategoryRepository myRepository)
        {
            repository = myRepository;

        }


        public async Task<List<Category>> getAll()
        {
            return await repository.getAll();

        }

    






    }
}
