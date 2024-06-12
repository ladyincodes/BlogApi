using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDapperController : Controller
    {
        [HttpGet(Name = "TestConnection")]
        public DateTime TestConnection()
        {
            return DateTime.Now;
        }
    }
}
