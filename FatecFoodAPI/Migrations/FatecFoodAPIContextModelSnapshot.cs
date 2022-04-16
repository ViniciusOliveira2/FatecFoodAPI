﻿// <auto-generated />
using FatecFoodAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FatecFoodAPI.Migrations
{
    [DbContext(typeof(FatecFoodAPIContext))]
    partial class FatecFoodAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FatecFoodAPI.Models.AdicionalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Adicionais");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.AdicionalSelecionadoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdicionalId")
                        .HasColumnType("int");

                    b.Property<int>("AdicionalModelId")
                        .HasColumnType("int");

                    b.Property<int>("ItemSelecionadoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdicionalModelId");

                    b.HasIndex("ItemSelecionadoId");

                    b.ToTable("AdicionaisSelecionados");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.BancoImagemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BancoImagens");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.CategoriaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ComandaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NumComanda")
                        .HasColumnType("int");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ItemSelecionadoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItensSelecionados");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ProdutoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.RestauranteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.AdicionalModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.ProdutoModel", "Produto")
                        .WithMany("Adicional")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.AdicionalSelecionadoModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.AdicionalModel", "AdicionalModel")
                        .WithMany("AdicionalSelecionado")
                        .HasForeignKey("AdicionalModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FatecFoodAPI.Models.ItemSelecionadoModel", "ItemSelecionado")
                        .WithMany("AdicionalSelecionado")
                        .HasForeignKey("ItemSelecionadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdicionalModel");

                    b.Navigation("ItemSelecionado");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.CategoriaModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.RestauranteModel", "Restaurante")
                        .WithMany("Categorias")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ComandaModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.RestauranteModel", "Restaurante")
                        .WithMany("Comandas")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ItemSelecionadoModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.ComandaModel", "Comanda")
                        .WithMany("ItemSelecionado")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FatecFoodAPI.Models.ProdutoModel", "Produto")
                        .WithMany("ItemSelecionado")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ProdutoModel", b =>
                {
                    b.HasOne("FatecFoodAPI.Models.CategoriaModel", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.AdicionalModel", b =>
                {
                    b.Navigation("AdicionalSelecionado");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.CategoriaModel", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ComandaModel", b =>
                {
                    b.Navigation("ItemSelecionado");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ItemSelecionadoModel", b =>
                {
                    b.Navigation("AdicionalSelecionado");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.ProdutoModel", b =>
                {
                    b.Navigation("Adicional");

                    b.Navigation("ItemSelecionado");
                });

            modelBuilder.Entity("FatecFoodAPI.Models.RestauranteModel", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Comandas");
                });
#pragma warning restore 612, 618
        }
    }
}
