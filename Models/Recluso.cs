using System;
using System.ComponentModel.DataAnnotations;
namespace PrisonApi.Models
{
    public class Recluso
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string CI { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set; }
        [Required]
        public int CondenaAnios { get; set; }

        // Que usuario registro al preso
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
