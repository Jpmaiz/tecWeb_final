TecWeb Final — Sistema de Gestión de Guardia y Usuarios
Instalación y configuración

Instalar paquetes de EF Core:

dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools


Crear la migración inicial y actualizar la base de datos:

dotnet ef migrations add InitSqlite
dotnet ef database update

Objetivo

Implementar un mini-sistema de gestión de guardias y usuarios, demostrando dominio de relaciones 1:N, N:M y 1:1 en EF Core, y aplicando arquitectura por capas (Controller → Service → Repository → DbContext).

Contexto del dominio

Usuario: puede tener uno o varios Roles (N:M).

Guardia: cada guardia pertenece a un Usuario y puede estar asignado a diferentes turnos o sectores (1:N).

Sector: puede tener varios guardias asignados (1:N).

Perfil: opcional 1:1 con Usuario (PK compartida).

Relaciones en AppDbContext

Usuario → Guardia: 1:N, FK requerida, eliminación en cascada.

Usuario ↔ Roles: N:M con tabla de unión UsuarioRol.

Sector → Guardia: 1:N, FK requerida.

Usuario ↔ Perfil: 1:1, PK compartida, opcional en Usuario.

Bonus: Validación de solapamiento de turnos en un sector al asignar guardias.

Capas de la aplicación

Controllers: delgados, llaman a Services.

Services: lógica de negocio y mapeos DTO.

Repositories: encapsulan acceso a datos.

DbContext: configuración de relaciones y migraciones.

Endpoints principales
Usuarios

POST /api/v1/usuarios → crear usuario.

GET /api/v1/usuarios/{id}/roles → obtener roles del usuario.

POST /api/v1/usuarios/{id}/perfil → crear perfil 1:1 opcional.

GET /api/v1/usuarios/{id}/perfil → ver perfil del usuario.

Roles

POST /api/v1/roles → crear rol.

POST /api/v1/usuarios/{id}/roles → asignar roles a usuario.

Guardias

POST /api/v1/guardias → crear guardia asignado a usuario y sector.

GET /api/v1/guardias/{id} → ver información del guardia.

GET /api/v1/sectores/{id}/guardias → listar guardias por sector.

Ejemplo de uso
Crear usuario
POST /api/v1/usuarios
{
  "nombre": "Juan Pérez",
  "email": "juan@example.com"
}

Crear rol
POST /api/v1/roles
{
  "nombre": "Administrador"
}

Asignar rol a usuario
POST /api/v1/usuarios/1/roles
{
  "roleId": 1
}

Crear perfil opcional
POST /api/v1/usuarios/1/perfil
{
  "documentId": "12345678",
  "birthDate": "1998-05-21"
}

Crear guardia
POST /api/v1/guardias
{
  "usuarioId": 1,
  "sectorId": 2,
  "turnoInicio": "2025-11-28T08:00:00",
  "turnoFin": "2025-11-28T16:00:00"
}

Listar guardias de un sector
GET /api/v1/sectores/2/guardias

Bonus — Validación de solapamiento de turnos
POST /api/v1/guardias
{
  "usuarioId": 2,
  "sectorId": 2,
  "turnoInicio": "2025-11-28T12:00:00",
  "turnoFin": "2025-11-28T20:00:00"
}


Respuesta esperada si hay solapamiento:

{ "error": "El sector ya tiene un guardia en este rango horario." }
