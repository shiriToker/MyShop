using Entity;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> createOrder(Order order);
        Task<Order> getById(int id);
    }
}