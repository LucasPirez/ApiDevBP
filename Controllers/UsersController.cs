
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        
        private readonly IUserServices _userServices;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="userServices">Servicio de usuarios.</param>
        /// <param name="logger">Logger para la clase UsersController.</param>
        public UsersController(ILogger<UsersController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        /// <summary>
        ///  Guardar nuevo usuario.
        /// </summary>

        /// <response code="200">El usuario se guarda correctamente</response>
        /// <response code="500">Error interno del servidor.</response>
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
        /// <summary>
        /// Obtiene lista de todos los usuarios.
        /// </summary>
        /// <returns>Detalles de los usuarios. </returns>
        /// <response code="200">Los usuarios se obtuvieron correctamente</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {

            var users = _userServices.GetUsers();

            if (users == null) return NotFound();


            return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUsers method");
                return StatusCode(500, "Internal Server Error");
            }

        }
        /// <summary>
        /// Obtiene un usuario por ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <response code="200">El usuario se obtuvo correctamente</response>
        /// <response code="404">No se encuentra el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
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

        /// <summary>
        /// Edita un usuario por ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <response code="200">El usuario se edito correctamente</response>
        /// <response code="404">No se encuentra el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
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

        /// <summary>
        /// Borra un usuario por ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <response code="200">El usuario se elimino correctamente</response>
        /// <response code="404">No se encuentra el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {

            var userEdited =  _userServices.DeleteUser(id);

            if (!userEdited) return  NotFound();


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
