using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Extensions;
using MoviesAPI.Models;


namespace MoviesAPI.Services;

public class DirectorService : IDirectorService
{
    private readonly ApplicationDbContext _context;

    public DirectorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DirectorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken)
    {
        var pagedModel = await _context.Directors
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .PaginateAsync(page, limit, cancellationToken);

        if (!pagedModel.Items.Any())
            throw new Exception("Nao existem diretores cadastrados.");

        return new DirectorListOutputGetAllDTO
        {
            CurrentPage = pagedModel.CurrentPage,
            TotalPages = pagedModel.TotalPages,
            TotalItems = pagedModel.TotalItems,
            Items = pagedModel.Items.Select(director => new DirectorOutputGetAllDTO(director.Id, director.Name))
                .ToList()
        };
    }

    public async Task<Director> GetById(long id)
    {
        var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);

        if (director == null)
            throw new Exception("Diretor nao encontrado.");

        return director;
    }

    public async Task<Director> Create(Director director)
    {
        _context.Directors.Add(director);
        await _context.SaveChangesAsync();

        return director;
    }

    public async Task<Director> Update(Director director, long id)
    {
        director.Id = id;
        _context.Directors.Update(director);
        await _context.SaveChangesAsync();

        return director;
    }

    public async Task Delete(long id)
    {
        var director = await _context.Directors.FirstOrDefaultAsync(director => director.Id == id);
        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();
    }
}