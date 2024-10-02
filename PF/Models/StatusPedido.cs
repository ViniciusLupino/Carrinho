using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    [Table("StatusPedido")]
    public class StatusPedido
    {

        public int Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        [MaxLength(20), Required]
        public string? StatusNome { get; set; }

    }
}
