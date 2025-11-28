# 🏛️ Sistema de Gestión Penitenciaria — API REST

## 📌 1. Presentación del Proyecto

Este proyecto consiste en el desarrollo de una **API REST en ASP.NET Core (.NET 8)** para la gestión administrativa de un centro penitenciario, permitiendo el control digital y centralizado de:

- Reclusos  
- Guardias  
- Celdas  
- Expedientes  
- Usuarios del sistema  

El sistema resuelve el problema de la desorganización de datos en entornos penitenciarios, donde tradicionalmente la información se encuentra dispersa o gestionada de forma manual, generando inconsistencias y pérdida de información.

La API proporciona una solución:

✅ Segura (JWT)  
✅ Escalable  
✅ Basada en arquitectura por capas  
✅ Preparada para despliegue en entornos reales (Docker + Railway)  

Cumple con los requerimientos de la materia Tecnología Web: arquitectura por capas, Entity Framework Core, relaciones entre entidades, autenticación JWT, documentación Swagger y despliegue.

---

## 🏗️ 2. Estructura del Proyecto

La estructura sigue un diseño estándar profesional para APIs en ASP.NET Core:

/Controllers
/Data
/Models
/Entities
/DTOs
/Repositories
/Services
/Migrations

Program.cs
appsettings.json

yaml
Copiar código

### 📂 Descripción de Carpetas

| Carpeta | Descripción |
|--------|-------------|
Controllers | Manejo de las solicitudes HTTP.
Data | Contiene el `AppDbContext` y configuración de la base de datos.
Models/Entities | Representaciones de tablas en la base de datos.
Models/DTOs | Modelos usados para transferencia de datos.
Repositories | Acceso a datos usando Entity Framework Core.
Services | Lógica de negocio del sistema.
Migrations | Historial de cambios en la base de datos.
Program.cs | Configuración principal de la aplicación.
appsettings.json | Configuración de conexión, JWT y variables.

---

## 🧩 3. Arquitectura por Capas

El proyecto utiliza una **Arquitectura por Capas** junto al patrón **Repository + Service**:

### 🎯 Controllers
Encargados de recibir solicitudes HTTP, validar datos y retornar respuestas.

Ejemplo:
AuthController
GuardiasController
ReclusoController
CeldaController
ExpedienteController
UsuarioController

yaml
Copiar código

---

### ⚙️ Services
Contienen la lógica de negocio.

Ejemplo:
GuardiaService
ReclusoService
UsuarioService
CeldaService
ExpedienteService

yaml
Copiar código

Funciones:
- Validaciones
- Procesamiento de datos
- Generación de tokens JWT
- Lógica del sistema

---

### 📦 Repositories
Encargados del acceso directo a la base de datos mediante Entity Framework Core.

Ejemplo:
IGuardiaRepository / GuardiaRepository
IUsuarioRepository / UsuarioRepository
IReclusoRepository / ReclusoRepository

yaml
Copiar código

---

### 🗄️ Data / AppDbContext

El `AppDbContext` administra:

- Las entidades del sistema
- Sus relaciones
- La conexión con PostgreSQL

Se utiliza enfoque **Code First** con migraciones.

---

## 🧬 4. Entidades del Sistema

### 👤 Usuario
Representa a los usuarios que acceden al sistema.

Campos:
- Id  
- Nombre  
- Correo  
- PasswordHash  
- Rol  

---

### 👮 Guardia
Representa al personal penitenciario.

Campos:
- Id  
- Nombre  
- CI  
- Turno  
- Rango  

---

### 🏢 Celda
Representa las celdas donde se encuentran los reclusos.

Campos:
- Id  
- Numero  
- Pabellon  
- Capacidad  

---

### 🚷 Recluso
Representa a los privados de libertad.

Campos:
- Id  
- Nombre  
- CI  
- FechaIngreso  
- CondenaAnios  

---

### 📁 Expediente
Representa el historial legal del recluso.

Campos:
- Id  
- Codigo  
- DelitoPrincipal  
- FechaRegistro  

---

## 🔐 5. Autenticación, Autorización y JWT

La API implementa un sistema de **autenticación y autorización basado en JSON Web Tokens (JWT)**, garantizando que solo los usuarios autenticados puedan acceder a rutas protegidas.

Se utiliza el atributo `[Authorize]` de ASP.NET Core.

---

## 🧩 Endpoints de Autenticación

### 📌 Registro de usuario
POST /auth/register

css
Copiar código

Body de ejemplo:
```json
{
  "nombre": "Juan Pablo",
  "ci": "1234567",
  "correo": "juan@test.com",
  "password": "123456"
}
📌 Login
bash
Copiar código
POST /auth/login
Body de ejemplo:

json
Copiar código
{
  "correo": "juan@test.com",
  "password": "123456"
}
Respuesta:

json
Copiar código
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
🔐 Funcionamiento del JWT
El token JWT contiene:

Id del usuario

Correo

Nombre

Rol (Admin / User)

Configuración en appsettings.json:

json
Copiar código
"Jwt": {
  "Key": "CLAVE_SUPER_SECRETA",
  "Issuer": "PrisonApi",
  "Audience": "PrisonClient"
}
🔒 Protección de rutas
Las rutas protegidas utilizan:

csharp
Copiar código
[Authorize]
Ejemplo real:

csharp
Copiar código
[ApiController]
[Route("api/recluso")]
[Authorize]
public class ReclusoController : ControllerBase
{
    // Todas las rutas requieren JWT
}
✅ Autorización por Roles
Se utilizan los roles:

Admin

User

Incluidos en el JWT mediante:

csharp
Copiar código
new Claim(ClaimTypes.Role, u.Rol);
Puede restringirse acceso por rol:

csharp
Copiar código
[Authorize(Roles = "Admin")]
🔑 Uso del Token en Swagger
Ejecutar:

bash
Copiar código
POST /auth/login
Copiar el token generado.

En Swagger, hacer clic en 🔒 Authorize.

Pegar:

nginx
Copiar código
Bearer TU_TOKEN_AQUI
Ahora podrás probar todas las rutas protegidas.

🔑 Uso del Token en Postman
En Postman agregar:

makefile
Copiar código
Authorization: Bearer TU_TOKEN_AQUI
En la pestaña Authorization usando tipo: Bearer Token.

✅ Esta implementación cumple con:
Autenticación JWT

Autorización por roles

Protección con [Authorize]

Arquitectura por capas

Uso de EF Core

Buenas prácticas profesionales

yaml
Copiar código

---
