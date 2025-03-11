namespace LibrariesWeb.Application.Dtos;

public class AuthorDtos
{
    public  Guid authorId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Country { get; set; }
    public required DateTime BithDay { get; set; }
}
