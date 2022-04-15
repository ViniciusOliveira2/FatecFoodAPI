using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class AdicionalRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        [MaxLength(30, ErrorMessage = "Nome must be less than 30 characters")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("Produtos")]
        [Required(ErrorMessage = "ProdutoId is required")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Preco is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Preco must be greater than 0")]
        public double Preco { get; set; }
    }
}
