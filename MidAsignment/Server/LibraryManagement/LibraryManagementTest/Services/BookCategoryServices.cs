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
    public class BookCategoryServiceTests
    {
        private Mock<IBaseRepository<BookCategory>> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private BookCategoryService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IBaseRepository<BookCategory>>();
            _mapperMock = new Mock<IMapper>();
            _service = new BookCategoryService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public void DeleteAsync_Id_CallsRepositoryDeleteAsync()
        {
            // Arrange
            int id = 1;

            // Act
            _service.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Test]
        public void DeleteByFieldAsync_FieldNameAndValue_CallsRepositoryDeleteByFieldAsync()
        {
            // Arrange
            string fieldName = "BookId";
            string fieldValue = "1";

            // Act
            _service.DeleteByFieldAsync(fieldName, fieldValue);

            // Assert
            _repositoryMock.Verify(r => r.DeleteByFieldAsync(fieldName, fieldValue), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllEntities()
        {
            // Arrange
            var bookCategories = new List<BookCategory> { new BookCategory(), new BookCategory() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(bookCategories);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task CreateAsync_BookCategoryCreateDTO_AddsEntity()
        {
            // Arrange
            var bookCategoryCreateDTO = new BookCategoryCreateDTO { BookId = 1, CategoryId = 1 };
            var bookCategory = new BookCategory { BookId = 1, CategoryId = 1 };
            _mapperMock.Setup(m => m.Map<BookCategory>(bookCategoryCreateDTO)).Returns(bookCategory);

            // Act
            var result = await _service.CreateAsync(bookCategoryCreateDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.BookId, Is.EqualTo(bookCategory.BookId));
            _repositoryMock.Verify(r => r.CreateAsync(bookCategory), Times.Once);
        }

        [Test]
        public void UpdateAsync_BookCategoryCreateDTO_UpdatesEntity()
        {
            // Arrange
            int id = 1;
            var bookCategoryCreateDTO = new BookCategoryCreateDTO { BookId = 1, CategoryId=1 };
            var bookCategory = new BookCategory { BookId = 1, CategoryId = 2 };
            _mapperMock.Setup(m => m.Map<BookCategory>(bookCategoryCreateDTO)).Returns(bookCategory);

            // Act
            _service.UpdateAsync(id, bookCategoryCreateDTO);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(bookCategory), Times.Once);
        }
    }
}
