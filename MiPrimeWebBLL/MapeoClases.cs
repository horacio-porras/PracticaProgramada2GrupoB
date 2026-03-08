using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticaProgramada2BLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<PracticaProgramada2DAL.Entidades.Categoria, PracticaProgramada2BLL.Dtos.CategoriaDto>().ReverseMap();
            CreateMap<PracticaProgramada2DAL.Entidades.Producto, PracticaProgramada2BLL.Dtos.ProductoDto>()
                .ForMember(d => d.NombreCategoria, o => o.Ignore())
                .ReverseMap()
                .ForMember(d => d.Categoria, o => o.Ignore()); // Para poder cargar los nombres de la categorías al agregar un producto
        }
    }
}
