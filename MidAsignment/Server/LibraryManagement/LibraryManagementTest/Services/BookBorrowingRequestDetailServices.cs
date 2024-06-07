using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Services
{
    [TestFixture]
    public class BookBorrowingRequestDetailsServiceTests
    {
        private Mock<IBaseRepository<BookBorrowingRequestDetails>> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private BookBorrowingRequestDetailsService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IBaseRepository<BookBorrowingRequestDetails>>();
            _mapperMock = new Mock<IMapper>();
            _service = new BookBorrowingRequestDetailsService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateAsync_BookBorrowingRequestDetailsCreateDTO_AddsEntity()
        {
            // Arrange
            var requestDetailsCreateDTO = new BookBorrowingRequestDetailsCreateDTO();
            var requestDetails = new BookBorrowingRequestDetails();
            _mapperMock.Setup(m => m.Map<BookBorrowingRequestDetails>(requestDetailsCreateDTO)).Returns(requestDetails);

            // Act
            await _service.CreateAsync(requestDetailsCreateDTO);

            // Assert
            _repositoryMock.Verify(r => r.CreateAsync(requestDetails), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_BookBorrowingRequestDetailsCreateDTO_UpdatesEntity()
        {
            // Arrange
            int id = 0;
            var requestDetailsUpdateDTO = new BookBorrowingRequestDetailsCreateDTO();
            var requestDetails = new BookBorrowingRequestDetails();
            _mapperMock.Setup(m => m.Map<BookBorrowingRequestDetails>(requestDetailsUpdateDTO)).Returns(requestDetails);

            // Act
            _service.UpdateAsync(id, requestDetailsUpdateDTO);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(requestDetails), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllEntities()
        {
            // Arrange
            var expectedEntities = new List<BookBorrowingRequestDetails>
            {
                new BookBorrowingRequestDetails { Id = 1 },
                new BookBorrowingRequestDetails { Id = 2 }
            };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedEntities);

            // Act
            var actualEntities = await _service.GetAllAsync();

            // Assert
            Assert.That(actualEntities.Count(), Is.EqualTo(expectedEntities.Count));
            Assert.IsTrue(actualEntities.All(e => expectedEntities.Any(expected => expected.Id == e.Id)));
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

    }
}
