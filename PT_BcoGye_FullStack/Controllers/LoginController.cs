using BE.Domain.Dto;
using BE.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = await _authService.ValidateLogin(login);
            if (response == null) return Unauthorized(new { Message = "Credenciales inválidas" });
            return Ok(response);
        }

    }
}
