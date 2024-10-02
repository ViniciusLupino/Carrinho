using Microsoft.EntityFrameworkCore;
using PF.Models;
using System.IO;

namespace PF.Repositories
{
    public class LojaRepositorio : ILojaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public LojaRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _db.Categorias.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutos(string sTerm = "", int categoriaId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Produto> produtos = await (from Produto in _db.Produtos
                                                   join Categoria in _db.Categorias on Produto.CategoriaId equals Categoria.Id
                                                   where string.IsNullOrWhiteSpace(sTerm) || (Produto != null && Produto.ProdutoNome.ToLower().StartsWith(sTerm))

                                                   select new Produto
                                                   {
                                                       Id = Produto.Id,
                                                       Imagem = Produto.Imagem,
                                                       ProdutoPreco = Produto.ProdutoPreco,
                                                       ProdutoNome = Produto.ProdutoNome,
                                                       CategoriaNome = Categoria.CategoriaNome,
                                                       CategoriaId = Categoria.Id,

                                                   }).ToListAsync();

            if (categoriaId > 0)
            {
                produtos = produtos.Where(a => a.CategoriaId == categoriaId).ToList();
            }

            return produtos;
        }
    }
}