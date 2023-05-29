﻿using FluentValidation;
using MediatR;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _context;

            public Manejador(ContextoAutor contexto)    //instanciar contexto entityframework
            {
                _context = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro         //intacia clase autorlibro
                {
                    Nombre= request.Nombre,
                    Apellido= request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString(),
                };
                _context.AutorLibros.Add(autorLibro);     //agregar al contexto
                var valor = await _context.SaveChangesAsync();              //guardar los cambios

                if(valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el autor");
            }
        }
    }
}
