using final.Data;
using final.Repositories;
using final.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext: PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

<<<<<<< HEAD
// Dependency Injection: Guardia
builder.Services.AddScoped<IGuardiaRepository, GuardiaRepository>();
builder.Services.AddScoped<IGuardiaService, GuardiaService>();

// Controllers + OpenAPI (.NET 9 style)
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
=======
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddAuthorization();


>>>>>>> e09d72a092387dfc00b6af8faedb5030a6ffdd85
builder.Services.AddOpenApi();

var app = builder.Build();

// OpenAPI document
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Luego agregaremos UseAuthentication() cuando metamos JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
