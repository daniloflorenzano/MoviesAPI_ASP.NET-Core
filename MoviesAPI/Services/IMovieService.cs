using MoviesAPI.DTOs.Movie;
using MoviesAPI.Models;

namespace MoviesAPI.Services;

public interface IMovieService
{
    Task<MovieListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    Task<Movie> GetById(long id);
    Task<Movie> Create(Movie movie);
    Task<Movie> Update(Movie movie, long id);
    Task Delete(long id);
}