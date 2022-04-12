using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class ComandaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumComanda { get; set; }

        [ForeignKey("Restaurantes")]
        public int RestauranteId { get; set; }

        public RestauranteModel Restaurante { get; set; }

        public List<ItemSelecionadoModel> ItemSelecionado { get; set; }
    }
}
