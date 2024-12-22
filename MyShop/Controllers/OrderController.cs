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
    public class OrderController : ControllerBase
    {
        IOrderService service;
        IMapper Mapper;
        public OrderController(IOrderService categoryService,IMapper mapper)
        {
            service = categoryService;
            Mapper = mapper;
        }


        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await service.getById(id);
            OrderDTO orderDTO = Mapper.Map<Order, OrderDTO>(order);
            return  Ok(orderDTO);
        }

        // POST api/<OrderController>
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
