namespace LibraryManagement.Models
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
