using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;

namespace PV521_BooksShop.DAL.Repositories
{
    public class RoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Role> Roles =>
            _context.Roles.AsNoTracking();

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<bool> CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}