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
            bool result = _userServices.SaveUser(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserModel>? users = _userServices.GetUsers();

            if (users == null) return NotFound();


            return Ok(users);

        }

        [HttpGet("GetOne")]
        public async Task<IActionResult> GetOne(int id)
        {
            var user = _userServices.GetUser(id);

            if (user == null) return NotFound();


            return Ok(user);

        }

        [HttpPut]
        public async Task<IActionResult> EditUser(int id, UserModel user)
        {
            var userEdited = _userServices.EditUser(user, id);

            if (!userEdited) return NotFound();


            return Ok(userEdited);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userEdited = _userServices.DeleteUser(id);

            if (!userEdited) return NotFound();


            return Ok(userEdited);

        }

    }
}
