using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoriaNome { get; set; }
        [MaxLength(256)]
        public string? CategoriaDescricao { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}
