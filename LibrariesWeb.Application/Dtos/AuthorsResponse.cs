using LibrariesWeb.Shared.RequestFeatures;

namespace LibrariesWeb.Application.Dtos;

public record AuthorsResponse(IEnumerable<AuthorDtos> Authors, MetaData MetaData);