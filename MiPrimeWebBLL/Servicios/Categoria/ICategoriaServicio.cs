using PracticaProgramada2BLL.Dtos;

namespace PracticaProgramada2BLL.Servicios.Categoria;

public interface ICategoriaServicio
{
    Task<CustomResponse<List<CategoriaDto>>> ObtenerCategoriasAsync();

    Task<CustomResponse<CategoriaDto>> ObtenerCategoriaPorIdAsync(int id);

    Task<CustomResponse<CategoriaDto>> AgregarCategoriaAsync(CategoriaDto categoriaDto);

    Task<CustomResponse<CategoriaDto>> ActualizarCategoriaAsync(CategoriaDto categoriaDto);

    Task<CustomResponse<CategoriaDto>> EliminarCategoriaAsync(int id);
}
