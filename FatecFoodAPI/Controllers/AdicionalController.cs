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
    public class AdicionalController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public AdicionalController(FatecFoodAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var code = 200;
            var data = _context.Adicionais.ToList();

            return StatusCode(code, data);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] AdicionalRequest payload)
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

                AdicionalModel model = new AdicionalModel()
                {
                    Nome = payload.Nome,
                    Ativo = payload.Ativo,
                    ProdutoId = payload.ProdutoId
                };

                _context.Adicionais.Add(model);
                _context.SaveChanges();

                response.Message = "Adicional was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err.Message;
                response.Message = "An error occurred while trying to insert a new adicional";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] AdicionalModel payload)
        {
            var adicional = _context.Adicionais.FirstOrDefault(x => x.Id == id);

            if (adicional == null)
            {
                return StatusCode(404, "Adicional nao encontrado");
            }

            adicional.Nome = payload.Nome;
            adicional.Ativo = payload.Ativo;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var adicional = _context.Adicionais.FirstOrDefault(x => x.Id == id);

            if (adicional == null)
            {
                return StatusCode(404, "Adicional nao encontrado");
            }

            _context.Adicionais.Remove(adicional);
            _context.SaveChanges();

            return StatusCode(200, "Adicional Removido");
        }
    }
}
