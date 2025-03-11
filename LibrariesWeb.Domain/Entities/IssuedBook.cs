namespace LibrariesWeb.Domain.Entities
{
    public class IssuedBook
    {
        public required Guid IssuedBookId { get; set; } 
        public required Guid UserId { get; set; } 
        public required Guid BookId { get; set; } 
        public DateTime IssueDate { get; set; } 
        public DateTime ReturnDate { get; set; }
        public AppUser? User { get; set; } 
        public Book? Book { get; set; } 
    }
}
