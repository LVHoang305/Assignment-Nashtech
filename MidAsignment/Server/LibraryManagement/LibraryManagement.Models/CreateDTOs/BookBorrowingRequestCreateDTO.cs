using LibraryManagement.Models;

namespace LibraryManagement.Models.CreateDTOs
{
	public class BookBorrowingRequestCreateDTO
	{
        public DateTime DateRequested { get; set; }
        public DateTime DateReturned { get; set; }
        public string Status { get; set; }
        public int? ApproverId { get; set; }
    }
}

