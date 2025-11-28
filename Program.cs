using System.Text;
using final.Data;
using final.Repositories;
using final.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. Cargar .env SOLO en desarrollo
// ============================================
try
{
    if (builder.Environment.IsDevelopment())
    {
        DotNetEnv.Env.Load();
        Console.WriteLine("Archivo .env cargado (Development).");
    }
}
catch
{
    Console.WriteLine(".env no encontrado o DotNetEnv no instalado. Continuando sin .env.");
}

// ============================================
// 2. CONFIGURACIÓN DE BASE DE DATOS
//    - Railway: usa DATABASE_URL
//    - Local: usa appsettings o valores por defecto
// ============================================

string connectionString;

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    // MODO RAILWAY - DATABASE_URL = postgresql://user:pass@host:port/db
    Console.WriteLine($"DATABASE_URL detectada: {databaseUrl}");

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Database = uri.LocalPath.TrimStart('/'),
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        SslMode = SslMode.Require,
        TrustServerCertificate = true
    };

    connectionString = npgsqlBuilder.ToString();

    Console.WriteLine(
        $"Usando conexión Railway: Host={npgsqlBuilder.Host};Port={npgsqlBuilder.Port};Database={npgsqlBuilder.Database};Username={npgsqlBuilder.Username};Password=******"
    );
}
else
{
    // MODO LOCAL
    Console.WriteLine("DATABASE_URL no encontrada. Usando conexión local.");

    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Host=localhost;Port=5432;Database=carcel_db;Username=postgres;Password=postgres";

    Console.WriteLine("Usando conexión local: " + connectionString);
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ============================================
// 3. CONFIGURACIÓN DE JWT
// ============================================

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
             ?? builder.Configuration["Jwt:Key"]
             ?? throw new InvalidOperationException("JWT_KEY no configurado.");

var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                ?? builder.Configuration["Jwt:Issuer"];

var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                  ?? builder.Configuration["Jwt:Audience"];

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
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
// 4. INYECCIÓN DE DEPENDENCIAS
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
        Console.WriteLine("Aplicando migraciones de base de datos...");
        db.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error aplicando migraciones: {ex.Message}");
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");


app.Run();
