using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Shared.RequestFeatures;

namespace LibrariesWeb.Domain.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorById(Guid authorId);
        Task<PagedList<Author>> GetAllAuthors(AuthorParameters parameters);
        Task AddAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
    }
}