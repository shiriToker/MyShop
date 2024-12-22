using Entity;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> getAll();

    }
}