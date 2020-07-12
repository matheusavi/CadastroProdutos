using CadastroProduto.CQS;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProduto.Api.Principal.Controllers
{
    /// <summary>
    /// CRUD de produtos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProdutoQueries _queries;
        private readonly ILogger<ProdutoController> _logger;


        /// <summary>
        /// Constrói um controller de produtos
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="queries"></param>
        /// <param name="logger"></param>
        public ProdutoController(
            IMediator mediator,
            IProdutoQueries queries,
            ILogger<ProdutoController> logger)
        {
            _mediator = mediator;
            _queries = queries;
            _logger = logger;
        }

        /// <summary>
        /// Obter um produto por Id
        /// </summary>
        /// <param name="id" example="1">Id do produto</param>
        /// <response code="200">Produto encontrado</response>
        /// <response code="404">Produto não encontrado</response>
        /// <response code="500">Ocorreu um erro ao encontrar o produto</response>
        /// <returns>O produto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProdutoDto>> GetProduto(long id)
        {
            try
            {
                var produto = await _queries.FindAsync(id);

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

        /// <summary>
        /// Atualiza um produto
        /// </summary>
        /// <param name="id" example="1">Id do produto</param>
        /// <param name="request"></param>
        /// <response code="204">Produto atualizado</response>
        /// <response code="400">Produto está inválido</response>
        /// <response code="404">Produto não encontrado</response>
        /// <response code="500">Ocorreu um erro atualizando o produto</response>
        /// <returns>O produto</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IEnumerable<ErrorModel>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutProduto(long id, [FromBody]AtualizarProdutoCommand request)
        {
            try
            {
                request.SetId(id);
                if (!await _mediator.Send(request))
                    return NotFound();

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Parse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao atualizar o produto");
            }
        }

        /// <summary>
        /// Criar um produto
        /// </summary>
        /// <response code="201">Produto criado</response>
        /// <response code="400">Produto está inválido</response>
        /// <response code="500">Ocorreu um erro ao criar o produto</response>
        /// <returns>O produto com seu id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoDto), 201)]
        [ProducesResponseType(typeof(IEnumerable<ErrorModel>), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProdutoDto>> PostProduto([FromBody]CriarProdutoCommand request)
        {
            try
            {
                var dto = await _mediator.Send(request);

                return CreatedAtAction("GetProduto", new { dto.Id }, dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Parse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar criar o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar criar o produto");
            }
        }


        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id" example="1">O id do produto</param>
        /// <response code="200">Produto deletado</response>
        /// <response code="404">Produto não encontrado</response>
        /// <response code="500">Ocorreu um erro ao deletar o produto</response>
        /// <returns>O produto</returns>
        [ProducesResponseType(typeof(ProdutoDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDto>> DeleteProduto(long id)
        {
            try
            {
                var dto = await _mediator.Send(new DeletarProdutoCommand(id));

                if (dto == null)
                {
                    return NotFound();
                }

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro deletando o produto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro deletando o produto");
            }
        }
    }
}