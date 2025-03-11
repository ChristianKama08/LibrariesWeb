using LibrariesWeb.Domain.Entities;

namespace LibrariesWeb.Application.Services.Interfaces;

public interface IIssuedBookService
{
    //Task<IssuedBook> IssueBookToUserAsync(Guid userId, Guid bookId);
    Task<List<IssuedBook>> GetIssuedBooksByUserIdAsync(Guid userId);
    Task ReturnIssuedBookAsync(Guid issuedBookId);
}