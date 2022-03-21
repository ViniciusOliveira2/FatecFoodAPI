using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Helpers.Request
{
    public class CategoriaRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
    }   
}