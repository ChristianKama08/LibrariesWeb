namespace LibrariesWeb.Application.Request
{
    public class BookRequest
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required DateTime PickOfDate { get; set; }
        public required DateTime ReturnDate { get; set; }
        public required Guid AuthorId { get; set; }
    }
}
