using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Autor.Aplicacion;
using TiendaServicio.Api.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddDbContext<ContextoAutor>(options =>
{
    //options.UseNpgsql(Environment.GetEnvironmentVariable("ConexionDatabase"));
    options.UseNpgsql(builder.Configuration.GetConnectionString("conexionBasedatos"));
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
