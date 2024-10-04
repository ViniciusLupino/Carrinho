using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Net.Quic;

namespace PF.Repositories
{
    public class CarrinhoRepositorio
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
        public async Task<bool> AddItem(int itemId, int qtd)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return false;

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

                var cartItem = _db.DetalheCarrinhos.FirstOrDefault(a => a.CarrinhoId == cart.Id && a.Id == itemId);
                if (cartItem is not null)
                {
                    cartItem.Quantidade += qtd;
                }
                else
                {
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoverItem(int itemId)
        {
            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return false;

                var cart = await GetCarrinho(userId);
                if (cart is null)
                {
                    return false;
                }

                var cartItem = _db.DetalheCarrinhos.FirstOrDefault(a => a.CarrinhoId == cart.Id && a.Id == itemId);
                if (cartItem is null)
                {
                    return false;
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Carrinho>> GetUserCarrinho()
        {

        }


        private async Task<Carrinho> GetCarrinho(string userId)
        {
            var cart = await _db.Carrinhos.FirstOrDefaultAsync(c => c.UserId == userId);
            return cart;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

    }
}
