using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using final.Models;
using final.Models.DTOs;
using final.Services;

namespace final.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CeldaController : ControllerBase
    {
        private readonly ICeldaService _service;
        public CeldaController(ICeldaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCeldas()
        {
            IEnumerable<Celda> items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var celda = await _service.GetOne(id);
            return Ok(celda);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateCelda([FromBody] CreateCeldaDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var celda = await _service.CreateCelda(dto);
            return CreatedAtAction(nameof(GetOne), new { id = celda.Id }, celda);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCelda([FromBody] UpdateCeldaDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var celda = await _service.UpdateCelda(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = celda.Id }, celda);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCelda(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteCelda(id);
            return NoContent();
        }
    }
}