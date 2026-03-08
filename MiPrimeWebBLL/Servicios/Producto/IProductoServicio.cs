using PracticaProgramada2BLL.Dtos;

namespace PracticaProgramada2BLL.Servicios.Producto;

public interface IProductoServicio
{
    Task<CustomResponse<List<ProductoDto>>> ObtenerProductosAsync();

    Task<CustomResponse<ProductoDto>> ObtenerProductoPorIdAsync(int id);

    Task<CustomResponse<List<CategoriaDto>>> ObtenerCategoriasAsync();

    Task<CustomResponse<ProductoDto>> AgregarProductoAsync(ProductoDto productoDto);

    Task<CustomResponse<ProductoDto>> ActualizarProductoAsync(ProductoDto productoDto);

    Task<CustomResponse<ProductoDto>> EliminarProductoAsync(int id);
}
