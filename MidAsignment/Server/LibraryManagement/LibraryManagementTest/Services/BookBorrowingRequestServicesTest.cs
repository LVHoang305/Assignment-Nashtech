using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Services
{
    [TestFixture]
    public class BookBorrowingRequestServiceTests
    {
        private Mock<IBaseRepository<BookBorrowingRequest>> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private BookBorrowingRequestService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IBaseRepository<BookBorrowingRequest>>();
            _mapperMock = new Mock<IMapper>();
            _service = new BookBorrowingRequestService(_repositoryMock.Object, _mapperMock.Object);
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
        public async Task GetAllAsync_NoInput_ReturnsAllEntities()
        {
            // Arrange
            var requests = new List<BookBorrowingRequest> { new BookBorrowingRequest(), new BookBorrowingRequest() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(requests);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetByFieldAsync_FieldNameAndValue_ReturnsEntities()
        {
            // Arrange
            string json = "{\"fieldName\": \"fieldValue\"}";
            var requests = new List<BookBorrowingRequest> { new BookBorrowingRequest(), new BookBorrowingRequest() };
            _repositoryMock.Setup(r => r.GetByFieldsAsync(json)).ReturnsAsync(requests);

            // Act
            var result = await _service.GetByFieldAsync(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            _repositoryMock.Verify(r => r.GetByFieldsAsync(json), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_Id_ReturnsEntity()
        {
            // Arrange
            int id = 1;
            var request = new BookBorrowingRequest { RequestorId = id };
            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(request);
            _mapperMock.Setup(m => m.Map<BookBorrowingRequestDTO>(request)).Returns(new BookBorrowingRequestDTO { RequestorId = id });

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.RequestorId, Is.EqualTo(id));
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Test]
        public async Task GetRequestsForCurrentMonthAsync_RequestorId_ReturnsRequestsForCurrentMonth()
        {
            // Arrange
            int userId = 1;
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;

            var requests = new List<BookBorrowingRequest>
            {
                new BookBorrowingRequest { DateRequested = currentDate, RequestorId = userId },
                new BookBorrowingRequest { DateRequested = currentDate.AddMonths(-1), RequestorId = userId },
                new BookBorrowingRequest { DateRequested = currentDate.AddYears(-1), RequestorId = userId },
                new BookBorrowingRequest { DateRequested = currentDate.AddMonths(1), RequestorId = userId },
                new BookBorrowingRequest { DateRequested = currentDate.AddMonths(-1), RequestorId = userId + 1 }
            };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(requests);
            _mapperMock.Setup(m => m.Map<List<BookBorrowingRequestDTO>>(It.IsAny<List<BookBorrowingRequest>>())).Returns((List<BookBorrowingRequest> reqs) => reqs.Select(r => new BookBorrowingRequestDTO { RequestorId = r.RequestorId }).ToList());

            // Act
            var result = await _service.GetRequestsForCurrentMonthAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].RequestorId, Is.EqualTo(1));

        }

        [Test]
        public async Task CreateAsync_BookBorrowingRequestCreateDTO_AddsEntity()
        {
            // Arrange
            var requestCreateDTO = new BookBorrowingRequestCreateDTO();
            var request = new BookBorrowingRequest();
            _mapperMock.Setup(m => m.Map<BookBorrowingRequest>(requestCreateDTO)).Returns(request);

            // Act
            await _service.CreateAsync(requestCreateDTO);

            // Assert
            _repositoryMock.Verify(r => r.CreateAsync(request), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_BookBorrowingRequestCreateDTO_UpdatesEntity()
        {
            // Arrange
            int id = 1;
            var requestUpdateDTO = new BookBorrowingRequestCreateDTO();
            var request = new BookBorrowingRequest();
            _mapperMock.Setup(m => m.Map<BookBorrowingRequest>(requestUpdateDTO)).Returns(request);

            // Act
            _service.UpdateAsync(id, requestUpdateDTO);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(request), Times.Once);
        }
    }
}