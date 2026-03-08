using AutoMapper;
using PracticaProgramada2BLL.Dtos;
using PracticaProgramada2BLL.Servicios.Categoria;
using PracticaProgramada2DAL.Repositorios.Generico;

namespace PracticaProgramada2BLL.Servicios.Producto;

public class ProductoServicio : IProductoServicio
{
    private readonly IMapper _mapper;
    private readonly IRepositorioGenerico<PracticaProgramada2DAL.Entidades.Producto> _repositorioProducto;
    private readonly ICategoriaServicio _categoriaServicio;

    public ProductoServicio(
        IMapper mapper,
        IRepositorioGenerico<PracticaProgramada2DAL.Entidades.Producto> repositorioProducto,
        ICategoriaServicio categoriaServicio)
    {
        _mapper = mapper;
        _repositorioProducto = repositorioProducto;
        _categoriaServicio = categoriaServicio;
    }

    public async Task<CustomResponse<List<ProductoDto>>> ObtenerProductosAsync()
    {
        var response = new CustomResponse<List<ProductoDto>>();
        var productos = await _repositorioProducto.ObtenerTodosAsync();
        var categoriasResponse = await _categoriaServicio.ObtenerCategoriasAsync();
        var categorias = categoriasResponse.Data ?? new List<CategoriaDto>();
        var dtos = _mapper.Map<List<ProductoDto>>(productos);
        foreach (var dto in dtos)
        {
            var cat = categorias.FirstOrDefault(c => c.Id == dto.CategoriaId);
            if (cat != null)
                dto.NombreCategoria = cat.Nombre;
        }
        response.Data = dtos;
        return response;
    }

    public async Task<CustomResponse<ProductoDto>> ObtenerProductoPorIdAsync(int id)
    {
        var response = new CustomResponse<ProductoDto>();
        var producto = await _repositorioProducto.ObtenerPorIdAsync(id);

        if (producto is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.NotFound;
            response.codigoStatus = 404;
            return response;
        }

        response.Data = _mapper.Map<ProductoDto>(producto);
        var categoriasResponse = await _categoriaServicio.ObtenerCategoriasAsync();
        var cat = categoriasResponse.Data?.FirstOrDefault(c => c.Id == response.Data!.CategoriaId);
        if (cat != null)
            response.Data.NombreCategoria = cat.Nombre;
        return response;
    }

    public async Task<CustomResponse<List<CategoriaDto>>> ObtenerCategoriasAsync()
    {
        return await _categoriaServicio.ObtenerCategoriasAsync();
    }

    public async Task<CustomResponse<ProductoDto>> AgregarProductoAsync(ProductoDto productoDto)
    {
        var response = new CustomResponse<ProductoDto>();

        if (productoDto is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.Null;
            response.codigoStatus = 400;
            return response;
        }

        var productoGuardar = _mapper.Map<PracticaProgramada2DAL.Entidades.Producto>(productoDto);
        _repositorioProducto.AgregarAsync(productoGuardar);

        if (!await _repositorioProducto.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.ErrorGuardar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }

    public async Task<CustomResponse<ProductoDto>> ActualizarProductoAsync(ProductoDto productoDto)
    {
        var response = new CustomResponse<ProductoDto>();

        if (productoDto is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.Null;
            response.codigoStatus = 400;
            return response;
        }

        var productoActualiza = _mapper.Map<PracticaProgramada2DAL.Entidades.Producto>(productoDto);
        _repositorioProducto.ActualizarAsync(productoActualiza);

        if (!await _repositorioProducto.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.ErrorActualizar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }

    public async Task<CustomResponse<ProductoDto>> EliminarProductoAsync(int id)
    {
        var response = new CustomResponse<ProductoDto>();

        if (id == 0)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.InvalidId;
            response.codigoStatus = 400;
            return response;
        }

        _repositorioProducto.EliminarAsync(id);

        if (!await _repositorioProducto.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Producto.ErrorEliminar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }
}
