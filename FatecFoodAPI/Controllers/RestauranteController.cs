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
    public class RestauranteController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public RestauranteController(FatecFoodAPIContext context)
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
                var query = _context.Restaurantes
                    .Include(x => x.Categorias)
                    .ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Login,
                    Senha = x.Senha,
                    Categorias = x.Categorias.Select(y => new
                    {
                        Id = y.Id,
                        Nome = y.Nome
                    }),
                    Comandas = x.Comandas.Select(z => new
                    {
                        Id = z.Id,
                        NumComanda = z.NumComanda
                    })
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "Restaurantes encontrados";
                response.Data = result;

                return StatusCode(response.Code, response);
            } catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return StatusCode(response.Code, response);
            }
        }

        [HttpPost]
        public ActionResult Insert([FromBody] RestauranteRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int) HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                RestauranteModel model = new RestauranteModel()
                {
                    Login = payload.Login,
                    Senha = payload.Senha
                };

                _context.Restaurantes.Add(model);
                _context.SaveChanges();

                response.Message = "Restaurante was successfully inserted";
                response.Code = (int) HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err;
                response.Message = "An error occurred while trying to insert a new restaurante";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] RestauranteRequest payload)
        {
            var restaurante = _context.Restaurantes.FirstOrDefault(x => x.Id == id);

            if (restaurante == null)
            {
                return StatusCode(404, "Restaurante nao encontrado");
            }

            restaurante.Login = payload.Login;
            restaurante.Senha = payload.Senha;
            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var restaurante = _context.Restaurantes.FirstOrDefault(c => c.Id == id);

            if (restaurante == null)
            {
                return StatusCode(404, "Restaurante nao encontrado");
            }

            _context.Restaurantes.Remove(restaurante);
            _context.SaveChanges();

            return StatusCode(200, "Restaurante Removido");
        }
    }
}
