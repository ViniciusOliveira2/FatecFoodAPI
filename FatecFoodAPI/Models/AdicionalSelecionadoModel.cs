using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class AdicionalSelecionadoModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ItensSelecionados")]
        public int ItemSelecionadoId { get; set; }

        public ItemSelecionadoModel ItemSelecionado { get; set; }

        [ForeignKey("Adicionais")]
        public int AdicionalId { get; set; }

        public AdicionalModel Adicional { get; set; }
    }
}
