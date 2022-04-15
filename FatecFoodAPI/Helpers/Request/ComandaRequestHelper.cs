using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class ComandaRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numero Comanda is required")]
        public int NumComanda { get; set; }

        [ForeignKey("Restaurantes")]
        [Required(ErrorMessage = "RestauranteId is required")]
        public int RestauranteId { get; set; }
    }
}
