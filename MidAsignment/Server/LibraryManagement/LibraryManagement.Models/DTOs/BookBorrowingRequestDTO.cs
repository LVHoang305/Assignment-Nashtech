namespace LibraryManagement.Models
{
    public class BookBorrowingRequestDTO
    {
        public int RequestorId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateReturned { get; set; }
        public string Status { get; set; }
        public int? ApproverId { get; set; }
        public List<BookBorrowingRequestDetailsDTO> RequestDetails { get; set; }
    }
}
