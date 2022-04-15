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
    public class ProdutoController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public ProdutoController(FatecFoodAPIContext context)
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
                var query = _context.Produtos
                    .Include(x => x.Adicional)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Preco = x.Preco,
                    Ativo = x.Ativo,
                    Adicional = x.Adicional.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome,
                        Ativo = y.Ativo,
                        Preco = y.Preco
                    })
                });
                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Produtos found.";
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
        public ActionResult Insert([FromBody] ProdutoRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int) HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.Id == payload.CategoriaId);

                if (categoria == null)
                {
                    response.Code = (int) HttpStatusCode.BadRequest;
                    response.Message = "Categoria was not found";
                }

                ProdutoModel model = new ProdutoModel()
                {
                    Nome = payload.Nome,
                    Preco = payload.Preco,
                    Ativo = payload.Ativo,
                    CategoriaId = payload.CategoriaId
                };

                _context.Produtos.Add(model);
                _context.SaveChanges();

                response.Message = "Produto was successfully inserted";
                response.Code = (int) HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err.Message;
                response.Message = "An error occurred while trying to insert a new Produto";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ProdutoRequest payload)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return StatusCode(404, "Produto not found");
            }

            produto.Nome = payload.Nome;
            produto.Preco = payload.Preco;
            produto.CategoriaId = payload.CategoriaId;
            produto.Ativo = payload.Ativo;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return StatusCode(404, "Produto not found");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return StatusCode(200, "Produto removed");
        }
    }
}
