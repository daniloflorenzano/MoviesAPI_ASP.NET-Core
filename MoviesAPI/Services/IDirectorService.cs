using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Services;

public interface IDirectorService
{
    Task<DirectorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    Task<Director> GetById(long id);
    Task<Director> Create(Director director);
    Task<Director> Update(Director director, long id);
    Task Delete(long id);
}