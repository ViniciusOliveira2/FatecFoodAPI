using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class ComandaRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumComanda { get; set; }

        [ForeignKey("Restaurantes")]
        public int RestauranteId { get; set; }
    }
}
