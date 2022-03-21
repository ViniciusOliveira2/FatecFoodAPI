using System.Net;
using FatecFoodAPI.Database;
using FatecFoodAPI.Helpers;
using FatecFoodAPI.Helpers.Request;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FatecFoodAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public CategoriaController(FatecFoodAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var code = 200;
            var data = _context.Categorias.ToList();

            return StatusCode(code, data);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] CategoriaRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int) HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {

                CategoriaModel model = new CategoriaModel()
                {
                    Nome = payload.Nome
                };

                _context.Categorias.Add(model);
                _context.SaveChanges();

                response.Message = "Categoria was successfully inserted";
                response.Code = (int) HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new categoria";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] CategoriaModel payload)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.Id == id);

            if (categoria == null)
            {
                return StatusCode(404, "Categoria nao encontrada");
            }

            categoria.Nome = payload.Nome;
            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return StatusCode(404, "Categoria nao encontrada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return StatusCode(200, "Categoria Removida");
        }

    }
}
