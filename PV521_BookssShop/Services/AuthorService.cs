using PV521_BookssShop.Dtos;
using PV521_BooksShop.DAL.Entities;
using PV521_BooksShop.DAL.Repositories;

namespace PV521_BookssShop.Services
{
    public class AuthorService
    {
        private readonly AuthorRepostiory _authorRepository;
        private readonly ImageService _imageService;

        public AuthorService(
            AuthorRepostiory authorRepository,
            ImageService imageService)
        {
            _authorRepository = authorRepository;
            _imageService = imageService;
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
            var imagePath = await _imageService.SaveImageAsync(dto.Image);

            var author = new Author
            {
                Name = dto.Name,
                Biography = dto.Biography,
                Image = imagePath,
                Country = dto.Country,
                BirthDate = dto.BirthDate
            };

            var created = await _authorRepository.CreateAsync(author);

            if (!created)
            {
                _imageService.DeleteImage(imagePath);
                return null;
            }

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

        public async Task<AuthorDto?> Update(
            int id,
            UpdateAuthorDto dto)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return null;

            string? oldImage = author.Image;

            if (dto.Image != null)
            {
                author.Image =
                    await _imageService.SaveImageAsync(dto.Image);
            }

            author.Name = dto.Name;
            author.Biography = dto.Biography;
            author.Country = dto.Country;
            author.BirthDate = dto.BirthDate;

            var updated = await _authorRepository.UpdateAsync(author);

            if (!updated)
                return null;

            if (dto.Image != null && oldImage != null)
                _imageService.DeleteImage(oldImage);

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
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return false;

            string? image = author.Image;

            var deleted = await _authorRepository.DeleteAsync(author);

            if (deleted)
                _imageService.DeleteImage(image);

            return deleted;
        }
    }
}