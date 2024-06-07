using System;
namespace LibraryManagement.Models.CreateDTOs
{
	public class BookBorrowingRequestDetailsCreateDTO
	{
        public int BookBorrowingRequestId { get; set; }
        public int BookId { get; set; }
    }
}

