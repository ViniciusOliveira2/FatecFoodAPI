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
                var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(x => x.Id == payload.ItemSelecionadoId);

                if (itemSelecionado == null)
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "ItemSelecionado was not found";
                }

                var adicional = _context.Adicionais.FirstOrDefault(x => x.Id == payload.AdicionalId);

                if (adicional == null)
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Adicional was not found";
                }

                AdicionalSelecionadoModel model = new AdicionalSelecionadoModel()
                {
                    ItemSelecionadoId = payload.ItemSelecionadoId,
                    AdicionalId = payload.AdicionalId,
                };

                _context.AdicionaisSelecionados.Add(model);
                _context.SaveChanges();

                response.Message = "AdicionalSelecionado was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            } catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new adicionalselecionado";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] AdicionalSelecionadoRequest payload)
        {
            var adicioanlSelecionado = _context.AdicionaisSelecionados.FirstOrDefault(a => a.Id == id);

            if (adicioanlSelecionado == null)
            {
                return StatusCode(404, "AdicionalSelecionado nao encontrado");
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
                return StatusCode(404, "AdicionalSelecionado nao encontrado");
            }

            _context.AdicionaisSelecionados.Remove(adicioanlSelecionado);
            _context.SaveChanges();

            return StatusCode(200, "ItemSelecionado Removido");
        }
    }
}
