using Microsoft.AspNetCore.Mvc;
using EFCore.Data;
using EFCore.Models;
using EFCore.Services;

namespace EFCore.Controllers
{
    [Route("api/salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly EFCoreContext _context;
        private readonly ISalariesService _salariesService;

        public SalariesController(EFCoreContext context, ISalariesService salariesService)
        {
            _context = context;
            _salariesService = salariesService;
        }

        // GET: api/Salaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salaries>>> GetSalaries()
        {
            return await _salariesService.GetSalaries();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salaries"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaries(int id, SalariesDTO salaries)
        {
            ValidationResult validationResult = _salariesService.ValidationSalaries(id, salaries);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _salariesService.UpdateSalary(id, salaries);

            return CreatedAtAction(nameof(GetSalaries), _context.Salaries);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="salaries"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Salaries>> CreateSalaries(SalariesDTO salaries)
        {
            ValidationResult validationResult = _salariesService.ValidationSalaries(1, salaries);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            var newSalaries = _salariesService.CreateSalary(salaries);

            return CreatedAtAction(nameof(GetSalaries), new { id = newSalaries.Id }, newSalaries);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaries(int id)
        {
            ValidationResult validationResult = _salariesService.ValidationSalaries(id, new SalariesDTO { EmployeeId = 1, Salary = 100000 });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _salariesService.DeleteSalary(id);

            return CreatedAtAction(nameof(GetSalaries), _context.Salaries);
        }
    }
}
