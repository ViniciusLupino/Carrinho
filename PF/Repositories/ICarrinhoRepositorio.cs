namespace PF.Repositories
{
    public interface ICarrinhoRepositorio
    {
        Task<bool> AddItem(int itemId, int qtd);
        Task<bool> RemoverItem(int itemId);
        Task<IEnumerable<Carrinho>> GetUserCarrinho();
    }
}