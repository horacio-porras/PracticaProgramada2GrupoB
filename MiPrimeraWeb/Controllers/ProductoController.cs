using Microsoft.AspNetCore.Mvc;
using PracticaProgramada2BLL.Dtos;
using PracticaProgramada2BLL.Servicios.Producto;

namespace PracticaProgramada2.Controllers;

public class ProductoController : Controller
{
    private readonly IProductoServicio _productoServicio;

    public ProductoController(IProductoServicio productoServicio)
    {
        _productoServicio = productoServicio;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ObtenerProductoPorId(int id)
    {
        var response = await _productoServicio.ObtenerProductoPorIdAsync(id);
        return Json(response);
    }

    public async Task<IActionResult> ObtenerProductos()
    {
        var response = await _productoServicio.ObtenerProductosAsync();
        return Json(response);
    }

    public async Task<IActionResult> ObtenerCategorias()
    {
        var response = await _productoServicio.ObtenerCategoriasAsync();
        return Json(response);
    }

    public async Task<IActionResult> AgregarProducto(ProductoDto producto)
    {
        var response = await _productoServicio.AgregarProductoAsync(producto);
        return Json(response);
    }

    public async Task<IActionResult> ActualizarProducto(ProductoDto producto)
    {
        var response = await _productoServicio.ActualizarProductoAsync(producto);
        return Json(response);
    }

    public async Task<IActionResult> EliminarProducto(int id)
    {
        var response = await _productoServicio.EliminarProductoAsync(id);
        return Json(response);
    }
}
