using Microsoft.AspNetCore.Mvc;
using ProjectLibraryDomain.IService;
using ProjectLibraryDomain.Models.DTO;

namespace ProjectLibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;

        }
        /// <summary>
        /// Login
        /// </summary>
        /// <response code="200">Generate Token</response>
        /// <response code="404">Invalid login</response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var token = await _loginService.Login(loginDTO);

            return Ok(token);
        }
    }
}
