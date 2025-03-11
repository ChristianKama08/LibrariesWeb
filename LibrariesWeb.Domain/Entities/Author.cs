namespace LibrariesWeb.Domain.Entities
{
    public class Author
    {
        public required Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Country { get; set; }
        public required DateTime BithDay { get; set; }
        public List<Book>? books { get; set; }
    }
}
