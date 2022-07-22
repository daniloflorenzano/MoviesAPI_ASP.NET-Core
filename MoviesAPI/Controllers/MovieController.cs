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
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<MovieListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1)
        {
            return await _movieService.GetByPageAsync(limit, page, cancellationToken);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieOutputGetByIdDTO>> Get(int id)
        {
            var movie = await _movieService.GetById(id);
            
            var outputDTO = new MovieOutputGetByIdDTO(movie.Id, movie.Title, movie.Director.Name);
            return Ok(outputDTO);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieOutputPutDTO>> Put(int id, [FromBody] MovieInputPutDTO inputDTO)
        {
            var movie = new Movie(inputDTO.Title, inputDTO.DirectorId);

            await _movieService.Update(movie, id);

            var outputDTO = new MovieOutputPutDTO(movie.Id, movie.Title);
            return Ok(outputDTO);
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieOutputPostDTO>> Post([FromBody] MovieInputPostDTO inputDTO)
        {
            var movie = await _movieService.Create(new Movie(inputDTO.Title, inputDTO.DirectorId));

            var outputDTO = new MovieOutputPostDTO(movie.Id, movie.Title);
            return Ok(outputDTO);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.Delete(id);
            return Ok();
        }
    }
}
