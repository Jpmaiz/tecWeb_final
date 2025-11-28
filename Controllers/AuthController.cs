using final.Models.DTOs;
using final.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace final.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public AuthController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUsuarioDto dto)
        {
            await _service.Registrar(dto);
            return Ok(new { message = "Usuario creado" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(new { token });
        }
    }
}
