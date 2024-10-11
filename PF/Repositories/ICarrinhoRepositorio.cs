namespace PF.Repositories
{
    public interface ICarrinhoRepositorio
    {
        Task<int> AddItem(int itemId, int qtd);
        Task<int> RemoverItem(int itemId);
        Task<Carrinho> GetUserCarrinho();
        Task<int> GetCarrinhoItemCount(string userId = "");
    }
}