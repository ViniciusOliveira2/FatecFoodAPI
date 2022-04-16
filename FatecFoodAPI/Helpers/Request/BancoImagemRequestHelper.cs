using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Helpers.Request
{
    public class BancoImagemRequest
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }
    }
}