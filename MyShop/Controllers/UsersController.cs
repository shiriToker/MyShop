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
        public ActionResult<User> Get(int id)
        {
            User user = services.getById(id);
            return (user == null ? NoContent() : Ok(user));
        }

        //POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            User newUser = services.createUser(user);
            return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);

        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public IActionResult LogIn([FromQuery] string UserName,string Password)
        {
            User user=services.LogIn(Password, UserName);
            return (user == null ? NoContent() : Ok(user));
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userToUpdate)
        {
            services.updateUser(id, userToUpdate);



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
