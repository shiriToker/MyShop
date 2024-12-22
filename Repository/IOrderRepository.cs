using Entity;

namespace Repository
{
    public interface IOrderRepository
    {
        Task<Order> createOrder(Order order);
        Task<Order> getById(int id);
    }
}