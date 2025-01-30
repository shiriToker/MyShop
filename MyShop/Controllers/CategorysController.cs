using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=
namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        ICategoryService service;
        IMapper Mapper;
        public CategorysController(ICategoryService categoryService,IMapper mapper)
        {
            service = categoryService;
            Mapper = mapper;
        }


        // GET: api/<Category>
        [HttpGet]
        public async Task <ActionResult< IEnumerable<CategoryDTO>>> Get()
        {

            List<Category> categories = await service.getAll();
            List<CategoryDTO> categoryDTOs = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }



 
    }
}
