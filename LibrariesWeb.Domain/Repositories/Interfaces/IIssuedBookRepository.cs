using LibrariesWeb.Domain.Entities;

namespace LibrariesWeb.Domain.Repositories.Interfaces
{
    public interface IIssuedBookRepository
    {
        Task<IssuedBook?> GetIssuedBookByIdAsync(Guid issuedBookId);
        Task<List<IssuedBook>> GetIssuedBooksByUserIdAsync(Guid userId);
        Task AddIssuedBookAsync(IssuedBook issuedBook);
        Task DeleteIssuedBookAsync(IssuedBook issuedBook);
        //Task<AppUser?> GetUserByIdAsync(Guid userId); 
        Task<Book?> GetBookByIdAsync(Guid bookId);

    }
}
