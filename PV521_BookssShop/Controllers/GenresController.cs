using Microsoft.AspNetCore.Mvc;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Services;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenresController(GenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAll()
        {
            var genres = await _genreService.GetAll();

            return Ok(genres);
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(
            CreateGenreDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Genre name is required.");

            var genre = await _genreService.Create(dto);

            if (genre == null)
                return BadRequest("Genre was not created.");

            return Ok(genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenreDto>> Update(
            int id,
            UpdateGenreDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Genre name is required.");

            var genre = await _genreService.Update(id, dto);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _genreService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}