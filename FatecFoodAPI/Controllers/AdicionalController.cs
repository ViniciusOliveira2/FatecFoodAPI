﻿using FatecFoodAPI.Database;
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
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Adicionais
                    .Include(x => x.AdicionalSelecionado)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo,
                    Preco = x.Preco,
                    ProdutoId = x.ProdutoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ItemSelecionadoId = y.ItemSelecionadoId
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Adicionais found";
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
        public ActionResult Insert([FromBody] AdicionalRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                if (!_context.Produtos.Any(c => c.Id == payload.ProdutoId))
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Produto was not found";

                    return StatusCode(response.Code, response);
                }

                AdicionalModel model = new AdicionalModel()
                {
                    Nome = payload.Nome,
                    Ativo = payload.Ativo,
                    Preco = payload.Preco,
                    ProdutoId = payload.ProdutoId
                };

                _context.Adicionais.Add(model);
                _context.SaveChanges();

                response.Message = "Adicional Id " + model.Id + " was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err.Message;
                response.Message = "An error occurred while trying to insert a new Adicional";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] AdicionalRequest payload)
        {
            var adicional = _context.Adicionais.FirstOrDefault(a => a.Id == id);

            if (adicional == null)
            {
                return StatusCode(404, "Adicional not found");
            }

            adicional.Nome = payload.Nome;
            adicional.Ativo = payload.Ativo;
            adicional.Preco = payload.Preco;
            adicional.ProdutoId = payload.ProdutoId;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var adicional = _context.Adicionais.FirstOrDefault(a => a.Id == id);

            if (adicional == null)
            {
                return StatusCode(404, "Adicional not found");
            }

            _context.Adicionais.Remove(adicional);
            _context.SaveChanges();

            return StatusCode(200, "Adicional removed");
        }

        [HttpGet("Produto")]
        public ActionResult Produto([FromQuery] int id)
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

                var query = _context.Adicionais
                                .Where(a => a.ProdutoId == id)
                                .Include(x => x.AdicionalSelecionado)
                                .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo,
                    Preco = x.Preco,
                    ProdutoId = x.ProdutoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ItemSelecionadoId = y.ItemSelecionadoId
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Adicionais found";
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
                var adicional = _context.Adicionais.FirstOrDefault(p => p.Id == id);

                if (adicional == null)
                {
                    return StatusCode(404, "Adicional not found");
                }

                var query = _context.Adicionais
                                .Where(x => x.Id == id)
                                .Include(x => x.AdicionalSelecionado)
                                .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo,
                    Preco = x.Preco,
                    ProdutoId = x.ProdutoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ItemSelecionadoId = y.ItemSelecionadoId
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Adicionais found";
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

        [HttpGet("Nome")]
        public ActionResult Nome([FromQuery] string? nome)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                var query = _context.Adicionais
                    .Include(produto => produto.AdicionalSelecionado)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(nome))
                {
                    query = query.Where(adicional => adicional.Nome.Contains(nome));
                }

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativo = x.Ativo,
                    Preco = x.Preco,
                    ProdutoId = x.ProdutoId,

                    AdicionalSelecionado = x.AdicionalSelecionado.Select(y => new
                    {
                        Id = y.Id,
                        ItemSelecionadoId = y.ItemSelecionadoId
                    })
                });
                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Adicionais found.";
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
