using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASPNETCoreMVC.Models;
using ASPNETCoreMVC.Services;
using NUnit.Framework;
using OfficeOpenXml;

namespace YourProjectName.Tests.Services
{
    [TestFixture]
    public class PersonServiceTests
    {
        private IPersonService _personService;
        private string _testFilePath;

        [SetUp]
        public void Setup()
        {
            _testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestPersons.xlsx");
            File.Copy("Users/tuanbeo/Projects/UnitTest/ASPNETCoreMVC-Day2/ASPNetCoreMVC.Models/Persons.xlsx", _testFilePath, true);
            _personService = new PersonService(_testFilePath);
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(_testFilePath);
        }

        [Test]
        public void Create_Person_AddsNewPerson()
        {
            // Arrange
            var person = new Person
            {
                FirstName = "Test",
                LastName = "Person",
                Gender = "Male",
                DateOfBirth = new DateTime(2002, 1, 1),
                PhoneNumber = "123456789",
                BirthPlace = "RandomPlace",
                IsGraduated = true
            };

            // Act
            _personService.Create(person);

            // Assert
            var createdPerson = GetPersonFromExcel(person.FirstName, person.LastName);
            Assert.IsNotNull(createdPerson);
            Assert.That(createdPerson.FirstName, Is.EqualTo(person.FirstName));
            Assert.That(createdPerson.LastName, Is.EqualTo(person.LastName));
            Assert.That(createdPerson.Gender, Is.EqualTo(person.Gender));
            Assert.That(createdPerson.DateOfBirth, Is.EqualTo(person.DateOfBirth));
            Assert.That(createdPerson.PhoneNumber, Is.EqualTo(person.PhoneNumber));
            Assert.That(createdPerson.BirthPlace, Is.EqualTo(person.BirthPlace));
            Assert.That(createdPerson.IsGraduated, Is.EqualTo(person.IsGraduated));
        }

        [Test]
        public void Update_Person_UpdatesExistingPerson()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = "Updated",
                LastName = "Person",
                Gender = "Female",
                DateOfBirth = new DateTime(2003, 5, 5),
                PhoneNumber = "987654321",
                BirthPlace = "RandomPlace",
                IsGraduated = false
            };

            // Act
            _personService.Update(person);

            // Assert
            var updatedPerson = GetPersonFromExcel(person.Id);
            Assert.IsNotNull(updatedPerson);
            Assert.That(updatedPerson.FirstName, Is.EqualTo(person.FirstName));
            Assert.That(updatedPerson.LastName, Is.EqualTo(person.LastName));
            Assert.That(updatedPerson.Gender, Is.EqualTo(person.Gender));
            Assert.That(updatedPerson.DateOfBirth, Is.EqualTo(person.DateOfBirth));
            Assert.That(updatedPerson.PhoneNumber, Is.EqualTo(person.PhoneNumber));
            Assert.That(updatedPerson.BirthPlace, Is.EqualTo(person.BirthPlace));
            Assert.That(updatedPerson.IsGraduated, Is.EqualTo(person.IsGraduated));
        }

        [Test]
        public void Delete_Id_RemovesExistingPerson()
        {
            // Arrange
            int idToDelete = 1;

            // Act
            _personService.Delete(idToDelete);

            // Assert
            var deletedPerson = GetPersonFromExcel(idToDelete);
            Assert.IsNull(deletedPerson);
        }

        [Test]
        public void GetPersonById_IdInList_ReturnsCorrectPerson()
        {
            // Arrange
            int idToRetrieve = 1;

            // Act
            var person = _personService.GetPersonById(idToRetrieve);

            // Assert
            Assert.IsNotNull(person);
            Assert.That(person.Id, Is.EqualTo(idToRetrieve));
        }

        [Test]
        public void GetPersonById_IdNotInList_ReturnsNull()
        {
            // Arrange
            int nonExistentId = 9999;

            // Act
            var person = _personService.GetPersonById(nonExistentId);

            // Assert
            Assert.IsNull(person);
        }

        [Test]
        public void ListAllPeople_NoConditon_ReturnsListOfPeople()
        {
            // Act
            var people = _personService.ListAllPeople();

            // Assert
            Assert.IsNotNull(people);
            Assert.IsInstanceOf<List<Person>>(people);
            Assert.That(people.Count, Is.EqualTo(2));
        }

        private Person GetPersonFromExcel(int id)
        {
            using (var package = new ExcelPackage(new FileInfo(_testFilePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (Convert.ToInt32(worksheet.Cells[row, 1].Value) == id)
                    {
                        return new Person
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                            FirstName = worksheet.Cells[row, 2].Value.ToString(),
                            LastName = worksheet.Cells[row, 3].Value.ToString(),
                            Gender = worksheet.Cells[row, 4].Value.ToString(),
                            DateOfBirth = Convert.ToDateTime(worksheet.Cells[row, 5].Value),
                            PhoneNumber = worksheet.Cells[row, 6].Value.ToString(),
                            BirthPlace = worksheet.Cells[row, 7].Value.ToString(),
                            IsGraduated = Convert.ToBoolean(worksheet.Cells[row, 8].Value)
                        };
                    }
                }
            }

            return null;
        }

        private Person GetPersonFromExcel(string firstName, string lastName)
        {
            using (var package = new ExcelPackage(new FileInfo(_testFilePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 2].Value.ToString() == firstName && worksheet.Cells[row, 3].Value.ToString() == lastName)
                    {
                        return new Person
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                            FirstName = worksheet.Cells[row, 2].Value.ToString(),
                            LastName = worksheet.Cells[row, 3].Value.ToString(),
                            Gender = worksheet.Cells[row, 4].Value.ToString(),
                            DateOfBirth = Convert.ToDateTime(worksheet.Cells[row, 5].Value),
                            PhoneNumber = worksheet.Cells[row, 6].Value.ToString(),
                            BirthPlace = worksheet.Cells[row, 7].Value.ToString(),
                            IsGraduated = Convert.ToBoolean(worksheet.Cells[row, 8].Value)
                        };
                    }
                }
            }

            return null;
        }
    }
}
