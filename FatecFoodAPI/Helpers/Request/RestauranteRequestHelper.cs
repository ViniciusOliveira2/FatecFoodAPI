using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Helpers.Request
{
    public class RestauranteRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha is required")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        [MaxLength(30, ErrorMessage = "Nome must be less than 30 characters")]
        public string Nome { get; set; }
    }
}
