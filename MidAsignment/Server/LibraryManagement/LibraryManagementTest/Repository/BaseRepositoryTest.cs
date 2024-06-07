using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;
using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace LibraryManagement.Tests.Repository
{
    [TestFixture]
    public class BaseRepositoryTests
    {
        private LibraryManagementContext _context;
        private DbSet<Book> _dbSet;
        private BaseRepository<Book> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LibraryManagementContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new LibraryManagementContext(options);
            _dbSet = _context.Set<Book>();
            _repository = new BaseRepository<Book>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_NoInput_ReturnsAllEntities()
        {
            // Arrange
            _dbSet.Add(new Book { Id = 1, Title = "Book1", Author = "Author1" });
            _dbSet.Add(new Book { Id = 2, Title = "Book2", Author = "Author2" });
            await _context.SaveChangesAsync();

            // Act
            var entities = await _repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(entities);
            Assert.That(entities.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByFieldsLikeAsync_Json_ReturnsMatchingEntities()
        {
            // Arrange
            _context.Books.Add(new Book { Id = 1, Title = "Book1", Author = "Author1" });
            _context.Books.Add(new Book { Id = 2, Title = "Book2", Author = "Author2" });
            await _context.SaveChangesAsync();

            // Act
            var entities = await _repository.GetByFieldsLikeAsync("{ 'Author': 'Author' }");

            // Assert
            Assert.That(entities.Count(), Is.EqualTo(2));
            Assert.IsNotNull(entities);
        }

        [Test]
        public async Task GetByFieldsAsync_Json_ReturnsEntitiesMatchingCriteria()
        {
            // Arrange
            var testData = new List<Book>
            {
                new Book { Id = 3, Title = "Entity1", Author = "A" },
                new Book { Id = 4, Title = "Entity2", Author = "B" },
                new Book { Id = 5, Title = "Entity3", Author = "A" },
            };
            _context.Books.AddRange(testData);
            _context.SaveChanges();

            var filterJson = "{\"Author\":\"A\"}";

            // Act
            var result = await _repository.GetByFieldsAsync(filterJson);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.IsTrue(result.All(e => e.Author == "A"));
        }

        [Test]
        public async Task GetByIdAsync_Id_ReturnsEntityWithGivenId()
        {
            // Arrange
            var testData = new List<Book>
            {
                new Book { Id = 6, Title = "Entity1", Author = "A" },
                new Book { Id = 7, Title = "Entity2" , Author = "A"},
                new Book { Id = 8, Title = "Entity3" , Author = "A"},
            };
            _context.Books.AddRange(testData);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetByIdAsync(7);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(7));
        }

        [Test]
        public async Task CreateAsync_Book_AddsEntityToDatabase()
        {
            // Arrange
            var entity = new Book { Id = 9, Title = "Entity1", Author = "A" };

            // Act
            _repository.CreateAsync(entity);

            // Assert
            Assert.That(_context.Books.Count(), Is.EqualTo(1));

        }

        [Test]
        public async Task DeleteAsync_Id_RemovesEntityFromDatabase()
        {
            // Arrange
            var testData = new List<Book>
            {
                new Book { Id = 1, Title = "Entity1", Author="A" },
                new Book { Id = 2, Title = "Entity2", Author="A" },
            };
            _context.Books.AddRange(testData);
            _context.SaveChanges();

            // Act
            _repository.DeleteAsync(1);

            // Assert
            Assert.That(_context.Books.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateAsync_Book_UpdatesEntityInDatabase()
        {
            // Arrange
            var entity = new Book { Id = 1, Title = "Entity1", Author="A" };
            _context.Books.Add(entity);
            _context.SaveChanges();

            // Act
            entity.Title = "UpdatedEntity";
            _repository.UpdateAsync(entity);

            // Assert
            var updatedEntity = await _context.Books.FindAsync(1);
            Assert.IsNotNull(updatedEntity);
            Assert.That(updatedEntity.Title, Is.EqualTo("UpdatedEntity"));
        }

        [Test]
        public async Task DeleteByFieldAsync_FieldAndValue_RemovesEntitiesWithMatchingField()
        {
            // Arrange
            _context.Books.Add(new Book { Id = 1, Title = "Book1", Author = "Author1" });
            _context.Books.Add(new Book { Id = 2, Title = "Book2", Author = "Author1" });
            _context.Books.Add(new Book { Id = 3, Title = "Book3", Author = "Author2" });
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteByFieldAsync("Author", "Author1");
            var remainingBooks = await _context.Books.ToListAsync();

            // Assert
            Assert.IsNotNull(remainingBooks);
            Assert.That(remainingBooks.First().Title, Is.EqualTo("Book3"));
            Assert.That(remainingBooks.Count(), Is.EqualTo(1));
        }
    }
}
