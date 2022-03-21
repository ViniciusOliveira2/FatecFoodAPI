using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Models
{
    public class ProdutoModel
    {
        [Key]
        public int Id { get; set; } 

        public string Nome { get; set; } 

        public double Preco { get; set; }

        // atributo imagem

        public string Descricao { get; set; }

        // atributo adicionais

        public string Observacoes { get; set; }

        public bool Ativo { get; set; }
    }
}
