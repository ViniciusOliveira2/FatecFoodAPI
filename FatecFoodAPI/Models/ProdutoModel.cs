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

        // atributo imagem

        public string Descricao { get; set; }

        // atributo adicionais

        public string Observacoes { get; set; }

        public bool Ativo { get; set; }
    }
}
