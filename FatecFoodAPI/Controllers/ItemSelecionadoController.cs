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
    public class ItemSelecionadoController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public ItemSelecionadoController(FatecFoodAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var code = 200;
            var data = _context.ItensSelecionados.ToList();

            return StatusCode(code, data);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] ItemSelecionadoRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var produto = _context.Produtos.FirstOrDefault(x => x.Id == payload.ProdutoId);

                if (produto == null)
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Produto was not found";
                }

                var comanda = _context.Comandas.FirstOrDefault(x => x.Id == payload.ComandaId);

                if (comanda == null)
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Comanda was not found";
                }

                ItemSelecionadoModel model = new ItemSelecionadoModel()
                {
                    ProdutoId = payload.ProdutoId,
                    Quantidade = payload.Quantidade,
                    Observacoes = payload.Observacoes,
                    ComandaId = payload.ComandaId
                };

                _context.ItensSelecionados.Add(model);
                _context.SaveChanges();

                response.Message = "ItemSelecionado was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            } catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new itemselecionado";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ItemSelecionadoRequest payload)
        {
            var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(x => x.Id == id);

            if (itemSelecionado == null)
            {
                return StatusCode(404, "ItemSelecionado nao encontrado");
            }

            itemSelecionado.Quantidade = payload.Quantidade;
            itemSelecionado.Observacoes = payload.Observacoes;
            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(c => c.Id == id);

            if (itemSelecionado == null)
            {
                return StatusCode(404, "ItemSelecionado nao encontrado");
            }

            _context.ItensSelecionados.Remove(itemSelecionado);
            _context.SaveChanges();

            return StatusCode(200, "ItemSelecionado Removido");
        }
    }
}
