using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Text;
using System.Net.Quic;

namespace PF.Repositories
{
    public class CarrinhoRepositorio : ICarrinhoRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrinhoRepositorio(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int itemId, int qtd)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("Usuário não está logado!");

                var cart = await GetCarrinho(userId);
                if (cart is null)
                {
                    cart = new Carrinho
                    {
                        UserId = userId
                    };
                    _db.Carrinhos.Add(cart);
                }
                _db.SaveChanges();

                var cartItem = _db.DetalheCarrinhos
                                  .FirstOrDefault(a => a.CarrinhoId == cart.Id && a.ProdutoId == itemId);
                if (cartItem != null)
                {
                    cartItem.Quantidade += qtd;
                }
                else
                {
                    var produto = _db.Produtos.Find(itemId);
                    cartItem = new DetalheCarrinho
                    {
                        ProdutoId = itemId,
                        CarrinhoId = cart.Id,
                        Quantidade = qtd
                    };
                    _db.DetalheCarrinhos.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {

            }
            var totalItems = await GetCarrinhoItemCount(userId);
            return totalItems;
        }

        public async Task<int> RemoverItem(int itemId)
        {
            string userId = GetUserId();

            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("Usuário não está logado!");

                var cart = await GetCarrinho(userId);
                if (cart is null)
                {
                    throw new Exception("Carrinho inválido!");

                }

                var cartItem = _db.DetalheCarrinhos
                                .FirstOrDefault(a => a.CarrinhoId == cart.Id && a.Id == itemId);
                if (cartItem is null)
                {
                    throw new Exception("Carrinho vazio!");

                }
                else if (cartItem.Quantidade == 1)
                {
                    _db.DetalheCarrinhos.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantidade = cartItem.Quantidade - 1;
                }
                _db.SaveChanges();
                //transaction.Commit();

            }
            catch (Exception ex)
            {

            }
            var totalItems = await GetCarrinhoItemCount(userId);
            return totalItems;
        }

        public async Task<Carrinho> GetUserCarrinho()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Identificador de usuário inválido!");
            var carrinhoDeCompras = await _db.Carrinhos
                .Include(a => a.CarrinhoDetalhes)
                .ThenInclude(a => a.Produto)
                .ThenInclude(a => a.Categoria)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return carrinhoDeCompras;
        }

        private async Task<Carrinho> GetCarrinho(string userId)
        {
            var cart = await _db.Carrinhos.FirstOrDefaultAsync(c => c.UserId == userId);
            return cart;
        }

        public async Task<int> GetCarrinhoItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from cart in _db.Carrinhos
                              join cartItem in _db.DetalheCarrinhos
                              on cart.Id equals cartItem.Id
                              select cartItem).ToListAsync();
            return data.Count;


        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

    }
}
