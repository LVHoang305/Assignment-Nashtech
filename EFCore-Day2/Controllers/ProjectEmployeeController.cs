using Microsoft.AspNetCore.Mvc;
using EFCore.Data;
using EFCore.Models;
using EFCore.Services;

namespace EFCore_Day1.Controllers
{
    [Route("api/projectemployee")]
    [ApiController]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly EFCoreContext _context;
        private readonly IProjectEmployeeService _projectEmployeeService;

        public ProjectEmployeeController(EFCoreContext context, IProjectEmployeeService projectEmployeeService)
        {
            _context = context;
            _projectEmployeeService = projectEmployeeService;
        }

        // GET: api/ProjectEmployee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project_Employee>>> GetProjectEmployees()
        {
            return await _projectEmployeeService.GetProjectEmployee();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project_Employee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject_Employee(int id, Project_EmployeeDTO project_Employee)
        {
            ValidationResult validationResult = _projectEmployeeService.ValidationProjectEmployee(id, project_Employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _projectEmployeeService.UpdateProjectEmployee(id,project_Employee);

            return CreatedAtAction(nameof(GetProjectEmployees), _context.Departments);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="project_Employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Project_Employee>> CreateProject_Employee(Project_EmployeeDTO project_Employee)
        {
            ValidationResult validationResult = _projectEmployeeService.ValidationProjectEmployee(1, project_Employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            var newProjectEmployee = _projectEmployeeService.CreateProjectEmployee(project_Employee);

            return CreatedAtAction(nameof(GetProjectEmployees), newProjectEmployee);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject_Employee(int id)
        {
            ValidationResult validationResult = _projectEmployeeService.ValidationProjectEmployee(id, new Project_EmployeeDTO { ProjectId = 1, EmployeeId = 1, Enable = true });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _projectEmployeeService.DeleteProjectEmployee(id);

            return CreatedAtAction(nameof(GetProjectEmployees), _context.Departments);
        }
    }
}
