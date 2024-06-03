using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Book>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BookBorrowingRequest>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BookCategory>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasKey(bd => new { bd.BookBorrowingRequestId, bd.BookId });

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasOne(bd => bd.BookBorrowingRequest)
                .WithMany(br => br.RequestDetails)
                .HasForeignKey(bd => bd.BookBorrowingRequestId);

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasOne(bd => bd.Book)
                .WithMany(b => b.BookBorrowingRequestDetails)
                .HasForeignKey(bd => bd.BookId);

            // Additional configurations (optional)
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
            });

            // Configure relationships for BookBorrowingRequest (optional)
            modelBuilder.Entity<BookBorrowingRequest>()
                .HasOne(br => br.Requestor)
                .WithMany()
                .HasForeignKey(br => br.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookBorrowingRequest>()
                .HasOne(br => br.Approver)
                .WithMany()
                .HasForeignKey(br => br.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Book>()
                .Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Book>()
                .Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookBorrowingRequest>()
                .Property(e => e.RequestorId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookBorrowingRequest>()
                .Property(e => e.DateRequested)
                .IsRequired();

            modelBuilder.Entity<BookBorrowingRequest>()
                .Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .Property(e => e.BookBorrowingRequestId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .Property(e => e.BookId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookCategory>()
                .Property(e => e.BookId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<BookCategory>()
                .Property(e => e.CategoryId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Category>()
                .Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(e => e.IsSuperUser)
                .IsRequired()
                .HasMaxLength(5);
        }
    }
}