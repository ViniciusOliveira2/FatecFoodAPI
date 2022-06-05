using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class PedidoModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public bool Entregue { get; set; }

        public double Total { get; set; }

        [ForeignKey("Comandas")]
        public int ComandaId { get; set; }

        public ComandaModel Comanda { get; set; }

        public List<ItemSelecionadoModel> ItemSelecionado { get; set; }
    }
}
