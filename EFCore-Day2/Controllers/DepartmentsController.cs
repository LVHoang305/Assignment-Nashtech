using Microsoft.AspNetCore.Mvc;
using EFCore.Data;
using EFCore.Models;
using EFCore.Services;

namespace EFCore.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly EFCoreContext _context;
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(EFCoreContext context, IDepartmentsService departmentsService)
        {
            _context = context;
            _departmentsService = departmentsService;
        }

        /// <summary>
        /// API get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departments>>> GetDepartments()
        {
            return await _departmentsService.GetDepartments();
        }

        /// <summary>
        /// API update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateDepartments(int id, DepartmentsDTO departments)
        {
            ValidationResult validationResult = _departmentsService.ValidationDepartments(id, departments);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _departmentsService.UpdateDepartments(id, departments);

            return CreatedAtAction(nameof(GetDepartments), _context.Departments);
        }

        /// <summary>
        /// API Create
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Departments>> CreateDepartments(DepartmentsDTO departments)
        {
            ValidationResult validationResult = _departmentsService.ValidationDepartments(1, departments);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            var newDepartments = _departmentsService.CreateDepartments(departments);

            return CreatedAtAction(nameof(GetDepartments), new { id = newDepartments.Id }, newDepartments);
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartments(int id)
        {
            ValidationResult validationResult = _departmentsService.ValidationDepartments(id, new DepartmentsDTO { Name = "random" });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _departmentsService.DeleteDepartments(id);

            return CreatedAtAction(nameof(GetDepartments), _context.Departments);
        }
    }
}
