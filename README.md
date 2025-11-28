"""# 🏛️ Sistema de Gestión Penitenciaria - API REST

## 📌 1. Presentación del Proyecto

Este proyecto consiste en el desarrollo de una **API REST en ASP.NET Core (.NET 8)** para la gestión administrativa de un centro penitenciario, permitiendo el control digital y centralizado de:

- Reclusos
- Guardias
- Celdas
- Expedientes
- Usuarios del sistema

El sistema resuelve el problema de la desorganización de datos en entornos penitenciarios, donde la información suele estar dispersa o gestionada manualmente.

La API es:
- Segura (JWT)
- Escalable
- Basada en arquitectura por capas
- Preparada para despliegue con Docker y Railway

Cumple con los requisitos de la materia Tecnología Web: arquitectura por capas, EF Core, JWT, relaciones, Swagger y despliegue.

---

## 🏗️ 2. Estructura del Proyecto

Estructura general:

/Controllers  
/Data  
/Models  
/Models/Entities  
/Models/DTOs  
/Repositories  
/Services  
/Migrations  

Program.cs  
appsettings.json  

---

## 🧩 3. Arquitectura por Capas

El proyecto utiliza **Arquitectura por Capas** con patrón **Repository + Service**.

### Controllers
Manejan solicitudes HTTP y respuestas.

Ejemplos:
- AuthController
- GuardiasController
- ReclusoController
- CeldaController
- ExpedienteController
- UsuarioController

---

### Services
Contienen la lógica de negocio del sistema.

Ejemplos:
- GuardiaService
- ReclusoService
- UsuarioService
- CeldaService
- ExpedienteService

---

### Repositories
Acceso a datos con Entity Framework Core.

Ejemplos:
- IGuardiaRepository / GuardiaRepository
- IUsuarioRepository / UsuarioRepository
- IReclusoRepository / ReclusoRepository

---

### Data / AppDbContext
Encargado de gestionar las entidades, relaciones y conexión con PostgreSQL.

---

## 🧬 4. Entidades del Sistema

### Usuario
Atributos:
- Id
- Nombre
- Correo
- PasswordHash
- Rol

### Guardia
- Id
- Nombre
- CI
- Turno
- Rango

### Celda
- Id
- Numero
- Pabellon
- Capacidad

### Recluso
- Id
- Nombre
- CI
- FechaIngreso
- CondenaAnios

### Expediente
- Id
- Codigo
- DelitoPrincipal
- FechaRegistro

---

## 🔐 5. Autenticación, Autorización y JWT

La API implementa autenticación basada en **JSON Web Tokens (JWT)** utilizando `[Authorize]` para proteger rutas.

---

## 🧩 Endpoints de Autenticación

### Registro de Usuario

POST /auth/register

Body de ejemplo:

{
  "nombre": "Juan Pablo",
  "ci": "1234567",
  "correo": "juan@test.com",
  "password": "123456"
}

---

### Login

POST /auth/login

Body de ejemplo:

{
  "correo": "juan@test.com",
  "password": "123456"
}

Respuesta:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

---

## 🔐 Funcionamiento del JWT

El token JWT contiene:
- Id del usuario
- Correo
- Nombre
- Rol (Admin / User)

Configurado en:

"Jwt": {
  "Key": "CLAVE_SUPER_SECRETA",
  "Issuer": "PrisonApi",
  "Audience": "PrisonClient"
}

---

## 🔒 Protección de rutas

Se usa:

[Authorize]

Ejemplo:

[ApiController]  
[Route("api/recluso")]  
[Authorize]  
public class ReclusoController : ControllerBase  
{
    // Todas las rutas requieren JWT
}

---

## ✅ Autorización por Roles

Roles soportados:
- Admin
- User

Implementado con:

new Claim(ClaimTypes.Role, u.Rol);

Ejemplo de protección:

[Authorize(Roles = "Admin")]

---

## 🔑 Uso del Token en Swagger

1. Ejecutar POST /auth/login  
2. Copiar token  
3. Acceder a http://localhost:5114/swagger  
4. Presionar botón Authorize  
5. Pegar: Bearer TU_TOKEN_AQUI  

---

## 🔑 Uso del Token en Postman

En la pestaña Authorization:

- Tipo: Bearer Token
- Token: TU_TOKEN_AQUI

O en Headers:

Authorization: Bearer TU_TOKEN_AQUI


## 📋 Endpoints de la API

### 🔐 Autenticación

| Método | Endpoint | Permiso | Descripción | Body |
|--------|----------|----------|-------------|------|
| `POST` | `/auth/register` | Público | Registrar usuario | `{"nombre":"string","correo":"string","password":"string"}` |
| `POST` | `/auth/login` | Público | Iniciar sesión | `{"correo":"string","password":"string"}` |

### 🏢 Celdas

| Método | Endpoint | Permiso | Descripción | Body |
|--------|----------|----------|-------------|------|
| `GET` | `/api/celda` | Auth | Todas las celdas | N/A |
| `GET` | `/api/celda/{id}` | Auth | Celda específica | N/A |
| `POST` | `/api/celda` | Admin | Crear celda | `{"numero":"string","capacidad":int,"tipo":"string"}` |
| `PUT` | `/api/celda/{id}` | Admin | Actualizar celda | `{"numero":"string","capacidad":int,"tipo":"string"}` |
| `DELETE` | `/api/celda/{id}` | Admin | Eliminar celda | N/A |

### 📁 Expedientes

| Método | Endpoint | Permiso | Descripción | Body |
|--------|----------|----------|-------------|------|
| `GET` | `/api/expediente` | Auth | Todos los expedientes | N/A |
| `GET` | `/api/expediente/{id}` | Auth | Expediente específico | N/A |
| `POST` | `/api/expediente` | Admin | Crear expediente | `{"codigo":"string","delitoPrincipal":"string","fechaRegistro":"datetime","reclusoId":"guid"}` |
| `PUT` | `/api/expediente/{id}` | Admin | Actualizar expediente | `{"codigo":"string","delitoPrincipal":"string","fechaRegistro":"datetime"}` |
| `DELETE` | `/api/expediente/{id}` | Admin | Eliminar expediente | N/A |

### 👮 Guardias

| Método | Endpoint | Permiso | Descripción | Body |
|--------|----------|----------|-------------|------|
| `GET` | `/api/v1/guardias` | Auth* | Todos los guardias | N/A |
| `GET` | `/api/v1/guardias/{id}` | Auth* | Guardia específico | N/A |
| `POST` | `/api/v1/guardias` | Auth* | Crear guardia | `{"nombre":"string","turno":"string","rango":"string"}` |
| `PUT` | `/api/v1/guardias/{id}` | Auth* | Actualizar guardia | `{"nombre":"string","turno":"string","rango":"string"}` |
| `DELETE` | `/api/v1/guardias/{id}` | Auth* | Eliminar guardia | N/A |

### 👤 Reclusos

| Método | Endpoint | Permiso | Descripción | Body |
|--------|----------|----------|-------------|------|
| `GET` | `/api/recluso` | Auth | Todos los reclusos | N/A |
| `POST` | `/api/recluso` | Auth | Crear recluso | `{"nombre":"string","identificacion":"string","fechaNacimiento":"datetime","genero":"string"}` |
| `PUT` | `/api/recluso/{id}` | Auth | Actualizar recluso | `{"nombre":"string","identificacion":"string","fechaNacimiento":"datetime","genero":"string"}` |
| `DELETE` | `/api/recluso/{id}` | Auth | Eliminar recluso | N/A |

## 🔐 Autenticación JWT

### Configuración
```json
"Jwt": {
  "Key": "clave-secreta-super-segura",
  "Issuer": "PrisonAPI",
  "Audience": "PrisonClient"
}

# 🚀 Deploy en Railway
Sistema de Gestión Penitenciaria – API REST

## 📌 1. Descripción del Deploy

Este proyecto fue desplegado en la plataforma **Railway** utilizando contenedores para ejecutar la API desarrollada en **ASP.NET Core** junto a una base de datos **PostgreSQL**.

La aplicación está disponible públicamente y se conecta a PostgreSQL mediante variables de entorno, siguiendo buenas prácticas para entornos de producción.

---

## 🌐 2. URL del Proyecto en Producción

**API Base:**
https://tecwebfinal-production.up.railway.app

**Swagger (Documentación):**
https://tecwebfinal-production.up.railway.app/swagger

Desde Swagger se pueden probar todos los endpoints del proyecto.

---

## 🛠 3. Servicios utilizados en Railway

En Railway se configuraron dos servicios:

### 📦 Servicio Backend
- Nombre: `tecWeb_final`
- Contiene la API ASP.NET Core.

### 🗄 Servicio Base de Datos
- Motor: PostgreSQL
- Proporcionado por Railway.

Ambos servicios están conectados mediante variables de entorno.

---

## 🔐 4. Variables de Entorno Configuradas

En Railway se configuraron las siguientes variables:

```env
DATABASE_URL=postgresql://usuario:password@postgres.railway.internal:5432/railway
JWT_KEY=TuClaveSecretaJWT
ASPNETCORE_ENVIRONMENT=Production

Importante:

DATABASE_URL la genera Railway automáticamente.

JWT_KEY es tu clave secreta utilizada para firmar los tokens JWT.

ASPNETCORE_ENVIRONMENT se configura en Production.

🧠 5. Lógica de Conexión en Program.cs
El proyecto detecta automáticamente si está en Railway o en local:

✅ Si existe DATABASE_URL → Se conecta a Railway ✅ Si no existe → Se conecta a PostgreSQL local

Se convierte la URL a formato NpgsqlConnectionString y se fuerza:

C#

SslMode = Require
TrustServerCertificate = true
Esto asegura una conexión segura en producción.

🗃 6. Migraciones Automáticas
La API ejecuta automáticamente las migraciones al iniciarse:

C#

db.Database.Migrate();
Esto permite que las tablas se creen automáticamente cuando la aplicación se inicia por primera vez en Railway.

En los logs se puede ver algo como:

Bash

DATABASE_URL detectada...
Aplicando migraciones de base de datos...
Migraciones aplicadas correctamente.
Now listening on: [http://0.0.0.0:8080](http://0.0.0.0:8080)
✅ 7. Verificación del Deploy
Para comprobar que todo funciona:

Abrir Swagger: https://tecwebfinal-production.up.railway.app/swagger

Hacer login: POST /auth/login Copia el token JWT.

Presionar el botón 🔒 Authorize en Swagger.

Pegar: Bearer TU_TOKEN

Probar los endpoints:

GET /api/guardia

GET /api/recluso

GET /api/celda

GET /api/expediente

Si todos responden: ✅ el deploy es exitoso.

🔄 8. Flujo del Deploy
Subir el proyecto a GitHub.

Crear proyecto en Railway.

Conectar el repositorio de GitHub.

Agregar servicio PostgreSQL.

Configurar variables de entorno.

Adaptar Program.cs para leer DATABASE_URL.

Activar migraciones automáticas.

Verificar logs en Railway.

Probar endpoints con Swagger.