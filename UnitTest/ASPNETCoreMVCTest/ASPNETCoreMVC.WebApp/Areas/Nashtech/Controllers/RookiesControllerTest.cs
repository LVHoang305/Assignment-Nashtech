using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ASPNETCoreMVC.WebApp.Areas.Nashtech.Controllers;
using ASPNETCoreMVC.Services;
using ASPNETCoreMVC.Models;
using System.Reflection;
using OfficeOpenXml;

namespace ASPNETCoreMVCTest
{
    [TestFixture]
    public class RookiesControllerTest
    {
        private Mock<IPersonService> _mockPersonService;
        private RookiesController _controller;


        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _mockPersonService = new Mock<IPersonService>();

            var httpContext = new DefaultHttpContext();
            var response = new Mock<HttpResponse>();
            httpContext.Response.Body = new MemoryStream();
            response.Setup(x => x.Headers).Returns(new HeaderDictionary());
            httpContext.Features.Set<IHttpResponseFeature>(new HttpResponseFeature()
            {
                Headers = new HeaderDictionary()
            });
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            _controller = new RookiesController(_mockPersonService.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext { HttpContext = httpContext}
            };
        }

        [Test]
        public void CreateEdit_IdNull_ViewWithNewPerson()
        {
            // Act
            var result = _controller.CreateEdit(null);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOf<Person>(viewResult.Model);
        }

        [Test]
        public void CreateEdit_IdNotInList_WhenPersonNotFound()
        {
            // Arrange
            _mockPersonService.Setup(service => service.GetPersonById(It.IsAny<int>())).Returns((Person)null);

            // Act
            var result = _controller.CreateEdit(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void CreateEdit_IdInList_ViewWithPerson()
        {
            // Arrange
            var person = new Person { Id = 1 };
            _mockPersonService.Setup(service => service.GetPersonById(1)).Returns(person);

            // Act
            var result = _controller.CreateEdit(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.That(person, Is.EqualTo(viewResult.Model));
            //Assert.AreEqual(person, viewResult.Model);
        }

        [Test]
        public void SavePerson_WhenModelStateIsValidAndIdEqual0_CreateNewPersonAndReturnIndex()
        {
            // Arrange
            var person = new Person { Id = 0 };
            _mockPersonService.Setup(service => service.Create(person));

            // Act
            var result = _controller.SavePerson(person);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            //Assert.AreEqual("Index", viewResult.ViewName);
            _mockPersonService.Verify(service => service.Create(person), Times.Once);
        }

        [Test]
        public void SavePerson_WhenModelStateIsValidAndIdGreaterThan0_UpdatePersonAndReturnIndex()
        {
            // Arrange
            var person = new Person { Id = 1 };
            _mockPersonService.Setup(service => service.Update(person));

            // Act
            var result = _controller.SavePerson(person);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            //Assert.AreEqual("Index", viewResult.ViewName);
            _mockPersonService.Verify(service => service.Update(person), Times.Once);
        }

        [Test]
        public void SavePerson_WhenModelStateIsInvalid_ReturnViewCreateEdit()
        {
            // Arrange
            var person = new Person { Id = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.SavePerson(person);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            //Assert.AreEqual("CreateEdit", viewResult.ViewName);
            //Assert.AreEqual(person, viewResult.Model);
            Assert.That(viewResult.ViewName, Is.EqualTo("CreateEdit"));
            Assert.That(person, Is.EqualTo(viewResult.Model));
        }

        [Test]
        public void ListPeople_PageAndPageSize_ListOfPeople()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 1 },
                new Person { Id = 2 },
                new Person { Id = 3 }
            };
            _mockPersonService.Setup(service => service.ListAllPeople()).Returns(people);

            // Act
            var result = _controller.ListPeople(1, 2);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as List<Person>;
            //Assert.AreEqual(2, model.Count);
            //Assert.AreEqual(1, model[0].Id);
            //Assert.AreEqual(2, model[1].Id);
            Assert.That(2, Is.EqualTo(model.Count));
            Assert.That(1, Is.EqualTo(model[0].Id));
            Assert.That(2, Is.EqualTo(model[1].Id));

        }

        [Test]
        public void PersonDetail_IdNotInList_ReturnsNotFound()
        {
            // Arrange
            _mockPersonService.Setup(service => service.GetPersonById(It.IsAny<int>())).Returns((Person)null);

            // Act
            var result = _controller.PersonDetail(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void PersonDetail_IdInList_ReturnsViewResult()
        {
            // Arrange
            var person = new Person { Id = 1 };
            _mockPersonService.Setup(service => service.GetPersonById(1)).Returns(person);

            // Act
            var result = _controller.PersonDetail(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.That(person, Is.EqualTo(viewResult.Model));
            //Assert.AreEqual(person, viewResult.Model);
        }

        [Test]
        public void Delete_IdNotInList_ReturnsNotFound()
        {
            // Arrange
            _mockPersonService.Setup(service => service.GetPersonById(It.IsAny<int>())).Returns((Person)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_IdInList_DeletePersonAndRedirectsToConfirmation()
        {
            // Arrange
            var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            //_mockController.Setup(p => p.TempData["Message"]).Returns(_controller.TempData["Message"]);
            _mockPersonService.Setup(service => service.GetPersonById(1)).Returns(person);
            _mockPersonService.Setup(service => service.Delete(1));

            // Act
            var result = _controller.Delete(1);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Confirmation"));

            Assert.IsTrue(_controller.TempData.ContainsKey("Message"));
            Assert.That($"Person {person.FirstName} {person.LastName} was removed from the list successfully!", Is.EqualTo(_controller.TempData["Message"]));

            _mockPersonService.Verify(service => service.Delete(1), Times.Once);
        }

        [Test]
        public void MaleMembers_NoCondition_ReturnViewMaleMembers()
        {
            // Arrange
            var maleMembers = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le", Gender = "Male" },
                new Person { Id = 2, FirstName = "Chau", LastName = "Nguyen", Gender = "Female" }
            };
            _mockPersonService.Setup(x => x.ListAllPeople()).Returns(maleMembers.AsQueryable().ToList());

            // Act
            var result = _controller.MaleMembers() as ViewResult;
            var model = result.Model as List<Person>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model.First().FirstName, Is.EqualTo("Hoang"));
            Assert.That(model.First().LastName, Is.EqualTo("Le"));
        }

        [Test]
        public void OldestMember_NoCondition_ReturnsViewOldestMember()
        {
            // Arrange
            var oldestPerson = new Person { Id = 1, FirstName = "Hoang", LastName = "Le", DateOfBirth = new DateTime(2002, 1, 1) };
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le", DateOfBirth = new DateTime(2002, 1, 1) },
                new Person { Id = 2, FirstName = "Chau", LastName = "Nguyen", DateOfBirth = new DateTime(2007, 5, 5) }
            };
            _mockPersonService.Setup(x => x.ListAllPeople()).Returns(people.AsQueryable().ToList());

            // Act
            var result = _controller.OldestMember() as ViewResult;
            var oldestMember = result.Model as Person;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(oldestMember);
            Assert.That(oldestMember.Id, Is.EqualTo(oldestPerson.Id));
        }

        [Test]
        public void FullNameList_NoCondition_ReturnsViewFullNames()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le" },
                new Person { Id = 2, FirstName = "Chau", LastName = "Nguyen" }
            };
            _mockPersonService.Setup(x => x.ListAllPeople()).Returns(people.AsQueryable().ToList());

            // Act
            var result = _controller.FullNameList() as ViewResult;
            var fullNameList = result.Model as List<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(fullNameList);
            Assert.That(fullNameList.Count, Is.EqualTo(2));
            Assert.That(fullNameList[0], Is.EqualTo("Le Hoang"));
            Assert.That(fullNameList[1], Is.EqualTo("Nguyen Chau"));
        }

        [Test]
        public void MembersByBirthYear_NoCondition_ReturnsViewFilteredMembers()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le", DateOfBirth = new DateTime(2000, 1, 1) },
                new Person { Id = 2, FirstName = "Chau", LastName = "Nguyen", DateOfBirth = new DateTime(1995, 5, 5) },
                new Person { Id = 3, FirstName = "Giang", LastName = "Le", DateOfBirth = new DateTime(2005, 10, 10) }
            };
            _mockPersonService.Setup(x => x.ListAllPeople()).Returns(people.AsQueryable().ToList());

            // Act
            var result = _controller.MembersByBirthYear() as ViewResult;
            var viewModel = result.Model as MembersByBirthYearViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(viewModel);
            Assert.That(viewModel.FilteredMembersEqual.Count(), Is.EqualTo(1));
            Assert.That(viewModel.FilteredMembersGreaterThan.Count(), Is.EqualTo(1));
            Assert.That(viewModel.FilteredMembersLessThan.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ExportToExcel_NoCondition_ReturnsFileResult()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Hoang", LastName = "Le", Gender = "Male", DateOfBirth = new DateTime(2002, 1, 1), PhoneNumber = "123456789", BirthPlace = "Ha Noi", IsGraduated = true },
                new Person { Id = 2, FirstName = "Chau", LastName = "Nguyen", Gender = "Female", DateOfBirth = new DateTime(2003, 5, 5), PhoneNumber = "987654321", BirthPlace = "Ha Noi", IsGraduated = false }
            };
            _mockPersonService.Setup(x => x.ListAllPeople()).Returns(people.AsQueryable().ToList());

            // Act
            var result = _controller.ExportToExcel() as FileContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ContentType, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            Assert.That(result.FileDownloadName, Is.EqualTo("Persons.xlsx"));

            var fileContents = result.FileContents;
            Assert.IsNotNull(fileContents);

            using (var memoryStream = new MemoryStream(fileContents))
            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                Assert.IsNotNull(worksheet);


                Assert.That(worksheet.Cells[1, 1].Value, Is.EqualTo("FirstName"));
                Assert.That(worksheet.Cells[1, 2].Value, Is.EqualTo("LastName"));
                Assert.That(worksheet.Cells[1, 3].Value, Is.EqualTo("Gender"));
                Assert.That(worksheet.Cells[1, 4].Value, Is.EqualTo("DateOfBirth"));
                Assert.That(worksheet.Cells[1, 5].Value, Is.EqualTo("PhoneNumber"));
                Assert.That(worksheet.Cells[1, 6].Value, Is.EqualTo("BirthPlace"));
                Assert.That(worksheet.Cells[1, 7].Value, Is.EqualTo("IsGraduated"));

                Assert.That(worksheet.Cells[2, 1].Value, Is.EqualTo("Hoang"));
                Assert.That(worksheet.Cells[2, 2].Value, Is.EqualTo("Le"));
                Assert.That(worksheet.Cells[2, 3].Value, Is.EqualTo("Male"));
                Assert.That(worksheet.Cells[2, 4].Value, Is.EqualTo(new DateTime(2002, 1, 1).ToString()));
                Assert.That(worksheet.Cells[2, 5].Value, Is.EqualTo("123456789"));
                Assert.That(worksheet.Cells[2, 6].Value, Is.EqualTo("Ha Noi"));
                Assert.That(worksheet.Cells[2, 7].Value, Is.EqualTo(true));

                Assert.That(worksheet.Cells[3, 1].Value, Is.EqualTo("Chau"));
                Assert.That(worksheet.Cells[3, 2].Value, Is.EqualTo("Nguyen"));
                Assert.That(worksheet.Cells[3, 3].Value, Is.EqualTo("Female"));
                Assert.That(worksheet.Cells[3, 4].Value, Is.EqualTo(new DateTime(2003, 5, 5).ToString()));
                Assert.That(worksheet.Cells[3, 5].Value, Is.EqualTo("987654321"));
                Assert.That(worksheet.Cells[3, 6].Value, Is.EqualTo("Ha Noi"));
                Assert.That(worksheet.Cells[3, 7].Value, Is.EqualTo(false));
            }
        }
    }
}
