using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;
using PV521_BooksShop.DAL.Repositories;
using PV521_BookssShop.Dtos;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepostiory _authorRepository;

        public AuthorsController(AuthorRepostiory authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _authorRepository.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    Image = a.Image,
                    Country = a.Country,
                    BirthDate = a.BirthDate
                })
                .ToListAsync();

            return Ok(authors);
        }

        // GET: api/authors/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound();

            var dto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };

            return Ok(dto);
        }

        // GET: api/authors/5/books
        [HttpGet("{id:int}/books")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound("Author not found.");

            var books = await _authorRepository
                .GetBooksByAuthorAsync(id);

            var result = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image,
                Rating = b.Rating,
                Price = b.Price,
                Pages = b.Pages,
                Year = b.Year
            });

            return Ok(result);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(
            CreateAuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Author name is required.");

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
                return BadRequest("Author was not created.");

            var result = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT: api/authors/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AuthorDto>> Update(
            int id,
            UpdateAuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Author name is required.");

            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound();

            author.Name = dto.Name;
            author.Biography = dto.Biography;
            author.Image = dto.Image;
            author.Country = dto.Country;
            author.BirthDate = dto.BirthDate;

            var updated = await _authorRepository.UpdateAsync(author);

            if (!updated)
                return BadRequest("Author was not updated.");

            var result = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                Image = author.Image,
                Country = author.Country,
                BirthDate = author.BirthDate
            };

            return Ok(result);
        }

        // DELETE: api/authors/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}