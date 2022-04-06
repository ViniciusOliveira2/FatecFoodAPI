using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Helpers.Request
{
    public class RestauranteRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
