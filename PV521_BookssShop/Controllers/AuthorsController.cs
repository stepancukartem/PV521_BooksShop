using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL.Entities;
using PV521_BooksShop.DAL.Repositories;

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

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.Authors.ToListAsync();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                return BadRequest("Name is required.");

            var created = await _authorRepository.CreateAsync(author);

            if (!created)
                return BadRequest();

            return Ok(author);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
                return BadRequest("ID in URL and body must match.");

            var existing = await _authorRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            var updated = await _authorRepository.UpdateAsync(author);

            if (!updated)
                return BadRequest();

            return Ok(author);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
