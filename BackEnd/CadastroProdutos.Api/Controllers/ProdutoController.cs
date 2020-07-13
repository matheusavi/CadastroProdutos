using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace CadastroProdutos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(MongoDbContext mongoDbContext, ILogger<ProdutoController> logger)
        {
            _context = mongoDbContext;
            _logger = logger;
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Produto>> GetProduto(Guid guid)
        {
            try
            {
                var request = await _context.Produtos.FindAsync(Builders<Produto>.Filter.Eq(x => x.Guid, guid));
                var produto = await request.FirstOrDefaultAsync();
                if (produto == null)
                {
                    return NotFound();
                }

                return Ok(produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao obter o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao obter o produto");
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutProduto([FromBody]Produto produto)
        {
            try
            {
                var result = await _context.Produtos.ReplaceOneAsync(Builders<Produto>.Filter.Eq(x => x.Guid, produto.Guid), produto);

                if (result.ModifiedCount == 0)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao atualizar o produto");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostProduto([FromBody]Produto produto)
        {
            try
            {
                await _context.Produtos.InsertOneAsync(produto);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar criar o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar criar o produto");
            }
        }


        [HttpDelete("{guid}")]
        public async Task<ActionResult> DeleteProduto(Guid guid)
        {
            try
            {
                var result = await _context.Produtos.DeleteOneAsync(Builders<Produto>.Filter.Eq(x => x.Guid, guid));

                if (result.DeletedCount == 0)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro deletando o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro deletando o produto");
            }
        }
    }
}
