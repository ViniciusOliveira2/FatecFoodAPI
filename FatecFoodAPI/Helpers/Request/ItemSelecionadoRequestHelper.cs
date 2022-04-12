using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class ItemSelecionadoRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produtos")]
        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public string Observacoes { get; set; }

        [ForeignKey("Comandas")]
        public int ComandaId { get; set; } 
    }
}
