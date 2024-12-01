using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IMyService services;
        public UsersController(IMyService myServices)
        {
            services = myServices; 
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "My", "Shop" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task <ActionResult<User>> Get(int id)
        {
             User user =await services.getById(id);
            return (user == null ? NoContent() : Ok(user));
        }

        //POST api/<UsersController>
        [HttpPost]
        public  ActionResult Post([FromBody] User user)
        {
            User newUser = services.createUser(user);
            if(user!=null)
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            return BadRequest("סיסמה לא חזקה");
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromQuery] string UserName,string Password)
        {
            User user=await services.LogIn(Password, UserName);
            return (user == null ? NoContent() : Ok(user));
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User userToUpdate)
        {
            try
            {
              services.updateUser(id, userToUpdate);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost("password")]
        public IActionResult Password([FromQuery] string Password)
        {
            int result = services.Password(Password);
            return (result<3?BadRequest(result):Ok(result));
        }


        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
