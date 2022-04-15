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
                    .Include(x => x.ItemSelecionado)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    NumComanda = x.NumComanda,
                    RestauranteId = x.RestauranteId,
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
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
                var restaurante = _context.Restaurantes.FirstOrDefault(r => r.Id == payload.RestauranteId);

                if (restaurante == null)
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Restaurante was not found";
                };

                ComandaModel model = new ComandaModel()
                {
                    NumComanda = payload.NumComanda,
                    RestauranteId = payload.RestauranteId
                };

                _context.Comandas.Add(model);
                _context.SaveChanges();

                response.Message = "Comanda was successfully inserted";
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

            comanda.NumComanda = payload.NumComanda;

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
    }
}
