using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [Required]
        public int PedidoStatusId { get; set; }
        public StatusPedido StatusPedido { get; set; }

        public bool Deletado { get; set; } = false;

        public List<DetalhesPedido> DetalhesPedidos { get; set; }

    }
}
