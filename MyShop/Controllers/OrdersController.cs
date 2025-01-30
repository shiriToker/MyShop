using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService service;
        IMapper Mapper;
        public OrdersController(IOrderService categoryService,IMapper mapper)
        {
            service = categoryService;
            Mapper = mapper;
        }


        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await service.getById(id);
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
