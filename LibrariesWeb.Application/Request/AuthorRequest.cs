namespace LibrariesWeb.Application.Request
{
    public class AuthorRequest
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Country { get; set; }
        public required DateTime BirthDay { get; set; }

    }
}
