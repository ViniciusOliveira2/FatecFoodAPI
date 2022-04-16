using FatecFoodAPI.Database;
using FatecFoodAPI.Helpers;
using FatecFoodAPI.Helpers.Request;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FatecFoodAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoImagemController : Controller
    {
        private readonly FatecFoodAPIContext _context;

        public BancoImagemController(FatecFoodAPIContext context)
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

                var query = _context.BancoImagens.ToList();

                var result = query.Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Foto = "/BancoImagem/Image?Id=" + x.Id
                });

                response.Code = (int)HttpStatusCode.OK;
                response.Message = "BancoImagem found";
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
        public ActionResult Insert([FromBody] BancoImagemRequest payload)
        {
            var response = new DefaultResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "User was not authorized"
            };

            try
            {
                BancoImagemModel model = new BancoImagemModel()
                {
                    Nome = payload.Nome,
                    Imagem = payload.Imagem
                };

                _context.BancoImagens.Add(model);
                _context.SaveChanges();

                response.Message = "BancoImagem was successfully inserted";
                response.Code = (int)HttpStatusCode.OK;

                return StatusCode(response.Code, response);
            }
            catch (Exception err)
            {
                response.Error = err.Message;
                response.Message = "An error occurred while trying to insert a new BancoImagem";

                return StatusCode(response.Code, response);
            }
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] BancoImagemRequest payload)
        {
            var banco = _context.BancoImagens.FirstOrDefault(p => p.Id == id);

            if (banco == null)
            {
                return StatusCode(404, "BancoImagens not found");
            }

            banco.Nome = payload.Nome;
            banco.Imagem = payload.Imagem;

            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            var banco = _context.BancoImagens.FirstOrDefault(p => p.Id == id);

            if (banco == null)
            {
                return StatusCode(404, "BancoImagens not found");
            }

            _context.BancoImagens.Remove(banco);
            _context.SaveChanges();

            return StatusCode(200, "BancoImagens removed");
        }


        [HttpGet("Image")]
        public ActionResult Image([FromQuery] int id)
        {
            var banco = _context.BancoImagens.FirstOrDefault(c => c.Id == id);



            if (banco == null)
            {
                return StatusCode(404, "ImageBank not found");
            }

            var base64 = banco.Imagem;
            base64 = base64.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", "");
            var bytes = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(bytes);

            return File(stream, "image/png");
        }

    }



}