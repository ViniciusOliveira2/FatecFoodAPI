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

        public string Observacoes { get; set; }

        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }

        public CategoriaModel Categoria { get; set; }

        public bool Ativo { get; set; }
    }
}
