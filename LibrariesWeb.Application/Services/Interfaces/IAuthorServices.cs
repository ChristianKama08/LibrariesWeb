using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Shared.RequestFeatures;

namespace LibrariesWeb.Application.Services.Interfaces
{
    public interface IAuthorServices
    {
        Task<AuthorDtos> GetAuthorByIdAsync(Guid authorId);
        Task<AuthorsResponse> GetAllAuthorAsync(AuthorParameters authorParameters);
        Task<AuthorDtos> AddAuthorAsync(AuthorRequest authorRequest);
        Task<AuthorDtos> UpdateAuthorAsync(Guid AuthorId, AuthorRequest authorRequest);
        Task<AuthorDtos?> DeleteAuthorAsync(Guid AuthorId);
    }
}
