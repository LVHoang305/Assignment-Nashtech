using Microsoft.AspNetCore.Mvc;
using EFCore.Data;
using EFCore.Models;
using EFCore.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EFCoreContext _context;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(EFCoreContext context, IEmployeesService employeesService)
        {
            _context = context;
            _employeesService = employeesService;
        }

        /// <summary>
        /// Get all Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            return await _employeesService.GetEmployees();
        }


        /// <summary>
        /// API update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateEmployees(int id, EmployeesDTO employees)
        {
            ValidationResult validationResult = _employeesService.ValidationEmployees(id, employees);

            _employeesService.UpdateEmployees(id, employees);

            return CreatedAtAction(nameof(GetEmployees), _context.Employees);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employees>> CreateEmployees(EmployeesDTO employees)
        {
            ValidationResult validationResult = _employeesService.ValidationEmployees(1, employees);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            var newEmployees = _employeesService.CreateEmployees(employees);

            return CreatedAtAction("GetEmployees", new { id = newEmployees.Id }, newEmployees);
        }

        /// <summary>
        /// API Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            ValidationResult validationResult = _employeesService.ValidationEmployees(id, new EmployeesDTO { Name="A", DepartmentId=1, JoinedDate="2023/05/30"});
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _employeesService.DeleteEmployees(id);

            return CreatedAtAction(nameof(GetEmployees), _context.Employees);
        }

        [HttpGet("employees-with-their-department-name")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartment()
        {
            return await _employeesService.GetEmployeesWithDepartment();
        }

        [HttpGet("employees-with-their-projects")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjects()
        {
            return await _employeesService.GetEmployeesWithProjects();
        }

        [HttpGet("employees-with-salary-and-joineddate")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithSalaryAndJoinedDate()
        {
            return await _employeesService.GetEmployeesWithSalaryAndJoinedDate();
        }


        [HttpGet("employees-with-their-projects-sql")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjectsSQL()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    return await _employeesService.GetEmployeesWithProjectsSQL();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
            }
        }
        [HttpGet("employees-with-department-sql")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartmentSQL()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    return await _employeesService.GetEmployeesWithDepartmentSQL();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
            }
        }

        [HttpGet("employees-with-salary-and-joined-date")]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployeesWithSalaryAndJoinedDateSQL()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    return await _employeesService.GetEmployeesWithSalaryAndJoinedDateSQL();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
            }
        }
    }
}
