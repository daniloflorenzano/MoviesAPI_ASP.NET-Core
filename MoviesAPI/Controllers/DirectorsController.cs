using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using MoviesAPI.Models;
using MoviesAPI.DTOs;
using MoviesAPI.DTOs.Director;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public async Task<List<DirectorOutputGetAllDTO>> GetDirectors()
        {
            var directors = await _context.Directors.ToListAsync();

            var outputDTOList = new List<DirectorOutputGetAllDTO>();

            foreach (Director director in directors)
            {
                outputDTOList.Add(new DirectorOutputGetAllDTO(director.Id, director.Name));
            }

            return outputDTOList;
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorOutputGetByIdDTO>> GetDirector(long id)
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);

            if (director == null)
            {
                return NotFound();
            }

            var outputDTO = new DirectorOutputGetByIdDTO(director.Id, director.Name);
            return Ok(outputDTO);
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<DirectorOutputPutDTO>> PutDirector(long id, [FromBody] DirectorInputPutDTO directorInputDTO)
        {
            var director = new Director(directorInputDTO.Name);
            director.Id = id;

            if (id != director.Id)
            {
                return BadRequest();
            }

            _context.Directors.Update(director);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var diretorOutputDto = new DirectorOutputPutDTO(director.Id, director.Name);
            return Ok(diretorOutputDto);
        }

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DirectorOutputPostDTO>> PostDirector([FromBody] DirectorInputPostDTO DirectorInputDTO)
        {
            var director = new Director(DirectorInputDTO.Name);

            if (_context.Directors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Directors'  is null.");
            }
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            var directorOutputDto = new DirectorOutputPostDTO(director.Id, director.Name);
            return Ok(directorOutputDto);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(long id)
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DirectorExists(long id)
        {
            return (_context.Directors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
