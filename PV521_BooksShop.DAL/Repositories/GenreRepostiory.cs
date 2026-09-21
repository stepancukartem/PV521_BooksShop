using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;

namespace PV521_BooksShop.DAL.Repositories
{
    public class GenreRepostiory
    {
        private readonly AppDbContext _context;

        public GenreRepostiory(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Genre> Genres =>
            _context.Genres
                .Include(g => g.Books)
                .AsNoTracking();

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> CreateAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Genre genre)
        {
            _context.Genres.Remove(genre);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
