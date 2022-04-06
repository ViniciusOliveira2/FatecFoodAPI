using System.Net;
using FatecFoodAPI.Database;
using FatecFoodAPI.Helpers;
using FatecFoodAPI.Helpers.Request;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Categorias
                    .Include(x => x.Produtos)    
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Produtos = x.Produtos.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome,
                        Preco = y.Preco
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Produtos encontrados";
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
        public ActionResult Insert([FromBody] CategoriaRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int) HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var restaurante = _context.Restaurantes.FirstOrDefault(x => x.Id == payload.RestauranteId);

                if (restaurante == null)
                {
                    response.Code = (int) HttpStatusCode.BadRequest;
                    response.Message = "Restaurante was not found";
                }

                CategoriaModel model = new CategoriaModel()
                {
                    Nome = payload.Nome,
                    RestauranteId = payload.RestauranteId
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
