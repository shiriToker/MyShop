using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService service;
        private readonly IMapper Mapper;
        private readonly IMemoryCache cache;
        public OrdersController(IOrderService categoryService,IMapper mapper,IMemoryCache memoryCache)
        {
            service = categoryService;
            Mapper = mapper;
            cache = memoryCache;
        }


        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            if (!cache.TryGetValue("orders", out Order order))
            {

                order = await service.getById(id);
                cache.Set("orders", order, TimeSpan.FromMinutes(10));

            }
            OrderDTO orderDTO = Mapper.Map<Order, OrderDTO>(order);
            return  Ok(orderDTO);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderCreatDTO order)
        {
            Order orderDTO = Mapper.Map<OrderCreatDTO, Order >(order);
            Order newOrder = await service.createOrder(orderDTO);
            OrderDTO orderDTO1= Mapper.Map< Order, OrderDTO> (newOrder);
            if (newOrder != null)
                return CreatedAtAction(nameof(Get), new { id = newOrder.OrderId }, orderDTO1);
            return BadRequest();
        }

     
    }
}
