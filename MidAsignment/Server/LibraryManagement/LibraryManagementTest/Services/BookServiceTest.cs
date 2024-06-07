using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Services
{
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IBaseRepository<Book>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private BookService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBaseRepository<Book>>();
            _mockMapper = new Mock<IMapper>();
            _service = new BookService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", Author = "Author1" },
                new Book { Id = 2, Title = "Book2", Author = "Author2" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Book1"));
        }

        [Test]
        public async Task GetByIdAsync_Id_ReturnsBookDTO()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book1", Author = "Author1" };
            var bookDto = new BookDTO { Title = "Book1", Author = "Author1" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(book);
            _mockMapper.Setup(m => m.Map<BookDTO>(It.IsAny<Book>())).Returns(bookDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Book1"));
            Assert.That(result.Author, Is.EqualTo("Author1"));

        }

        [Test]
        public async Task CreateAsync_BookCreateDTO_AddsNewBook()
        {
            // Arrange
            var bookCreateDto = new BookCreateDTO { Title = "New Book", Author = "New Author" };
            var book = new Book { Id = 1, Title = "New Book", Author = "New Author" };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookCreateDTO>())).Returns(book);
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Book>()));

            // Act
            var result = await _service.CreateAsync(bookCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("New Book"));
        }

        [Test]
        public void DeleteAsync_Id_RemovesBook()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            // Act
            _service.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Test]
        public void UpdateAsync_BookCreateDTO_UpdatesBook()
        {
            // Arrange
            var bookCreateDto = new BookCreateDTO { Title = "Updated Book", Author = "Updated Author" };
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookCreateDTO>())).Returns(book);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>()));

            // Act
            _service.UpdateAsync(1, bookCreateDto);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Book>(b => b.Id == 1 && b.Title == "Updated Book")), Times.Once);
        }
    }
}
