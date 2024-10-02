using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<DetalheCarrinho> DetalheCarrinhos { get; set; }
        public DbSet<DetalhesPedido> DetalhesPedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<StatusPedido> StatusPedidos { get; set; }
    }
}
