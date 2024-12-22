using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository repository;

        public OrderService(IOrderRepository myRepository)
        {
            repository = myRepository;

        }

        public async Task<Order> getById(int id)
        {
            return await repository.getById(id);

        }

        public async Task<Order> createOrder(Order order)
        {
            return await repository.createOrder(order);
        }




    }
}
