using Entity;

namespace Repository
{
    public interface IcategoryRepository
    {
        Task<List<Category>> getAll();
    }
}