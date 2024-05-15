using Microsoft.AspNetCore.Mvc;
using ASPNETCoreMVC.Models;
using OfficeOpenXml;
using ASPNETCoreMVC.Services;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCoreMVC.WebApp.Areas.Nashtech.Controllers
{
    [Area("Nashtech")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;

        public RookiesController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult CreateEdit(int? id)
        {
            if (id == null)
            {
                return View(new Person());
            }
            else
            {
                var person = _personService.GetPersonById(id.Value);
                if (person == null)
                {
                    return NotFound();
                }
                return View(person);
            }
        }

        [HttpPost]
        public IActionResult SavePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                if (person.Id == 0)
                {
                    _personService.Create(person);
                }
                else
                {
                    _personService.Update(person);
                }
                return View("Index");
            }
            return View("CreateEdit", person);
        }

        public IActionResult ListPeople(int page = 1, int pageSize = 2)
        {
            int skip = (page - 1) * pageSize;
            var totalItems = _personService.ListAllPeople().Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var hasPreviousPage = page > 1;
            var hasNextPage = page < totalPages;

            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = hasPreviousPage;
            ViewBag.HasNextPage = hasNextPage;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;

            var people = _personService.ListAllPeople().Skip(skip).Take(pageSize).ToList();
            //var people = _personService.ListAllPeople();
            return View(people);
        }

        [HttpGet]
        public IActionResult PersonDetail(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            _personService.Delete(id);

            TempData["Message"] = $"Person {person.FirstName} {person.LastName} was removed from the list successfully!";

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MaleMembers()
        {
            var maleMembers = _personService.ListAllPeople().Where(p => p.Gender == "Male").ToList();
            return View(maleMembers);
        }

        public IActionResult OldestMember()
        {
            var oldestMember = _personService.ListAllPeople().OrderBy(p => p.DateOfBirth).FirstOrDefault();
            return View(oldestMember);
        }

        public IActionResult FullNameList()
        {
            var fullNameList = _personService.ListAllPeople().Select(p => $"{p.LastName} {p.FirstName}").ToList();
            return View(fullNameList);
        }

        public IActionResult MembersByBirthYear()
        {
            var filteredMembersEqual = _personService.ListAllPeople().Where(p => p.DateOfBirth.Year == 2000).ToList();
            var filteredMembersGreaterThan = _personService.ListAllPeople().Where(p => p.DateOfBirth.Year > 2000).ToList();
            var filteredMembersLessThan = _personService.ListAllPeople().Where(p => p.DateOfBirth.Year < 2000).ToList();

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
                foreach (var person in _personService.ListAllPeople())
                {
                    worksheet.Cells[row, 1].Value = person.FirstName;
                    worksheet.Cells[row, 2].Value = person.LastName;
                    worksheet.Cells[row, 3].Value = person.Gender;
                    worksheet.Cells[row, 4].Value = person.DateOfBirth.ToString();
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

