using FatecFoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FatecFoodAPI.Database
{
    public class FatecFoodAPIContext : DbContext
    {

        public FatecFoodAPIContext(DbContextOptions<FatecFoodAPIContext> options) : base(options)
        {

        }

        public DbSet<CategoriaModel> Categorias { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }

    }
}
