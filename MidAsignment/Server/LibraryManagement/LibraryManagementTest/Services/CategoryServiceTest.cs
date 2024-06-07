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
    public class CategoryServiceTests
    {
        private Mock<IBaseRepository<Category>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private CategoryService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBaseRepository<Category>>();
            _mockMapper = new Mock<IMapper>();
            _service = new CategoryService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Category1"));
        }

        [Test]
        public async Task GetByIdAsync_Id_ReturnsCategoryDTO()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category1" };
            var categoryDto = new CategoryDTO { Id = 1, Name = "Category1" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<CategoryDTO>(It.IsAny<Category>())).Returns(categoryDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Category1"));
        }

        [Test]
        public async Task CreateAsync_CategoryCreateDTO_AddsNewCategory()
        {
            // Arrange
            var categoryCreateDto = new CategoryCreateDTO { Name = "NewCategory" };
            var category = new Category { Id = 1, Name = "NewCategory" };

            _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CategoryCreateDTO>())).Returns(category);
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Category>()));

            // Act
            var result = await _service.CreateAsync(categoryCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("NewCategory"));
        }

        [Test]
        public void DeleteAsync_Id_RemovesCategory()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            // Act
            _service.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Test]
        public void UpdateAsync_CategoryCreateDTO_UpdatesCategory()
        {
            // Arrange
            var categoryCreateDto = new CategoryCreateDTO { Name = "UpdatedCategory" };
            var category = new Category { Id = 1, Name = "UpdatedCategory" };

            _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CategoryCreateDTO>())).Returns(category);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>()));

            // Act
            _service.UpdateAsync(1, categoryCreateDto);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Category>(c => c.Id == 1 && c.Name == "UpdatedCategory")), Times.Once);
        }
    }
}
