using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}

