namespace LibraryManagement.Models
{
    public class BookBorrowingRequestDetails
    {
        public int Id { get; set; }
        public int BookBorrowingRequestId { get; set; }
        public virtual BookBorrowingRequest BookBorrowingRequest { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
