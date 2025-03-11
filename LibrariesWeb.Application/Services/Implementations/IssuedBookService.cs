using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Domain.Repositories.Interfaces;

namespace LibrariesWeb.Application.Services.Implementations
{
     public class IssuedBookService : IIssuedBookService
     {
        private readonly IIssuedBookRepository _repository;
       
        public IssuedBookService(IIssuedBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IssuedBook>> GetIssuedBooksByUserIdAsync(Guid userId)
        {
            return await _repository.GetIssuedBooksByUserIdAsync(userId); 
        }

        //public async Task<IssuedBook> IssueBookToUserAsync(Guid userId, Guid bookId)
        //{
        //    var user = await _repository.GetUserByIdAsync(userId);
        //    var book = await _repository.GetBookByIdAsync(bookId);

        //    if (user == null || book == null)   
        //    {
        //        throw new NotFoundException("User or Book not found.");
        //    }

        //    var issuedBook = new IssuedBook
        //    {
        //        IssuedBookId = Guid.NewGuid(),
        //        UserId = userId,
        //        BookId = bookId,
        //        IssueDate = DateTime.UtcNow,
        //        ReturnDate = DateTime.UtcNow.AddDays(14) 
        //    };

        //    await _repository.AddIssuedBookAsync(issuedBook);
        //    return issuedBook;
        //}

        public async Task ReturnIssuedBookAsync(Guid issuedBookId)
        {
            var issuedBook = await _repository.GetIssuedBookByIdAsync(issuedBookId);
            if (issuedBook == null)
            {
                throw new NotFoundException("Issued book not found.");
            }

            await _repository.DeleteIssuedBookAsync(issuedBook);
        }
    }
}
