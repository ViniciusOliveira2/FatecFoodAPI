using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class AdicionalModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("Produtos")]
        public int ProdutoId { get; set; }

        public ProdutoModel Produto { get; set; }
    }
}
