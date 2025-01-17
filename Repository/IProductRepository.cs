using Entity;

namespace Repository
{
    public interface IProductRepository
    {

        Task<List<Product>> getAll(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoryIds);

    }
}