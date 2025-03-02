using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=
namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IMapper Mapper;
        private readonly IMemoryCache cache;
        public CategorysController(ICategoryService categoryService, IMapper mapper, IMemoryCache memoryCache)
        {
            service = categoryService;
            Mapper = mapper;
            cache = memoryCache;
        }


        // GET: api/<Category>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            if (!cache.TryGetValue("categories", out List<Category> categories)) 
            { 

             categories = await service.getAll();         
             cache.Set("categories", categories, TimeSpan.FromMinutes(10));

            }
            List<CategoryDTO> categoryDTOs = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);

            return categoryDTOs!=null?Ok(categoryDTOs):NoContent();
        }

    }
   
}
