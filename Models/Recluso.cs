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

        // 1:N Usuario -> Reclusos
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // 1:N Celda -> Reclusos
        public Guid CeldaId { get; set; }
        public Celda Celda { get; set; } = null!;

        // 1:1 Recluso -> Expediente
        public Expediente? Expediente { get; set; }

        // N:M con Guardia
        public ICollection<Guardia> Guardias { get; set; } = new List<Guardia>();
    }
}
