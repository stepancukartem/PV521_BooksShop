using PV521_BookssShop.Dtos;
using PV521_BooksShop.DAL.Entities;
using PV521_BooksShop.DAL.Repositories;

namespace PV521_BookssShop.Services
{
    public class AuthorService
    {
        private readonly AuthorRepostiory _authorRepository;

        public AuthorService(AuthorRepostiory authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            var authors = _authorRepository.Authors.ToList();

            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                Image = a.Image,
                Country = a.Country,
                BirthDate = a.BirthDate
            }).ToList();
        }

        public async Task<AuthorDto?> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };
        }

        public async Task<AuthorDto?> Create(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                Biography = dto.Biography,
                Image = dto.Image,
                Country = dto.Country,
                BirthDate = dto.BirthDate
            };

            var created = await _authorRepository.CreateAsync(author);

            if (!created)
                return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };
        }

        public async Task<AuthorDto?> Update(int id, UpdateAuthorDto dto)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return null;

            author.Name = dto.Name;
            author.Biography = dto.Biography;
            author.Image = dto.Image;
            author.Country = dto.Country;
            author.BirthDate = dto.BirthDate;

            var updated = await _authorRepository.UpdateAsync(author);

            if (!updated)
                return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };
        }

        public async Task<bool> Delete(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }
    }
}