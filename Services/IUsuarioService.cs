using final.Models.DTOs;
using System;

namespace final.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> Registrar(CreateUsuarioDto dto);
        Task<string> Login(LoginUsuarioDto dto);
    }
}