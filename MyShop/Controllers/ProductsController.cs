using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService service;
        private readonly IMapper Mapper;
        private readonly IMemoryCache cache;
        public ProductsController(IProductService categoryService, IMapper mapper,IMemoryCache memoryCache)
        {
            service = categoryService;
            Mapper = mapper;
            cache = memoryCache;
        }

        // GET: api/<ProductsController>
        [HttpGet]
       
        public async  Task <ActionResult<List<ProductDTO>>> getAll([FromQuery] int position, [FromQuery] int skip, [FromQuery] string? name, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            if (!cache.TryGetValue("products", out List<Product> products))
            {
                products = await service.getAll(position, skip, name, minPrice, maxPrice,categoryIds);
                cache.Set("products", products, TimeSpan.FromMinutes(10));
            }
            List<ProductDTO> productDTOs = Mapper.Map<List<Product>, List<ProductDTO>>(products);
            return Ok(productDTOs) ;
        }
    }
}
