using LibrariesWeb.Application.Request;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Shared.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibrariesWeb.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _services;

        public AuthorController(IAuthorServices services)
        {
            _services = services;
        }

        [HttpGet("author/{authorId:guid}")]
        [Authorize(Policy = "AdminOrUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthorByIdAsync(Guid authorId)
        {
            var auth = await _services.GetAuthorByIdAsync(authorId);

            return Ok(auth);
        }

        [HttpPost("author")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthorAsync(AuthorRequest authorRequest)
        {
            var auths = await _services.AddAuthorAsync(authorRequest);

            return Created(nameof(CreateAuthorAsync), auths);
        }

        [HttpDelete("author/{authorId:guid}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthorAsync(Guid authorId)
        {
            await _services.DeleteAuthorAsync(authorId);

            return NoContent();
        }

        [HttpPut("author/{authorId:guid}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAuthorAsync(Guid authorId, AuthorRequest authorRequest)
        {
            await _services.UpdateAuthorAsync(authorId, authorRequest);

            return NoContent();
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAuthorAsync([FromQuery] AuthorParameters parameters)
        {
            var authors = await _services.GetAllAuthorAsync(parameters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(authors.MetaData));

            return Ok(authors.Authors);
        }
    }
}
