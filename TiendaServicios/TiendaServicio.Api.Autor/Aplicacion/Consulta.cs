﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>>{
        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            public readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto= contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibros.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                return autoresDto;
                
            }
        }
    }
}
