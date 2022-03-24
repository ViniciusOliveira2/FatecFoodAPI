using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Helpers.Request
{
    public class AdicionalRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("Produtos")]
        public int ProdutoId { get; set; }
    }
}
