using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Models
{
    public class BancoImagemModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }
    }
}