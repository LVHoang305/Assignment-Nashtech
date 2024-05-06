using Microsoft.AspNetCore.Mvc;
using ASPNETCoreMVC.WebApp.Models;
using OfficeOpenXml;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCoreMVC.WebApp.Areas.Nashtech.Controllers
{
    [Area("Nashtech")]
    public class RookiesController : Controller
    {

        public static List<Person> GetDummyData()
        {
            List<Person> persons = new List<Person>
        {
            new Person { FirstName = "Hoang", LastName = "Le Viet", Gender = "Male", DateOfBirth = new DateTime(2002, 5, 30), PhoneNumber = "1234567890", BirthPlace = "Ha Noi", IsGraduated = true },
            new Person { FirstName = "Giang", LastName = "Le", Gender = "Female", DateOfBirth = new DateTime(2007, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
            new Person { FirstName = "Phu", LastName = "Le", Gender = "Male", DateOfBirth = new DateTime(2000, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
            new Person { FirstName = "Mao", LastName = "Xam", Gender = "Female", DateOfBirth = new DateTime(1997, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
            new Person { FirstName = "Hac", LastName = "Cau", Gender = "Male", DateOfBirth = new DateTime(1999, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
            new Person { FirstName = "Chau", LastName = "Minhh", Gender = "Female", DateOfBirth = new DateTime(2000, 10, 20), PhoneNumber = "9876543210", BirthPlace = "Ha Noi", IsGraduated = false },
        };

            return persons;
        }

        // GET: /<controller>/
        private List<Person> persons = GetDummyData();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MaleMembers()
        {
            var maleMembers = persons.Where(p => p.Gender == "Male").ToList();
            return View(maleMembers);
        }

        public IActionResult OldestMember()
        {
            var oldestMember = persons.OrderBy(p => p.DateOfBirth).FirstOrDefault();
            return View(oldestMember);
        }

        public IActionResult FullNameList()
        {
            var fullNameList = persons.Select(p => $"{p.LastName} {p.FirstName}").ToList();
            return View(fullNameList);
        }

        public IActionResult MembersByBirthYear()
        {
            var filteredMembersEqual = persons.Where(p => p.DateOfBirth.Year == 2000).ToList();
            var filteredMembersGreaterThan = persons.Where(p => p.DateOfBirth.Year > 2000).ToList();
            var filteredMembersLessThan = persons.Where(p => p.DateOfBirth.Year < 2000).ToList();

            ViewBag.Year = 2000;

            var viewModel = new MembersByBirthYearViewModel
            {
                FilteredMembersEqual = filteredMembersEqual,
                FilteredMembersGreaterThan = filteredMembersGreaterThan,
                FilteredMembersLessThan = filteredMembersLessThan
            };

            return View(viewModel);
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Persons");

                worksheet.Cells[1, 1].Value = "FirstName";
                worksheet.Cells[1, 2].Value = "LastName";
                worksheet.Cells[1, 3].Value = "Gender";
                worksheet.Cells[1, 4].Value = "DateOfBirth";
                worksheet.Cells[1, 5].Value = "PhoneNumber";
                worksheet.Cells[1, 6].Value = "BirthPlace";
                worksheet.Cells[1, 7].Value = "IsGraduated";

                int row = 2;
                foreach (var person in persons)
                {
                    worksheet.Cells[row, 1].Value = person.FirstName;
                    worksheet.Cells[row, 2].Value = person.LastName;
                    worksheet.Cells[row, 3].Value = person.Gender;
                    worksheet.Cells[row, 4].Value = person.DateOfBirth;
                    worksheet.Cells[row, 5].Value = person.PhoneNumber;
                    worksheet.Cells[row, 6].Value = person.BirthPlace;
                    worksheet.Cells[row, 7].Value = person.IsGraduated;
                    row++;
                }

                MemoryStream stream = new MemoryStream();
                excelPackage.SaveAs(stream);

                Response.Headers.Add("Content-Disposition", "attachment; filename=Persons.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream.ToArray(), Response.ContentType, "Persons.xlsx");
            }
        }
    }
}

