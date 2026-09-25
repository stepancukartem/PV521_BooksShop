using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;

namespace PV521_BooksShop.DAL
{  
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>(e =>
            {
                e.HasKey(r => r.Id);

                e.Property(r => r.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                e.HasIndex(r => r.Name)
                    .IsUnique();
            });
            base.OnModelCreating(builder);

            builder.Entity<Book>(e =>
            {
                e.HasKey(b => b.Id);

                e.Property(b => b.Title)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(b => b.Description)
                    .HasColumnType("text");

                e.Property(b => b.Image)
                    .HasMaxLength(100);
            });

            builder.Entity<Author>(e =>
            {
                e.HasKey(a => a.Id);

                e.Property(a => a.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(a => a.Biography)
                    .HasColumnType("text");

                e.Property(a => a.Image)
                    .HasMaxLength(100);

                e.Property(a => a.Country)
                    .HasMaxLength(255);
            });

            builder.Entity<Genre>(e =>
            {
                e.HasKey(g => g.Id);

                e.Property(g => g.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(j => j.ToTable("BookGenres"));
            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.Property(u => u.Email)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(u => u.Password)
                    .IsRequired();

                e.HasIndex(u => u.Email)
                    .IsUnique();
            });
        }
    }
}