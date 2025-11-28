using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string CI { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Rol { get; set; } = "User";
    }
}
