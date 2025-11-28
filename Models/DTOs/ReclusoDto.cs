using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.DTOs
{
    // DTO para crear
    public class CreateReclusoDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string CI { get; set; } = string.Empty;

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public int CondenaAnios { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }
    }

    // DTO para actualizar
    public class UpdateReclusoDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string CI { get; set; } = string.Empty;

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public int CondenaAnios { get; set; }
    }

    // DTO para mostrar
    public class ReclusoDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string CI { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }

        public int CondenaAnios { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
