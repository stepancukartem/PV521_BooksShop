using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL;
using PV521_BooksShop.DAL.Entities;
using PV521_BooksShop.DAL.Repositories;
using PV521_BookssShop.Dtos;

namespace PV521_BookssShop.Services
{
    public class GenreService
    {
        private readonly GenreRepostiory _genreRepository;
        private readonly AppDbContext _context;

        public GenreService(
            GenreRepostiory genreRepository,
            AppDbContext context)
        {
            _genreRepository = genreRepository;
            _context = context;
        }

        public async Task<List<GenreDto>> GetAll()
        {
            var genres = await _genreRepository.Genres.ToListAsync();

            return genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name,
                BookIds = g.Books.Select(b => b.Id).ToList()
            }).ToList();
        }

        public async Task<GenreDto?> GetById(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
                return null;

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                BookIds = genre.Books.Select(b => b.Id).ToList()
            };
        }

        public async Task<GenreDto?> Create(CreateGenreDto dto)
        {
            var books = await _context.Books
                .Where(b => dto.BookIds.Contains(b.Id))
                .ToListAsync();

            var genre = new Genre
            {
                Name = dto.Name,
                Books = books
            };

            var created = await _genreRepository.CreateAsync(genre);

            if (!created)
                return null;

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                BookIds = genre.Books.Select(b => b.Id).ToList()
            };
        }

        public async Task<GenreDto?> Update(
            int id,
            UpdateGenreDto dto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
                return null;

            var books = await _context.Books
                .Where(b => dto.BookIds.Contains(b.Id))
                .ToListAsync();

            genre.Name = dto.Name;
            genre.Books = books;

            var updated = await _genreRepository.UpdateAsync(genre);

            if (!updated)
                return null;

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                BookIds = genre.Books.Select(b => b.Id).ToList()
            };
        }

        public async Task<bool> Delete(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
                return false;

            return await _genreRepository.DeleteAsync(genre);
        }
    }
}