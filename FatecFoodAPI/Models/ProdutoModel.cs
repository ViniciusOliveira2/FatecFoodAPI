using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class ProdutoModel
    {
        [Key]
        public int Id { get; set; } 

        public string Nome { get; set; } 

        public double Preco { get; set; }

        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }

        public CategoriaModel Categoria { get; set; }

        public bool Ativo { get; set; }

        public List<AdicionalModel> Adicional { get; set; }

        public List<ItemSelecionadoModel> ItemSelecionado { get; set; }

        public string Imagem { get; set; }

        public string Descricao { get; set; }
    }
}
