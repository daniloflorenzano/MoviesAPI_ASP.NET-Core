using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.DTOs;
using MoviesAPI.DTOs.Director;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDirectorService _directorService;

        public DirectorController(ApplicationDbContext context, IDirectorService directorService)
        {
            _context = context;
            _directorService = directorService;
        }

        // GET: api/Directors
        [HttpGet]
        public async Task<ActionResult<DirectorListOutputGetAllDTO>> Get(CancellationToken cancellationToken,
            int limit = 5, int page = 1)
        {
            return await _directorService.GetByPageAsync(limit, page, cancellationToken);
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorOutputGetByIdDTO>> Get(long id)
        {
            var director = await _directorService.GetById(id);

            var outputDto = new DirectorOutputGetByIdDTO(director.Id, director.Name);
            return Ok(outputDto);
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Edita um diretor
        /// </summary>
        /// <remarks>
        ///
        ///     PUT /director{id}
        ///     {
        ///         "nome": "Steven Spielberg"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <param name="directorInputDTO">Nome do diretor</param>
        /// <returns>O diretor alterado</returns>
        /// <response code="200">Diretor foi alterado com sucesso</response>
        /// <response code="500">Erro interno inesperado</response>
        /// <response code="400">Erro de validacao"</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DirectorOutputPutDTO>> Put(long id,
            [FromBody] DirectorInputPutDTO directorInputDTO)
        {
            var director = await _directorService.Update(new Director(directorInputDTO.Name), id);

            var diretorOutputDto = new DirectorOutputPutDTO(director.Id, director.Name);
            return Ok(diretorOutputDto);
        }

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Cria um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /director
        ///     {
        ///         "nome": "Steven Spielberg"
        ///     }
        /// 
        /// </remarks>
        /// <param name="DirectorInputDTO">Nome do diretor</param>
        /// <returns>O diretor criado</returns>
        /// <response code="200">Diretor foi criado com sucesso</response>
        /// <response code="500">Erro interno inesperado</response>
        /// <response code="400">Erro de validacao"</response>
        [HttpPost]
        public async Task<ActionResult<DirectorOutputPostDTO>> Post(
            [FromBody] DirectorInputPostDTO DirectorInputDTO)
        {
            var director = await _directorService.Create(new Director(DirectorInputDTO.Name));

            var directorOutputDto = new DirectorOutputPostDTO(director.Id, director.Name);
            return Ok(directorOutputDto);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _directorService.Delete(id);
            return Ok();
        }
    }
}