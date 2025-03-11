using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Request;
using Microsoft.AspNetCore.Http;

namespace LibrariesWeb.Application.Services.Interfaces
{
    public interface IBookServices
    {
        Task<BookDtos> GetBookByIdAsync(Guid bookId);
        Task<List<BookDtos>> GetAllBookAsync();
        Task<BookDtos> AddBookAsync(BookRequest bookRequest);
        Task<BookDtos> UpdateBookAsync(Guid bookId, BookRequest bookRequest);
        Task<BookDtos> DeleteBookAsync(Guid bookId);
        Task<BookDtos> GetBookByISBNAsync(string ISBN);
        Task AddImageToBookAsync(Guid bookId, IFormFile ImageFile);

    }
}
