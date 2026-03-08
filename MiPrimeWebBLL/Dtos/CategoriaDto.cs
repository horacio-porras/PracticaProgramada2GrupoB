using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada2BLL.Dtos;

public class CategoriaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Activo { get; set; } = 1;
}
