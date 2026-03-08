using Microsoft.AspNetCore.Mvc;
using PracticaProgramada2BLL.Dtos;
using PracticaProgramada2BLL.Servicios.Categoria;

namespace PracticaProgramada2.Controllers;

public class CategoriaController : Controller
{
    private readonly ICategoriaServicio _categoriaServicio;

    public CategoriaController(ICategoriaServicio categoriaServicio)
    {
        _categoriaServicio = categoriaServicio;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ObtenerCategoriaPorId(int id)
    {
        var response = await _categoriaServicio.ObtenerCategoriaPorIdAsync(id);
        return Json(response);
    }

    public async Task<IActionResult> ObtenerCategorias()
    {
        var response = await _categoriaServicio.ObtenerCategoriasAsync();
        return Json(response);
    }

    public async Task<IActionResult> AgregarCategoria(CategoriaDto categoria)
    {
        var response = await _categoriaServicio.AgregarCategoriaAsync(categoria);
        return Json(response);
    }

    public async Task<IActionResult> ActualizarCategoria(CategoriaDto categoria)
    {
        var response = await _categoriaServicio.ActualizarCategoriaAsync(categoria);
        return Json(response);
    }

    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var response = await _categoriaServicio.EliminarCategoriaAsync(id);
        return Json(response);
    }
}
