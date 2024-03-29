﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatecFoodAPI.Models
{
    public class ComandaModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurantes")]
        public int RestauranteId { get; set; }

        public RestauranteModel Restaurante { get; set; }

        public List<PedidoModel> Pedido { get; set; }
    }
}
