using Microsoft.AspNetCore.Mvc;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Services;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            var authors = await _authorService.GetAll();

            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorService.GetById(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(
            [FromForm] CreateAuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Author name is required.");

            var author = await _authorService.Create(dto);

            if (author == null)
                return BadRequest("Author was not created.");

            return CreatedAtAction(
                nameof(GetById),
                new { id = author.Id },
                author);
        }

        // PUT: api/Authors/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AuthorDto>> Update(
            int id,
            [FromForm] UpdateAuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Author name is required.");

            var author = await _authorService.Update(id, dto);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}