using System.ComponentModel.DataAnnotations.Schema;

namespace LibrariesWeb.Domain.Entities
{
    public class Book
    {
        public required Guid bookId { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required DateTime PickOfDate { get; set; }
        public required DateTime ReturnDate { get; set; }

        [ForeignKey(nameof(Author))]
        public required Guid AuthorId { get; set; }
        public Author ?author { get; set; }
        public string? ImageUrl { get; set; }
        public List<IssuedBook>? IssuedBooks { get; set; }
}
}
