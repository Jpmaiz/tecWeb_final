using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class Guardia
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string CI { get; set; } = null!;

        [Required]
        public string Turno { get; set; } = null!;

        [Required]
        public string Rango { get; set; } = null!;
    }
}
