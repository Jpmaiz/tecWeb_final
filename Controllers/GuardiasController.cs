using final.Models.DTOs;
using final.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GuardiasController : ControllerBase
    {
        private readonly IGuardiaService _service;

        public GuardiasController(IGuardiaService service)
        {
            _service = service;
        }

        // POST: api/v1/guardias
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGuardiaDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // GET: api/v1/guardias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        // GET: api/v1/guardias/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var guardia = await _service.GetByIdAsync(id);
            if (guardia == null) return NotFound();
            return Ok(guardia);
        }

        // PUT: api/v1/guardias/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGuardiaDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        // DELETE: api/v1/guardias/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
