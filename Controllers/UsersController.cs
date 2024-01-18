using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly  SQLiteConnection _db;
        
        private readonly IUserServices _userServices;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            try
            {

            bool result = _userServices.SaveUser(user);
            return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetOne method for user with ID {user}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {

            IEnumerable<UserModel>? users = _userServices.GetUsers();

            if (users == null) return NotFound();


            return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUsers method");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("GetOne")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {

            var user = _userServices.GetUser(id);

            if (user == null) return NotFound();


            return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetOne method with userId:{id} ");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(int id, UserModel user)
        {
            try
            {

            var userEdited = _userServices.EditUser(user, id);

            if (!userEdited) return NotFound();


            return Ok(userEdited);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in EditUser method with user: {user} ");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {

            var userEdited = _userServices.DeleteUser(id);

            if (!userEdited) return NotFound();


            return Ok(userEdited);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteUser method with userId: {id}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
