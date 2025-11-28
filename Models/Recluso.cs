using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.Entities
{
    public class Recluso
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string CI { get; set; } = string.Empty;

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public int CondenaAnios { get; set; }

        // Qué usuario registró al preso
        public Guid UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
