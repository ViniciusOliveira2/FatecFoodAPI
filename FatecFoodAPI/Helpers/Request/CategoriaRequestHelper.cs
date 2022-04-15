using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class CategoriaRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        [MaxLength(30, ErrorMessage = "Nome must be less than 30 characters")]
        public string Nome { get; set; }

        [ForeignKey("Restaurantes")]
        [Required(ErrorMessage = "RestauranteId is required")]
        public int RestauranteId { get; set; }

        public bool Ativo { get; set; }

    }
}