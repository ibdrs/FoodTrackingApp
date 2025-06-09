using FoodTracking.Logic.Services;
using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Dtos.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserService userService;
        private readonly ILogger<FoodController> logger;


        public UserController(UserService userService, ILogger<FoodController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = await userService.RegisterUser(registerDto);
                return StatusCode(201, user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            try
            {
                var loggedInUser = await userService.LoginUser(loginDto);
                return StatusCode(201, loggedInUser);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error logging user in");

                return Conflict(new { message = ex.Message });
            }
        }


    }
}
