using FatecFoodAPI.Database;
using FatecFoodAPI.Helpers;
using FatecFoodAPI.Helpers.Request;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FatecFoodAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdicionalSelecionadoController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public AdicionalSelecionadoController(FatecFoodAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var code = 200;
            var data = _context.AdicionaisSelecionados.ToList();

            return StatusCode(code, data);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] AdicionalSelecionadoRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                if (!_context.ItensSelecionados.Any(c => c.Id == payload.ItemSelecionadoId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "ItemSelecionado was not found";

                    return StatusCode(response.Code, response);
                }

                if (!_context.Adicionais.Any(c => c.Id == payload.AdicionalId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Adicional was not found";

                    return StatusCode(response.Code, response);
                }

                var model = new AdicionalSelecionadoModel()
                {
                    ItemSelecionadoId = payload.ItemSelecionadoId,
                    AdicionalId = payload.AdicionalId
                };

                _context.AdicionaisSelecionados.Add(model);
                _context.SaveChanges();

                response.Message = "AdicionalSelecionado Id " + model.Id + " was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            } 
            catch (Exception err)
            {
                response.Error = err.Message;
                response.Message = "An error occurred while trying to insert a new AdicionalSelecionado";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] AdicionalSelecionadoRequest payload)
        {
            var adicioanlSelecionado = _context.AdicionaisSelecionados.FirstOrDefault(a => a.Id == id);

            if (adicioanlSelecionado == null)
            {
                return StatusCode(404, "AdicionalSelecionado not found");
            }

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var adicioanlSelecionado = _context.AdicionaisSelecionados.FirstOrDefault(a => a.Id == id);

            if (adicioanlSelecionado == null)
            {
                return StatusCode(404, "AdicionalSelecionado not found");
            }

            _context.AdicionaisSelecionados.Remove(adicioanlSelecionado);
            _context.SaveChanges();

            return StatusCode(200, "ItemSelecionado removed");
        }

        [HttpGet("ItemSelecionado")]
        public ActionResult ItemSelecionado([FromQuery] int id)
        {
            var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(p => p.Id == id);

            if (itemSelecionado == null)
            {
                return StatusCode(404, "AdicioanlSelecionado not found");
            }

            var code = 200;
            var data = _context.AdicionaisSelecionados
                            .Where(a => a.ItemSelecionadoId == id)
                            .ToList();

            return StatusCode(code, data);
        }
    }
}
