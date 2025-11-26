using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.DTOs
{
    public class CreateGuardiaDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string CI { get; set; } = null!;

        [Required]
        public string Turno { get; set; } = null!;

        [Required]
        public string Rango { get; set; } = null!;
    }

    public class UpdateGuardiaDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string CI { get; set; } = null!;

        [Required]
        public string Turno { get; set; } = null!;

        [Required]
        public string Rango { get; set; } = null!;
    }

    public class GuardiaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CI { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public string Rango { get; set; } = null!;
    }
}
