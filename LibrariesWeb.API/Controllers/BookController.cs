using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrariesWeb.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookServices _services;

        public BookController(IBookServices services)
        {
            _services = services;
        }

        [HttpGet("book/{bookId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookByIdAsync(Guid bookId)
        {
            var book = await _services.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBookAsync(BookRequest bookRequest)
        {
            try
            {
                var book = await _services.AddBookAsync(bookRequest);

               return CreatedAtAction(nameof(GetBookByIdAsync), new { bookId = book.bookId}, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("book/{bookId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBookAsync(Guid bookId)
        {
            await _services.DeleteBookAsync(bookId);

            return NoContent();
        }

        [HttpPut("book/{bookId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBookAsync(Guid bookId, BookRequest bookRequest)
        {
            await _services.UpdateBookAsync(bookId, bookRequest);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBookAsync()
        {
            var book = await _services.GetAllBookAsync();

            return Ok(book);
        }

        [HttpGet("ISBN/{ISBN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookByISBNAsync(string ISBN)
        {
            try
            {
                var book = await _services.GetBookByISBNAsync(ISBN);
                return Ok(book);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("book/{bookId:guid}/image")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddImageToBook(Guid bookId, IFormFile imageFile)
        {
            try
            {
                await _services.AddImageToBookAsync(bookId, imageFile);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
