using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping for User
        CreateMap<UserCreateDTO, User>();
        CreateMap<User, UserDTO>();

        // Mapping for Book
        CreateMap<BookCreateDTO, Book>();
        CreateMap<Book, BookDTO>();

        // Mapping for Category
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<Category, CategoryDTO>();

        // Mapping for BookCategory
        CreateMap<BookCategoryCreateDTO, BookCategory>();
        CreateMap<BookCategory, BookCategoryDTO>();

        // Mapping for BookBorrowingRequest
        CreateMap<BookBorrowingRequestCreateDTO, BookBorrowingRequest>();
        CreateMap<BookBorrowingRequest, BookBorrowingRequestDTO>();

        // Mapping for BookBorrowingRequestDetails
        CreateMap<BookBorrowingRequestDetailsCreateDTO, BookBorrowingRequestDetails>();
        CreateMap<BookBorrowingRequestDetails, BookBorrowingRequestDetailsDTO>();
    }
}
