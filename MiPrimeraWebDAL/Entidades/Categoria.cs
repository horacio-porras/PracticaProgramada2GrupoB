namespace PracticaProgramada2DAL.Entidades;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Activo { get; set; } = 1;
}
