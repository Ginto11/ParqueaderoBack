using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parqueadero_Back.Database;
using Parqueadero_Back.Services;
using Parqueadero_Back.Servicios;
using Parqueadero_Back.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
{
    opciones.RequireHttpsMetadata = false;
    opciones.SaveToken = true;
    opciones.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddScoped<CupoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<Utilidad>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<SoporteService>();
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("DesarrolloAngular", app =>
    {
        app.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DesarrolloAngular");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
