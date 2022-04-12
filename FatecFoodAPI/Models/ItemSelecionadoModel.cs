using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class ItemSelecionadoModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produtos")]
        public int ProdutoId { get; set; }

        public ProdutoModel Produto { get; set; }

        public int Quantidade { get; set; }

        public string Observacoes { get; set; }

        [ForeignKey("Comandas")]
        public int ComandaId { get; set; }

        public ComandaModel Comanda { get; set; }

        public List<AdicionalSelecionadoModel> AdicionalSelecionado { get; set; }
    }
}
