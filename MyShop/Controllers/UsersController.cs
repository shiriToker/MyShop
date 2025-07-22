using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMyService services;
        private readonly IMapper Mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IMemoryCache cache;

        public UsersController(IMyService myServices, IMapper mapper, ILogger<UsersController> logger,IMemoryCache memoryCache)
        {
            services = myServices;
            Mapper = mapper;
            _logger = logger;
            cache = memoryCache;
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetByIdDTO>> Get(int id)
        {
            if (!cache.TryGetValue("user", out User user))
            {
                user = await services.getById(id);
                cache.Set("user", user, TimeSpan.FromMinutes(10));
            }
            UserGetByIdDTO userGetByIdDTO = Mapper.Map<User, UserGetByIdDTO>(user);
            return Ok(userGetByIdDTO);
        }

        //POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateDTO user)
        {
            User newuser = Mapper.Map<UserCreateDTO, User>(user);
            User newUser = await services.createUser(newuser);
            if (user != null)
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            return BadRequest("סיסמה לא חזקה");
        }
        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromQuery] string UserName, string Password)
        {
            _logger.LogInformation($"Loggin attempted with user name {UserName} and password {Password}");

            User user = await services.LogIn(Password, UserName);
            if (user == null)
                return NoContent();

            // יצירת JWT
            var secret = HttpContext.RequestServices.GetRequiredService<IConfiguration>( )["Jwt:Key"];
            var token = JwtTokenHelper.GenerateJwtToken(user.UserId.ToString(), secret);
            // שמירת הטוקן בקוקי
            var isDev = HttpContext.Request.Host.Host == "localhost" || HttpContext.Request.Host.Host == "127.0.0.1";
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = !isDev, // בפיתוח לא דורש HTTPS
                SameSite = isDev ? SameSiteMode.Lax : SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(2)
            };
            Response.Cookies.Append("jwtToken", token, cookieOptions);
            return Ok(new { message = "התחברת בהצלחה" });
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UserCreateDTO userToUpdate)
        {
            User newUserToUpdate = Mapper.Map<UserCreateDTO, User>(userToUpdate);
           return Ok( await services.updateUser(id, newUserToUpdate));           
        }

        [HttpPost("password")]
        public IActionResult Password([FromQuery] string Password)
        {
            int result = services.Password(Password);
            return (result < 3 ? BadRequest(result) : Ok(result));
        }

    }
}
