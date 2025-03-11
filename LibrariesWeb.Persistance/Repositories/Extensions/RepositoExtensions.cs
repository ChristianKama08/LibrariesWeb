using LibrariesWeb.Domain.Entities;

namespace LibrariesWeb.Persistance.Repositories.Extensions;

public static class RepositoExtensions
{
    public static IQueryable<Author> ApplyAuthorFilters(this IQueryable<Author> authors, Guid? authorId, string name, string surname, string country)
    {
        if (authorId.HasValue)
            authors = authors.Where(a => a.AuthorId == authorId.Value);

        if (!string.IsNullOrWhiteSpace(name))
            authors = authors.Where(a => a.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(surname))
            authors = authors.Where(a => a.Surname.Contains(surname));

        if (!string.IsNullOrWhiteSpace(country))
            authors = authors.Where(a => a.Country.Contains(country));

        return authors;
    }
}