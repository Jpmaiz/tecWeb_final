using final.Models.DTOs;
using final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [ApiController]
    [Route("api/recluso")]
    [Authorize]
    public class ReclusoController : ControllerBase
    {
        private readonly ReclusoService _service;

        public ReclusoController(ReclusoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReclusoDto dto)
        {
            return Ok(await _service.Create(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateReclusoDto dto)
        {
            return Ok(await _service.Update(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(new { eliminado = await _service.Delete(id) });
        }
    }
}
