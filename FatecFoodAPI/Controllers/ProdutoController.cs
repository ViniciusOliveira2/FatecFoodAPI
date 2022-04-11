﻿using System.Net;
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
                    Adicional = x.Adicional.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome
                    })
                });
                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Adicionais encontrados.";
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
                var categoria = _context.Categorias.FirstOrDefault(x => x.Id == payload.CategoriaId);

                if (categoria == null)
                {
                    response.Code = (int) HttpStatusCode.BadRequest;
                    response.Message = "Categoria was not found";
                }

                ProdutoModel model = new ProdutoModel()
                {
                    Nome = payload.Nome,
                    Preco = payload.Preco,
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
                response.Message = "An error occurred while trying to insert a new produto";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ProdutoRequest payload)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == id);

            if (produto == null)
            {
                return StatusCode(404, "Produto nao encontrado");
            }

            produto.Nome = payload.Nome;
            produto.Preco = payload.Preco;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == id);

            if (produto == null)
            {
                return StatusCode(404, "Produto nao encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return StatusCode(200, "Produto Removido");
        }
    }
}
