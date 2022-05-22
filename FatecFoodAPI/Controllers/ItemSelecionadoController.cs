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
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.ItensSelecionados
                    .Include(x => x.AdicionalSelecionado)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade,
                    Observacoes = x.Observacoes,
                    PedidoId = x.PedidoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        AdicionalId = y.AdicionalId
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "ItemSelecionado found";
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
        public async Task<ActionResult> Insert([FromBody] ItemSelecionadoRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                if (!_context.Produtos.Any(key => key.Id == payload.ProdutoId))
                {
                    response.Code = (int) HttpStatusCode.BadRequest;
                    response.Message = "Produto was not found";

                    return StatusCode(response.Code, response);
                }

                if (!_context.Pedidos.Any(key => key.Id == payload.PedidoId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Pedido was not found";

                    return StatusCode(response.Code, response);
                }

                var model = new ItemSelecionadoModel()
                {
                    ProdutoId = payload.ProdutoId,
                    Quantidade = payload.Quantidade,
                    Observacoes = payload.Observacoes,
                    PedidoId = payload.PedidoId
                };

                _context.ItensSelecionados.Add(model);
                await _context.SaveChangesAsync();

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "ItemSelecionado Id " + model.Id + " was successfully inserted";
                response.Data = model;
                
                return StatusCode(response.Code, response);
                
            } 
            catch (Exception err)
            {
                response.Code = (int) HttpStatusCode.InternalServerError;
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new ItemSelecionado";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ItemSelecionadoRequest payload)
        {
            var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(i => i.Id == id);

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
            var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(i => i.Id == id);

            if (itemSelecionado == null)
            {
                return StatusCode(404, "ItemSelecionado not found");
            }

            _context.ItensSelecionados.Remove(itemSelecionado);
            _context.SaveChanges();

            return StatusCode(200, "ItemSelecionado removed");
        }

        [HttpGet("Pedido")]
        public ActionResult Pedido([FromQuery] int id)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var itemSelecionado = _context.ItensSelecionados.FirstOrDefault(p => p.Id == id);

                if (itemSelecionado == null)
                {
                    return StatusCode(404, "ItemSelecionado not found");
                }

                var query = _context.ItensSelecionados
                    .Where(a => a.PedidoId == id)
                    .Include(x => x.AdicionalSelecionado)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade,
                    Observacoes = x.Observacoes,
                    PedidoId = x.PedidoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        AdicionalId = y.AdicionalId
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "ItemSelecionado found";
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
