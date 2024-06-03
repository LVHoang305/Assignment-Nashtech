namespace LibraryManagement.Models
{
    public class BookBorrowingRequest
    {
        public int Id { get; set; }
        public int RequestorId { get; set; }
        public User Requestor { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateReturned { get; set; }
        public string Status { get; set; } // Approved/Rejected/Waiting
        public int? ApproverId { get; set; }
        public User Approver { get; set; }
        public ICollection<BookBorrowingRequestDetails> RequestDetails { get; set; }
    }
}
