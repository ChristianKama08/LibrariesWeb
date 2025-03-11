namespace LibrariesWeb.Application.Request
{
    public class IssuedBookRequest
    {
        public required Guid UserId { get; set; }
        public required Guid BookId { get; set; }
    }
}
