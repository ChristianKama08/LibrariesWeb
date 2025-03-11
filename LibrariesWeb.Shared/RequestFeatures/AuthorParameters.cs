namespace LibrariesWeb.Shared.RequestFeatures;

public class AuthorParameters : RequestParameters
{
    public Guid? AuthorId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Country { get; set; }
}