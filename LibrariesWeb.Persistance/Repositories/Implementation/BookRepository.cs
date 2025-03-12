using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Domain.Repositories.Interfaces;
using LibrariesWeb.Persistance.Data;

using Microsoft.EntityFrameworkCore;

namespace LibrariesWeb.Persistance.Repositories.Implementation
{
    public class BookRepository:IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddImageToBooks(Guid bookId, string imageUrl)
        {
            var book = await GetBookByIdAsync(bookId);
            if (book != null)
            {
                book.ImageUrl = imageUrl; 
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(a => a.author)
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorIdAsync(Guid authorId)
        {
            return await _context.Authors.FirstOrDefaultAsync(s =>s.AuthorId.Equals(authorId));
        }

        public async Task<Book?> GetBookByIdAsync(Guid Id)
        {
            return await _context.Books.FirstOrDefaultAsync(a =>a.bookId.Equals(Id));
        }

        public async Task<Book?> GetBookByISBNAsync(string ISBN)
        {
          return   await _context.Books.FirstOrDefaultAsync(a => a.ISBN == ISBN);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
