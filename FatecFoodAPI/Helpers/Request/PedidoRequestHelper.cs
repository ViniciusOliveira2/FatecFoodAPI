using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class PedidoRequest
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public bool Entregue { get; set; }

        [ForeignKey("Comandas")]
        [Required(ErrorMessage = "ComandaId is required")]
        public int ComandaId { get; set; }
    }
}
