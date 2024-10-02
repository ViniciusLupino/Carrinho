using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    [Table("DetalheCarrinho")]
    public class DetalheCarrinho
    {
        public int Id { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
