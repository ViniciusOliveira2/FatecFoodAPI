using FatecFoodAPI.Database;
using FatecFoodAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
            var code = 200;
            var data = _context.Produtos.ToList();

            return StatusCode(code, data);
        }

        [HttpPost]
        public ActionResult Insert([FromBody] ProdutoModel payload)
        {
            _context.Produtos.Add(payload);
            _context.SaveChanges();

            return StatusCode(200, payload);
        }

        [HttpPut]
        public ActionResult Update([FromQuery] int id, [FromBody] ProdutoModel payload)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == id);

            if (produto == null)
            {
                return StatusCode(404, "Produto nao encontrado");
            }

            produto.Nome = payload.Nome;
            produto.Preco = payload.Preco;
            produto.Descricao = payload.Descricao;
            produto.Observacoes = payload.Observacoes;
            produto.Ativo = payload.Ativo;

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
