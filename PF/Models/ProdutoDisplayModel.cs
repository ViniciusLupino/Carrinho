namespace PF.Models
{
    public class ProdutoDisplayModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public string sTerm { get; set; } = "";
        public int CategoriaId { get; set; } = 0;
    }
}
