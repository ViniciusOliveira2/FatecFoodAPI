using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class AdicionalSelecionadoRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ItensSelecionados")]
        [Required(ErrorMessage = "ItemSelecionadoId is required")]
        public int ItemSelecionadoId { get; set; }

        [ForeignKey("Adicionais")]
        [Required(ErrorMessage = "AdicionalId is required")]
        public int AdicionalId { get; set; }
    }
}
