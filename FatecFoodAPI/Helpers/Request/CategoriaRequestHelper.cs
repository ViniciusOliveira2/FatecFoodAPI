using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class CategoriaRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [ForeignKey("Restaurantes")]
        public int RestauranteId { get; set; }

    }
}