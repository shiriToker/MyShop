using Entity;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> getAll();
    }
}