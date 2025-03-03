using Entity;
using Microsoft.Extensions.Logging;
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
        ILogger<OrderService> logger;

        public OrderService(IOrderRepository myRepository,IProductRepository ProductRepository,ILogger<OrderService> logger1)
        {

            repository = myRepository;
            productRepository = ProductRepository;
            logger = logger1;
        }

        public async Task<Order> getById(int id)
        {
            return await repository.getById(id);

        }

        public async Task<Order> createOrder(Order order)
        {
            if (!await checkSum(order))
            {
                logger.LogCritical($"UserId {order.UserId} tried to rob you, a security breach was found!!!!!");
                return null;             
            }
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
            return Math.Floor( sum )==Math.Floor((decimal)order.OrderSum);
        }

    }
}
