using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Domain.Repositories.Interfaces;
using LibrariesWeb.Persistance.Data;
using LibrariesWeb.Persistance.Repositories.Extensions;
using LibrariesWeb.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace LibrariesWeb.Persistance.Repositories.Implementation
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddAuthorAsync(Author author)
        {
           _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<Author>> GetAllAuthors(AuthorParameters authorParameters)
        {
            var authors = _context.Authors.AsQueryable();

            if (authorParameters.AuthorId.HasValue ||
                !string.IsNullOrWhiteSpace(authorParameters.Name) ||
                !string.IsNullOrWhiteSpace(authorParameters.Surname) ||
                !string.IsNullOrWhiteSpace(authorParameters.Country))
            {
                authors = authors.ApplyAuthorFilters(authorParameters.AuthorId, authorParameters.Name, authorParameters.Surname, authorParameters.Country);
            }

            var count = await authors.CountAsync();

            var pagedAuthors = await authors
                .Skip((authorParameters.PageNumber - 1) * authorParameters.PageSize)
                .Take(authorParameters.PageSize)
                .ToListAsync();

            return new PagedList<Author>(pagedAuthors, count, authorParameters.PageNumber, authorParameters.PageSize);
        }

        public async Task<Author?> GetAuthorById(Guid Id)
        {
            return await _context.Authors.FirstOrDefaultAsync(o => o.AuthorId.Equals(Id));
        }

        public async Task UpdateAuthorAsync(Author author)
        {
           _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
