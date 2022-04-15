using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class ItemSelecionadoRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produtos")]
        [Required(ErrorMessage = "ProdutoId is required")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Preco is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade must be greater than 0")]
        public int Quantidade { get; set; }

        [MaxLength(100, ErrorMessage = "Nome must be less than 100 characters")]
        public string Observacoes { get; set; }

        [ForeignKey("Comandas")]
        [Required(ErrorMessage = "ComandaId is required")]
        public int ComandaId { get; set; } 
    }
}
