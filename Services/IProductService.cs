using Entity;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> getAll(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}