using System.ComponentModel.DataAnnotations;
namespace PARCIAL1A.Models
{
    public class Libros
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }
    }
}
