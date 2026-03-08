using Microsoft.AspNetCore.Mvc;
using PracticaProgramada.Models;
using PracticaProgramada.Services;

namespace PracticaProgramada.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _service.GetAll();
            return View(categorias);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _service.GetById(id);
            return categoria == null ? NotFound() : View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _service.Update(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _service.GetById(id);
            return categoria == null ? NotFound() : View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
      