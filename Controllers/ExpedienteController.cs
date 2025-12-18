using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using final.Models.Entities;
using final.Models.DTOs;
using final.Services;

namespace final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteService _service;
        public ExpedienteController(IExpedienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllExpedientes()
        {
            IEnumerable<Expediente> items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var expediente = await _service.GetOne(id);
            return Ok(expediente);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateExpediente([FromBody] CreateExpedienteDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var expediente = await _service.CreateExpediente(dto);
            return CreatedAtAction(nameof(GetOne), new { id = expediente.Id }, expediente);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateExpediente([FromBody] UpdateExpedienteDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var expediente = await _service.UpdateExpediente(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = expediente.Id }, expediente);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteExpediente(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteExpediente(id);
            return NoContent();
        }
    }
}