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
    public class PedidoController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public PedidoController(FatecFoodAPIContext context)
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
                var query = _context.Pedidos
                    .Include(x => x.ItemSelecionado)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ComandaId = x.ComandaId,
                    Data = x.Data,
                    Entregue = x.Entregue,
                    Total = calculaTotal(x.Id),
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Pedidos found";
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

        private double calculaTotal(int id)
        {
            double total = 0;

            var query = _context.ItensSelecionados
                        .Where(x => x.PedidoId == id)
                        .Include(x => x.AdicionalSelecionado)
                        .Include(x => x.Produto)
                        .ToList();

            foreach (var a in query)
            {
                total += (a.Produto.Preco + calculaAdicionais(a.Id)) * a.Quantidade;
            }

            return total;
        }

        private double calculaAdicionais(int id)
        {
            double total = 0;

            var query = _context.AdicionaisSelecionados
                        .Where(x => x.ItemSelecionadoId == id)
                        .Include(x => x.Adicional)
                        .ToList();

            foreach (var a in query)
            {
                total += a.Adicional.Preco;
            }

            return total;
        }

        [HttpPost]
        public ActionResult Insert([FromBody] PedidoRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                if (!_context.Comandas.Any(c => c.Id == payload.ComandaId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Comanda was not found";

                    return StatusCode(response.Code, response);
                }

                PedidoModel model = new PedidoModel()
                {
                    ComandaId = payload.ComandaId,
                    Data = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
                    Entregue = payload.Entregue
                };

                _context.Pedidos.Add(model);
                _context.SaveChanges();

                response.Message = "Pedido Id " + model.Id + " was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new Pedido";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] PedidoRequest payload)
        {
            var pedido = _context.Pedidos.FirstOrDefault(c => c.Id == id);

            if (pedido == null)
            {
                return StatusCode(404, "Pedido not found");
            }

            pedido.Entregue = payload.Entregue;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(c => c.Id == id);

            if (pedido == null)
            {
                return StatusCode(404, "Pedido not found");
            }

            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();

            return StatusCode(200, "Pedido removed");
        }

        [HttpGet("Comanda")]
        public ActionResult Comanda([FromQuery] int id)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Pedidos
                                .Where(a => a.ComandaId == id)
                                .Include(x => x.ItemSelecionado)
                                .ToList();

                if (query == null)
                {
                    return StatusCode(404, "Pedido not found");
                }

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ComandaId = x.ComandaId,
                    Data = x.Data,
                    Entregue = x.Entregue,
                    Total = calculaTotal(x.Id),
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Pedidos found";
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
                var query = _context.Pedidos
                                .Where(x => x.Id == id)
                                .Include(x => x.ItemSelecionado)
                                .ToList();

                if (query == null)
                {
                    return StatusCode(404, "Pedido not found");
                }

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ComandaId = x.ComandaId,
                    Data = x.Data,
                    Entregue = x.Entregue,
                    Total = calculaTotal(x.Id),
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Pedidos found";
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

        [HttpGet("Entregue")]
        public ActionResult Entregue()
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Pedidos
                                .Where(x => x.Entregue == true)
                                .Include(x => x.ItemSelecionado)
                                .ToList();

                if (query == null)
                {
                    return StatusCode(404, "Pedido not found");
                }

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ComandaId = x.ComandaId,
                    Data = x.Data,
                    Entregue = x.Entregue,
                    Total = calculaTotal(x.Id),
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Pedidos found";
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

        [HttpGet("NaoEntregue")]
        public ActionResult NaoEntregue()
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Pedidos
                                .Where(x => x.Entregue == false)
                                .Include(x => x.ItemSelecionado)
                                .ToList();

                if (query == null)
                {
                    return StatusCode(404, "Pedido not found");
                }

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    ComandaId = x.ComandaId,
                    Data = x.Data,
                    Entregue = x.Entregue,
                    Total = calculaTotal(x.Id),
                    ItemSelecionado = x.ItemSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ProdutoId = y.ProdutoId,
                        Quantidade = y.Quantidade,
                        Observacoes = y.Observacoes
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Pedidos found";
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
