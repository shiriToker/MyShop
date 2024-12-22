using Entity;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<Category>> getAll();
    }
}