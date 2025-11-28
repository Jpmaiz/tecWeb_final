using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.DTOs
{
    public class CreateReclusoDto
    {
        [Required] public string Nombre { get; set; } = null!;
        [Required] public string CI { get; set; } = null!;
        [Required] public string Delito { get; set; } = null!;
        [Required] public string Celda { get; set; } = null!;
    }

    public class UpdateReclusoDto
    {
        [Required] public string Nombre { get; set; } = null!;
        [Required] public string CI { get; set; } = null!;
        [Required] public string Delito { get; set; } = null!;
        [Required] public string Celda { get; set; } = null!;
    }

    public class ReclusoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CI { get; set; } = null!;
        public string Delito { get; set; } = null!;
        public string Celda { get; set; } = null!;
    }
}
