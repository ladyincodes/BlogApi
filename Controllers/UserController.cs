using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public UserController(IConfiguration configuration)
        {
            Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("TestConnection")]
        public DateTime TestConnection()
        {
            return DateTime.Now;
        }
    }
}
