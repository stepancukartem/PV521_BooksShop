using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;

namespace PV521_BooksShop.DAL
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            // Створюємо ролі
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == "admin");

            if (adminRole == null)
            {
                adminRole = new Role
                {
                    Id = 1,
                    Name = "admin"
                };

                await context.Roles.AddAsync(adminRole);
            }

            var userRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == "user");

            if (userRole == null)
            {
                userRole = new Role
                {
                    Id = 2,
                    Name = "user"
                };

                await context.Roles.AddAsync(userRole);
            }

            await context.SaveChangesAsync();

            // Створюємо тестового admin
            var adminEmail = "admin@gmail.com";

            var adminUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    Email = adminEmail,
                    Password = "Admin123!",
                    RoleId = adminRole.Id
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}