using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs.Movie;
using MoviesAPI.Extensions;
using MoviesAPI.Models;

namespace MoviesAPI.Services;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovieListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken)
    {
        var pagedModel = await _context.Movies
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .PaginateAsync(page, limit,  cancellationToken);

        if (!pagedModel.Items.Any())
            throw new Exception("Nao exitem filmes cadastrados.");
        

        return new MovieListOutputGetAllDTO
        {
            CurrentPage = pagedModel.CurrentPage,
            TotalPages = pagedModel.TotalPages,
            TotalITems = pagedModel.TotalItems,
            Items = pagedModel.Items.Select(movie => new MovieOutputGetAllDTO(movie.Id, movie.Title)).ToList()
        };
    }

    public async Task<Movie> GetById(long id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

        if (movie == null)
            throw new Exception("Filme nao encontrado.");

        return movie;
    }

    public async Task<Movie> Create(Movie movie)
    {
        _context.Movies.Add(movie);

        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie> Update(Movie movie, long id)
    {
        movie.Id = id;
        _context.Movies.Update(movie);

        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task Delete(long id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
        _context.Remove(movie);

        await _context.SaveChangesAsync();
    }
}