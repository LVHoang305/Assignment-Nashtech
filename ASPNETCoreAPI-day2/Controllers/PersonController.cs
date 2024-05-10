using ASPNETCoreAPI_day2.Models;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreAPI_day2.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCoreAPI_day2.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IValidationPersonService _validationPersonService;
        private readonly List<Person> people;

        public PersonController(IPersonService personService, IValidationPersonService validationPersonService)
        {
            _personService = personService;
            _validationPersonService = validationPersonService;
            people = _personService.ListAllPeople();
        }

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return Ok(people);
        }

        [HttpPost]
        public IActionResult AddPerson(ViewPerson person)
        {
            ValidationResult validationResult = _validationPersonService.ValidationPerson(person);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }
            _personService.Add(person);
            return CreatedAtAction(nameof(GetAllPeople), person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, ViewPerson person)
        {
            if ((id <= 0) || (id > people.Count))
            {
                return BadRequest("Invalid ID");
            }

            ValidationResult validationResult = _validationPersonService.ValidationPerson(person);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }
            _personService.Update(id, person);
            return CreatedAtAction(nameof(GetAllPeople), person);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            if ((id <= 0) || (id > people.Count))
            {
                return BadRequest("Invalid ID.");
            }
            _personService.Delete(id);
            return CreatedAtAction(nameof(GetAllPeople), people);
        }
        /// <summary>
        /// Filter by params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="birthPlace"></param>
        /// <returns></returns>
        [HttpGet("filtration")]
        public IActionResult FilterPeople(string? name = null, string? gender = null, string? birthPlace = null)
        {
            List<Person> filtered = _personService.Filter(name, gender, birthPlace);
            return CreatedAtAction(nameof(GetAllPeople), filtered);
        }

        /// <summary>
        /// Filter by JSON
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("filtration")]
        public IActionResult FilterPeople([FromBody] Filter filter)
        {
            List<Person> filtered = _personService.Filter(filter.Name, filter.Gender, filter.BirthPlace);
            return CreatedAtAction(nameof(GetAllPeople), filtered);
        }

    }
}

