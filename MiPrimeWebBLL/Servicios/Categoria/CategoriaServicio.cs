using AutoMapper;
using PracticaProgramada2BLL.Dtos;
using PracticaProgramada2DAL.Repositorios.Generico;

namespace PracticaProgramada2BLL.Servicios.Categoria;

public class CategoriaServicio : ICategoriaServicio
{
    private readonly IMapper _mapper;
    private readonly IRepositorioGenerico<PracticaProgramada2DAL.Entidades.Categoria> _repositorioGenerico;

    public CategoriaServicio(
        IMapper mapper,
        IRepositorioGenerico<PracticaProgramada2DAL.Entidades.Categoria> repositorioGenerico)
    {
        _mapper = mapper;
        _repositorioGenerico = repositorioGenerico;
    }

    public async Task<CustomResponse<List<CategoriaDto>>> ObtenerCategoriasAsync()
    {
        var response = new CustomResponse<List<CategoriaDto>>();
        response.Data = _mapper.Map<List<CategoriaDto>>(await _repositorioGenerico.ObtenerTodosAsync());
        return response;
    }

    public async Task<CustomResponse<CategoriaDto>> ObtenerCategoriaPorIdAsync(int id)
    {
        var response = new CustomResponse<CategoriaDto>();
        var categoria = await _repositorioGenerico.ObtenerPorIdAsync(id);

        if (categoria is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.NotFound;
            response.codigoStatus = 404;
            return response;
        }

        response.Data = _mapper.Map<CategoriaDto>(categoria);
        return response;
    }

    public async Task<CustomResponse<CategoriaDto>> AgregarCategoriaAsync(CategoriaDto categoriaDto)
    {
        var response = new CustomResponse<CategoriaDto>();

        if (categoriaDto is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.Null;
            response.codigoStatus = 400;
            return response;
        }

        var categoriaGuardar = _mapper.Map<PracticaProgramada2DAL.Entidades.Categoria>(categoriaDto);
        _repositorioGenerico.AgregarAsync(categoriaGuardar);

        if (!await _repositorioGenerico.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.ErrorGuardar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }

    public async Task<CustomResponse<CategoriaDto>> ActualizarCategoriaAsync(CategoriaDto categoriaDto)
    {
        var response = new CustomResponse<CategoriaDto>();

        if (categoriaDto is null)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.Null;
            response.codigoStatus = 400;
            return response;
        }

        var categoriaActualiza = _mapper.Map<PracticaProgramada2DAL.Entidades.Categoria>(categoriaDto);
        _repositorioGenerico.ActualizarAsync(categoriaActualiza);

        if (!await _repositorioGenerico.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.ErrorActualizar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }

    public async Task<CustomResponse<CategoriaDto>> EliminarCategoriaAsync(int id)
    {
        var response = new CustomResponse<CategoriaDto>();

        if (id == 0)
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.InvalidId;
            response.codigoStatus = 400;
            return response;
        }

        _repositorioGenerico.EliminarAsync(id);

        if (!await _repositorioGenerico.GuardarCambiosAsync())
        {
            response.esCorrecto = false;
            response.mensaje = Constantes.Categoria.ErrorEliminar;
            response.codigoStatus = 500;
            return response;
        }

        return response;
    }
}
