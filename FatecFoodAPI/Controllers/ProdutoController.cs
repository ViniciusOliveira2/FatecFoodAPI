using System.Net;
using System.Net.Mime;
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
                    Foto = "/Produto/Image?Id=" + x.Id,
                    CategoriaId = x.CategoriaId,
                    Descricao = x.Descricao,
                    Porcao = x.Porcao,
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
                if (!_context.Categorias.Any(c => c.Id == payload.CategoriaId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Categoria was not found";

                    return StatusCode(response.Code, response);
                }

                ProdutoModel model = new ProdutoModel()
                {
                    Nome = payload.Nome,
                    Preco = payload.Preco,
                    Ativo = payload.Ativo,
                    CategoriaId = payload.CategoriaId,
                    Imagem = payload.Imagem,
                    Descricao = payload.Descricao,
                    Porcao = payload.Porcao
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
            produto.Imagem = payload.Imagem;
            produto.Descricao = payload.Descricao;
            produto.Porcao = payload.Porcao;

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
        
        [HttpGet("Image")]
        public ActionResult Image([FromQuery] int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);




            if (produto == null)
            {
                return StatusCode(404, "Produto not found");
            }


            var base64 = produto.Imagem;
            base64 = base64.Replace("data:image/jpeg;base64,","").Replace("data:image/png;base64,","");
            var bytes = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(bytes);

            return File(stream, "image/png");
        }

        [HttpGet("Categoria")]
        public ActionResult Categoria([FromQuery] int id)
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

                var query = _context.Produtos
                    .Where(a => a.CategoriaId == id)
                    .Include(x => x.Adicional)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Preco = x.Preco,
                    Ativo = x.Ativo,
                    Foto = "/Produto/Image?Id=" + x.Id,
                    CategoriaId = x.CategoriaId,
                    Descricao = x.Descricao,
                    Porcao = x.Porcao,
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
                var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

                if (produto == null)
                {
                    return StatusCode(404, "Produto not found");
                }

                var query = _context.Produtos
                    .Where(a => a.Id == id)
                    .Include(x => x.Adicional)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Preco = x.Preco,
                    Ativo = x.Ativo,
                    Foto = "/Produto/Image?Id=" + x.Id,
                    CategoriaId = x.CategoriaId,
                    Descricao = x.Descricao,
                    Porcao = x.Porcao,
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
    }
}
