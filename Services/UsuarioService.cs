using final.Models.DTOs;
using final.Models.Entities;
using final.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace final.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IConfiguration _config;

        public UsuarioService(IUsuarioRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<UsuarioDto> Registrar(CreateUsuarioDto dto)
        {
            var existe = await _repo.GetByCorreo(dto.Correo);
            if (existe != null) throw new Exception("El correo ya está registrado");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                CI = dto.CI,
                Correo = dto.Correo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol   // 👈 GUARDAMOS EL ROL (Admin / User)
            };

            await _repo.Add(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                CI = usuario.CI,
                Correo = usuario.Correo,
                // Si tu UsuarioDto tiene Rol, puedes devolverlo también:
                // Rol = usuario.Rol
            };
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var usuario = await _repo.GetByCorreo(dto.Correo);
            if (usuario == null) throw new Exception("Credenciales incorrectas");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                throw new Exception("Contraseña incorrecta");

            return GenerarJwt(usuario);
        }

        private string GenerarJwt(Usuario u)
        {
            var claims = new List<Claim>
            {
                new Claim("id", u.Id.ToString()),
                new Claim("correo", u.Correo),
                new Claim(ClaimTypes.Name, u.Nombre),
                new Claim(ClaimTypes.Role, u.Rol) // 👈 AQUÍ VA EL ROL EN EL TOKEN
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(4),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
