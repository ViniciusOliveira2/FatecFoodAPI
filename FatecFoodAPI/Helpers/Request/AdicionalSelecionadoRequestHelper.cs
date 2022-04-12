using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class AdicionalSelecionadoRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ItensSelecionados")]
        public int ItemSelecionadoId { get; set; }

        [ForeignKey("Adicionais")]
        public int AdicionalId { get; set; }
    }
}
