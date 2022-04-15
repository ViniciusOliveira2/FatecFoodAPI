using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<ProdutoModel> Produtos { get; set; }

        [ForeignKey("Restaurantes")]
        public int RestauranteId { get; set; }

        public RestauranteModel Restaurante { get; set; }

        public bool Ativo { get; set; }
    }
}
