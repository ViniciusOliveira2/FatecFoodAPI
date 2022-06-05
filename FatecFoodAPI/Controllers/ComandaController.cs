using FatecFoodAPI.Database;
using FatecFoodAPI.Helpers;
using FatecFoodAPI.Helpers.Request;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FatecFoodAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComandaController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public ComandaController(FatecFoodAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Comandas
                    .Include(x => x.Pedido)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    RestauranteId = x.RestauranteId,
                    Pedido = x.Pedido.Select(y => new
                    {
                        Id = y.Id,
                        Data = y.Data,
                        Total = calculaPedidos(y.Id),
                        Entregue = y.Entregue
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Comandas found";
                response.Data = result;

                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;

                return StatusCode(response.Code, response);
            }
        }
        private double calculaPedidos(int id)
        {
            double total = 0;

            var query = _context.ItensSelecionados
                        .Where(x => x.PedidoId == id)
                        .Include(x => x.AdicionalSelecionado)
                        .Include(x => x.Produto)
                        .ToList();

            foreach (var a in query)
            {
                total += (a.Produto.Preco + calculaAdicionais(a.Id)) * a.Quantidade;
            }

            return total;
        }

        private double calculaAdicionais(int id)
        {
            double total = 0;

            var query = _context.AdicionaisSelecionados
                        .Where(x => x.ItemSelecionadoId == id)
                        .Include(x => x.Adicional)
                        .ToList();

            foreach (var a in query)
            {
                total += a.Adicional.Preco;
            }

            return total;
        }

        [HttpPost]
        public ActionResult Insert([FromBody] ComandaRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                if (!_context.Restaurantes.Any(c => c.Id == payload.RestauranteId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Restaurante was not found";

                    return StatusCode(response.Code, response);
                }

                ComandaModel model = new ComandaModel()
                {
                    RestauranteId = payload.RestauranteId
                };

                _context.Comandas.Add(model);
                _context.SaveChanges();

                response.Message = "Comanda Id " + model.Id + " was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            } catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new Comanda";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ComandaRequest payload)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);

            if (comanda == null)
            {
                return StatusCode(404, "Comanda not found");
            }

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);

            if (comanda == null)
            {
                return StatusCode(404, "Comanda not found");
            }

            _context.Comandas.Remove(comanda);
            _context.SaveChanges();

            return StatusCode(200, "Comanda removed");
        }

        [HttpGet("Individual")]
        public ActionResult Individual([FromQuery] int id)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var comanda = _context.Comandas.FirstOrDefault(p => p.Id == id);

                if (comanda == null)
                {
                    return StatusCode(404, "Comanda not found");
                }

                var query = _context.Comandas
                    .Where(a => a.Id == id)
                    .Include(x => x.Pedido)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    RestauranteId = x.RestauranteId,
                    Pedido = x.Pedido.Select(y => new
                    {
                        Id = y.Id,
                        Data = y.Data,
                        Total = calculaPedidos(y.Id),
                        Entregue = y.Entregue
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Comandas found";
                response.Data = result;

                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;

                return StatusCode(response.Code, response);
            }
        }
    }
}
