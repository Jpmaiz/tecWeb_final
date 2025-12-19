using final.Models.DTOs;
using final.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)   
        {
            _service = service;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _service.Registrar(dto);
            return Ok(usuario);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(new { token });
        }
    }
}
