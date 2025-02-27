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
        IProductRepository productRepository;

        public OrderService(IOrderRepository myRepository,IProductRepository ProductRepository)
        {
            repository = myRepository;
            productRepository = ProductRepository;
        }

        public async Task<Order> getById(int id)
        {
            return await repository.getById(id);

        }

        public async Task<Order> createOrder(Order order)
        {
            if (!await checkSum(order))
                return null;
            return await repository.createOrder(order);
        }
        private async Task<bool> checkSum(Order order)
        {
            decimal sum = 0;
            List<Product> products = await productRepository.getAll(0, 0, null, null, null, []);

            foreach (var item in order.OrderItems)
            {
              sum+=products.Find(i=>i.ProductId==item.ProductId).Price;      
            }
            return sum == order.OrderSum;
        }

    }
}
