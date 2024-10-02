using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    [Table("Produto")]
    public class Produto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string ProdutoNome { get; set; }
        [Required]
        [MaxLength(256)]
        public string ProdutoDescricao { get; set; }
        [Required]
        public double ProdutoPreco { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public string? Imagem { get; set; }

        public List<DetalhesPedido> DetalhesPedidos { get; set; }
        public List<DetalheCarrinho> DetalhesCarrinho { get; set; }

        [NotMapped]
        public string CategoriaNome { get; set; }
    }
}
