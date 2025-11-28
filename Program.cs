using System.Text;
using final.Data;
using final.Repositories;
using final.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. Cargar variables de entorno (.env)
// ============================================
try
{
    // Si tienes DotNetEnv instalado, esto carga el .env en local.
    DotNetEnv.Env.Load();
}
catch
{
    // Si no existe el .env o no está el paquete, no rompemos la app.
}

// ============================================
// 2. CONFIGURACIÓN DE BASE DE DATOS (PostgreSQL)
// ============================================

// Variables de entorno para Postgres (local o Railway)
var pgUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
var pgPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";
var pgHost = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
var pgDb = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "carcel_db";
var pgPort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";

// Connection string construida desde env vars
var connectionString =
    $"Host={pgHost};Port={pgPort};Database={pgDb};Username={pgUser};Password={pgPassword}";

// Si quieres, aquí puedes forzar SSL solo en producción:
// var isProduction = builder.Environment.IsProduction();
// if (isProduction)
// {
//     connectionString += ";SSL Mode=Require;Trust Server Certificate=true";
// }

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ============================================
// 3. CONFIGURACIÓN DE JWT (Autenticación)
// ============================================

// Primero intentamos leer desde env vars (Railway / .env)
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
             ?? builder.Configuration["Jwt:Key"]
             ?? throw new InvalidOperationException("JWT_KEY no configurado.");

var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                ?? builder.Configuration["Jwt:Issuer"];   // puede ser null

var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                  ?? builder.Configuration["Jwt:Audience"]; // puede ser null

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // en Railway va con https igual, en local puede ser http
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
            ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// ============================================
// 4. INYECCIÓN DE DEPENDENCIAS (Services/Repositories)
// ============================================

// Repositories
builder.Services.AddScoped<IGuardiaRepository, GuardiaRepository>();
builder.Services.AddScoped<IReclusoRepository, ReclusoRepository>();
builder.Services.AddScoped<ICeldaRepository, CeldaRepository>();
builder.Services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Services
builder.Services.AddScoped<IGuardiaService, GuardiaService>();
builder.Services.AddScoped<IReclusoService, ReclusoService>();
builder.Services.AddScoped<ICeldaService, CeldaService>();
builder.Services.AddScoped<IExpedienteService, ExpedienteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// ============================================
// 5. CONTROLLERS + SWAGGER
// ============================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================
// 6. APLICAR MIGRACIONES AUTOMÁTICAMENTE
// ============================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error aplicando migraciones: {ex.Message}");
        // Puedes loguearlo, pero no detenemos la app a menos que quieras.
    }
}

// ============================================
// 7. PIPELINE HTTP
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Si quieres que Swagger también funcione en producción (Railway),
    // puedes descomentar esto:
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
