namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    }
}
