using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrariesWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuedBookController : ControllerBase
    {

        [ApiController]
        [Route("api/issuedbook")]
        public class IssuedBooksController : ControllerBase
        {
            private readonly IIssuedBookService _issuedBookService;

            public IssuedBooksController(IIssuedBookService issuedBookService)
            {
                _issuedBookService = issuedBookService;
            }

            // POST: api/IssuedBooks/issue
            //[HttpPost("issue")]
            //[Authorize(Policy = "UserOnly")]
            //public async Task<IActionResult> IssueBookToUser([FromBody] IssuedBookRequest request)
            //{
            //    if (request == null || request.UserId == Guid.Empty || request.BookId == Guid.Empty)
            //    {
            //        return BadRequest("Invalid request.");
            //    }

            //    try
            //    {
            //        var issuedBook = await _issuedBookService.IssueBookToUserAsync(request.UserId, request.BookId);
            //        return CreatedAtAction(nameof(GetIssuedBooksByUserId), new { userId = request.UserId }, issuedBook);
            //    }
            //    catch (NotFoundException ex)
            //    {
            //        return NotFound(ex.Message);
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest(ex.Message);
            //    }
            //}

            // GET: api/IssuedBooks/user/{userId}
            [HttpGet("user/{userId}")]
            [Authorize(Policy = "UserOnly")]
            public async Task<IActionResult> GetIssuedBooksByUserId(Guid userId)
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest("Invalid user ID.");
                }

                var issuedBooks = await _issuedBookService.GetIssuedBooksByUserIdAsync(userId);
                return Ok(issuedBooks);
            }

            // DELETE: api/IssuedBooks/return/{issuedBookId}
            [HttpDelete("return/{issuedBookId}")]
            [Authorize(Policy = "RequireUserRole")] // Adjust the policy as needed
            public async Task<IActionResult> ReturnIssuedBook(Guid issuedBookId)
            {
                if (issuedBookId == Guid.Empty)
                {
                    return BadRequest("Invalid issued book ID.");
                }

                try
                {
                    await _issuedBookService.ReturnIssuedBookAsync(issuedBookId);
                    return NoContent(); // 204 No Content
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
