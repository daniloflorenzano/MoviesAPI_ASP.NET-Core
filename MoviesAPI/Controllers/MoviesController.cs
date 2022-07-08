using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using MoviesAPI.DTOs.Movie;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<List<MovieOutputGetAllDTO>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();

            var outputDTOList = new List<MovieOutputGetAllDTO>();

            foreach (Movie movie in movies)
            {
                outputDTOList.Add(new MovieOutputGetAllDTO(movie.Id, movie.Title));
            }

            return outputDTOList;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieOutputGetByIdDTO>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(movie => movie.Director)
                .FirstOrDefaultAsync(movie => movie.Id == id);


            if (_context.Movies == null)
            {
                return NotFound();
            }
            
            if (movie == null)
            {
                return NotFound();
            }

            var outputDTO = new MovieOutputGetByIdDTO(movie.Id, movie.Title, movie.Director.Name);
            return Ok(outputDTO);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieOutputPutDTO>> PutMovie(int id, [FromBody] MovieInputPutDTO inputDTO)
        {
            var movie = new Movie(inputDTO.Title, inputDTO.DirectorId);
            movie.Id = id;

            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Movies.Update(movie);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var outputDTO = new MovieOutputPutDTO(movie.Id, movie.Title);
            return Ok(outputDTO);
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieOutputPostDTO>> PostMovie([FromBody] MovieInputPostDTO inputDTO)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == inputDTO.DirectorId);

            if (director == null) return NotFound("Director not found");

            var movie = new Movie(inputDTO.Title, director.Id);

            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var outputDTO = new MovieOutputPostDTO(movie.Id, movie.Title);
            return Ok(outputDTO);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
