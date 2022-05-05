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
                    Ativo = x.Ativo,
                    Foto = "/Categoria/Image?Id=" + x.Id,
                    RestauranteId = x.RestauranteId,
                    Produtos = x.Produtos.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome,
                        Preco = y.Preco,
                        Ativo = y.Ativo,
                        Descricao = y.Descricao,
                        Foto = "/Produto/Image?Id=" + y.Id
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Categorias found";
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
                if (!_context.Restaurantes.Any(c => c.Id == payload.RestauranteId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Restaurante was not found";

                    return StatusCode(response.Code, response);
                }

                CategoriaModel model = new CategoriaModel()
                {
                    Nome = payload.Nome,
                    RestauranteId = payload.RestauranteId,
                    Ativo = payload.Ativo,
                    Imagem = payload.Imagem
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
                response.Message = "An error occurred while trying to insert a new Categoria";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] CategoriaRequest payload)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return StatusCode(404, "Categoria not found");
            }

            categoria.Nome = payload.Nome;
            categoria.Ativo = payload.Ativo;
            categoria.Imagem = payload.Imagem;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return StatusCode(404, "Categoria not found");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return StatusCode(200, "Categoria removed");
        }

        [HttpGet("Image")]
        public ActionResult Image([FromQuery] int id)
        {
            var categoria =_context.Categorias.FirstOrDefault(c => c.Id == id);

            

            if (categoria == null)
            {
                return StatusCode(404, "Categoria not found");
            }

            var base64 = categoria.Imagem;
            base64 = base64.Replace("data:image/jpeg;base64,","").Replace("data:image/png;base64,","");
            var bytes = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(bytes);

            return File(stream, "image/png");
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
                var categoria = _context.Categorias.FirstOrDefault(p => p.Id == id);

                if (categoria == null)
                {
                    return StatusCode(404, "Categoria not found");
                }

                var query = _context.Categorias
                    .Where(c => c.Id == id)
                    .Include(x => x.Produtos)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo,
                    Foto = "/Categoria/Image?Id=" + x.Id,
                    RestauranteId = x.RestauranteId,
                    Produtos = x.Produtos.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome,
                        Preco = y.Preco,
                        Ativo = y.Ativo,
                        Descricao = y.Descricao,
                        Foto = "/Produto/Image?Id=" + x.Id
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Categorias found";
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
