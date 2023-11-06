using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectChallengeDomain.IService;
using ProjectChallengeDomain.Models.Requests;
using ProjectLibraryDomain.IService;
using ProjectLibraryDomain.Models;
using ProjectLibraryDomain.Models.Requests;
using System.Security.Claims;

namespace ProjectLibraryAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUSerService _serviceUser;
        public UserController(IUSerService serviceUser)
        {
            _serviceUser = serviceUser;

        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <response code="200">List of all users</response>
        /// <response code="404">No users found</response>
        /// <response code="500">Error to get all users</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _serviceUser.Get();

            if (users == null || users.Count() == 0)
                return NotFound(new { message = "Users not found." });
            return Ok(users);
        }
        /// <summary>
        /// Get a user by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <response code="200">The user from the id</response>
        /// <response code="404">No user found</response>
        /// <response code="500">Error to get the user</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _serviceUser.GetById(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="User">User data</param>
        /// <returns>User successfully created</returns>
        /// <response code="200">true</response>
        /// <response code="400">false</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public IActionResult CreateUser(UserRequestPost user)
        {
            ClaimsIdentityExtensions.ValidaPerfil((ClaimsIdentity)HttpContext.User.Identity, ClaimsIdentityExtensions.AdminRole());

            var userMethod = ClaimsIdentityExtensions.GetUsuario((ClaimsIdentity)HttpContext.User.Identity);

            var response = _serviceUser.Post(user, userMethod);
            return Ok(response);
        }
        /// <summary>
        /// Update a user
        /// </summary> 
        /// <param name="id">User id to be updated</param>
        /// <param name="User">User data</param>
        /// <response code="200">true</response>
        /// <response code="400">false</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Error to update user</response>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserRequestPut user)
        {
            ClaimsIdentityExtensions.ValidaPerfil((ClaimsIdentity)HttpContext.User.Identity, ClaimsIdentityExtensions.AdminRole());

            var userMethod = ClaimsIdentityExtensions.GetUsuario((ClaimsIdentity)HttpContext.User.Identity);

            var response = await _serviceUser.Put(user, userMethod);

            if (response == null)
                return NotFound(new { message = "User not found." });

            return Ok(response);
        }
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <response code="200">User successfully deleted</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Error to delete user</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            ClaimsIdentityExtensions.ValidaPerfil((ClaimsIdentity)HttpContext.User.Identity, ClaimsIdentityExtensions.AdminRole());

            var userMethod = ClaimsIdentityExtensions.GetUsuario((ClaimsIdentity)HttpContext.User.Identity);

            var response = await _serviceUser.Delete(id);
            if (!response)
                return NotFound(new { message = "User not found." });
            return Ok(true);
        }
    }
}
