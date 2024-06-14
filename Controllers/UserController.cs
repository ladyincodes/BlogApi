using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs;
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
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration)
        {
            _dbContext = new AppDbContext(configuration);

            // Mapped UserToAddDTO to User model
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<UserToAddDTO, User>()));
        }

        [HttpGet("TestConnection")]
        public IActionResult TestConnection()
        {
            var currentDateTime = _dbContext.Database.SqlQueryRaw<DateTime>("SELECT GETDATE()")
            .AsEnumerable()
            .FirstOrDefault();
            return Ok(currentDateTime);
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                IEnumerable<User> users = _dbContext.Users.AsEnumerable<User>().ToList();
                return Ok(users);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to load data. {ex}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to load data. {ex}");
            }
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            try
            {
                User? userDb = _dbContext.Users
                .Where(u => u.UserId == user.UserId)
                .FirstOrDefault<User>();

                if (userDb == null)
                {
                    return NotFound("User was not found.");
                }

                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Active = user.Active;
                _dbContext.SaveChanges();

                return Ok("User updated successfully.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to update data. {ex}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update data. {ex}");
            }
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserToAddDTO user)
        {
            try
            {
                User userDb = _mapper.Map<User>(user);
                _dbContext.Users.Add(userDb);
                _dbContext.SaveChanges();
                return Ok("User added successfully.");

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to add user. {ex}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add user. {ex}");
            }

        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {

            try
            {
                User? userDb = _dbContext.Users.Find(userId);
                if (userDb == null) 
                {
                    return NotFound("There is no user with this id.");
                }

                _dbContext.Users.Remove(userDb);
                _dbContext.SaveChanges();
                return Ok("User deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to delete user. {ex}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete user. {ex}");
            }
        }

    }
}
