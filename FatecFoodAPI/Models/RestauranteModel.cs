using System.ComponentModel.DataAnnotations;

namespace FatecFoodAPI.Models
{
    public class RestauranteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        public string Senha { get; set; }

        public List<CategoriaModel> Categorias { get; set; }
    }
}
