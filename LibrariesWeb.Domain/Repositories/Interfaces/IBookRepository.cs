using LibrariesWeb.Domain.Entities;

namespace LibrariesWeb.Domain.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetBookByIdAsync(Guid bookId);
        Task<Author?> GetAuthorIdAsync(Guid authorId);
        Task <Book?>GetBookByISBNAsync(string ISBN);
        Task<List<Book>> GetAllBooks();
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task GetBookByUser();
        Task AddImageToBooks(Guid bookId, string imageUrl);
    }
}
