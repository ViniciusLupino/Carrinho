using PF.Models;
using System.IO;

namespace PF
{
    public interface ILojaRepositorio
    {
        Task<IEnumerable<Produto>> GetProdutos(string sTerm = "", int categoriaId = 0);
        Task<IEnumerable<Categoria>> GetCategorias();
    }
}