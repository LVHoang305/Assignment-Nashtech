using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IBaseRepository<User>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private UserService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBaseRepository<User>>();
            _mockMapper = new Mock<IMapper>();
            _service = new UserService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", IsSuperUser = false },
                new User { Id = 2, Email = "user2@example.com", IsSuperUser = false }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.First().Email, Is.EqualTo("user1@example.com"));
            Assert.That(result.Count(), Is.EqualTo(2));

        }

        [Test]
        public async Task GetByIdAsync_Id_ReturnsUserDTO()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", IsSuperUser = false };
            var userDto = new UserDTO { Email = "user1@example.com", IsSuperUser = false };

            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDTO>(It.IsAny<User>())).Returns(userDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo("user1@example.com"));
            Assert.That(result.IsSuperUser, Is.EqualTo(false));

        }

        [Test]
        public async Task GetByEmail_Email_ReturnsUser()
        {
            // Arrange
            var email = "user1@example.com";
            var user = new User { Id = 1, Email = email, IsSuperUser = false };
            var users = new List<User> { user };

            _mockRepository.Setup(repo => repo.GetByFieldsAsync(It.IsAny<string>())).ReturnsAsync(users);

            // Act
            var result = await _service.GetByEmail(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(email));
        }

        [Test]
        public async Task CreateAsync_UserCreateDTO_AddsNewUser()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO { Email = "newuser@example.com", PasswordHash = "password" };
            var user = new User { Id = 1, Email = "newuser@example.com", IsSuperUser = false, PasswordHash = "hashedpassword" };

            _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserCreateDTO>())).Returns(user);
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>()));

            // Act
            var result = await _service.CreateAsync(userCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo("newuser@example.com"));
            Assert.IsFalse(result.IsSuperUser);
        }

        [Test]
        public void DeleteAsync_Id_RemovesUser()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            // Act
            _service.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Test]
        public void UpdateAsync_UserCreateDTO_UpdatesUser()
        {
            // Arrange
            var userDto = new UserDTO { Email = "updateduser@example.com", IsSuperUser = false };
            var user = new User { Id = 1, Email = "updateduser@example.com", IsSuperUser = false };

            _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserDTO>())).Returns(user);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>()));

            // Act
            _service.UpdateAsync(1, userDto);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<User>(u => u.Id == 1 && u.Email == "updateduser@example.com")), Times.Once);
        }

        [Test]
        public void VerifyPassword_PasswordMatches_ReturnsTrue()
        {
            // Arrange
            var password = "password";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Act
            var result = _service.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_PasswordDoesNotMatch_ReturnsFalse()
        {
            // Arrange
            var password = "password";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("differentpassword");

            // Act
            var result = _service.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
