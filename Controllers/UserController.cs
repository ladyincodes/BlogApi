using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _dataContext;

        public UserController(IConfiguration configuration)
        {
            _dataContext = new AppDbContext(configuration);
        }

        [HttpGet("TestConnection")]
        public IActionResult TestConnection()
        {
            var currentDateTime = _dataContext.Database.SqlQueryRaw<DateTime>("SELECT GETDATE()")
            .AsEnumerable()
            .FirstOrDefault();
            return Ok(currentDateTime);
        }
    }
}
