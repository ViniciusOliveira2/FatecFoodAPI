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

        public DbSet<AdicionalModel> Adicionais { get; set; }

        public DbSet<RestauranteModel> Restaurantes { get; set; }

        public DbSet<ComandaModel> Comandas { get; set; }

        public DbSet<ItemSelecionadoModel> ItensSelecionados { get; set; }

        public DbSet<AdicionalSelecionadoModel> AdicionaisSelecionados { get; set; }

        public DbSet<BancoImagemModel> BancoImagens { get; set; }

        public DbSet<PedidoModel> Pedidos { get; set; }

    }
}
