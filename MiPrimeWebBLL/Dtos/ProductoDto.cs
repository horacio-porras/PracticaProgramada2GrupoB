using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada2BLL.Dtos;

public class ProductoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int Stock { get; set; } = 0;

    [Required(ErrorMessage = "Debe seleccionar una categoría")]
    public int CategoriaId { get; set; }

    public string? NombreCategoria { get; set; }
}
