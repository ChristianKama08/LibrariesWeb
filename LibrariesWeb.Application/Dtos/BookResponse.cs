using LibrariesWeb.Shared.RequestFeatures;

namespace LibrariesWeb.Application.Dtos;

public record BookResponse(IEnumerable<BookDtos> BookDtos, MetaData MetaData);

