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
    public class ProductsController : ControllerBase
    {

        IProductService service;
        IMapper Mapper;
        public ProductsController(IProductService categoryService, IMapper mapper)
        {
            service = categoryService;
            Mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {


            List<Product> products = await service.getAll();
            List<ProductDTO> productDTOs = Mapper.Map<List<Product>, List<ProductDTO>>(products);
            return Ok(productDTOs) ;
        }
    }
}
