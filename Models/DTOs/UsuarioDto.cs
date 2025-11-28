using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models.DTOs
{
    public class CreateUsuarioDto
    {
        [Required] public string Nombre { get; set; } = null!;
        [Required] public string CI { get; set; } = null!;
        [Required][EmailAddress] public string Correo { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;

        public string Rol { get; set; } = "User"; 
    }

    public class LoginUsuarioDto
    {
        [Required][EmailAddress] public string Correo { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }

    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CI { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}
