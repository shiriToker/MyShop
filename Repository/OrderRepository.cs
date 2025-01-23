using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {


        MyShop328306782Context _dbcontext;
        public OrderRepository(MyShop328306782Context context)
        {
            _dbcontext = context;

        }

        public async Task<Order> getById(int id)
        {
            Order order = await _dbcontext.Orders.Include(currentOrder => currentOrder.User).FirstOrDefaultAsync(currentOrder => currentOrder.OrderId == id);
            return order == null ? null : order;
            //return order, if it's null- will return null

        }
        public async Task<Order> createOrder(Order newOrder)
        {

            newOrder.OrserDate = DateOnly.FromDateTime(DateTime.Now);
            await _dbcontext.Orders.AddAsync(newOrder);
            await _dbcontext.SaveChangesAsync();
            Order order = await _dbcontext.Orders.Include(currentOrder => currentOrder.User).FirstOrDefaultAsync(currentOrder => currentOrder.OrderId == newOrder.OrderId);
            return order;


        }
    }
}
