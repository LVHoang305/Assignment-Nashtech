using EFCore.Data;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public class ProjectEmployeesService : IProjectEmployeeService
    {
        private readonly EFCoreContext _context;
        private readonly IValidationService _validationService;

        public ProjectEmployeesService(EFCoreContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }
        /// <summary>
        /// Get all Departments Service
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Project_Employee>>> GetProjectEmployee()
        {
            return  await _context.ProjectEmployees.Include(x => x.Employee).ToListAsync();
        }

        /// <summary>
        /// Get 1 Department Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project_Employee GetProjectEmployee(int id)
        {
            var projectEmployee = _context.ProjectEmployees.Find(id);

            return projectEmployee;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProjectEmployee"></param>
        /// <returns></returns>
        public void UpdateProjectEmployee(int id, Project_EmployeeDTO projectEmployee)
        {
            var newProjectEmployee = _context.ProjectEmployees.Find(id);
            if (newProjectEmployee != null)
            {
                newProjectEmployee.EmployeeId = projectEmployee.EmployeeId;
                newProjectEmployee.ProjectId = projectEmployee.ProjectId;
                newProjectEmployee.Enable = projectEmployee.Enable;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
        }

        /// <summary>
        /// API Create
        /// </summary>
        /// <param name="Project_Employee"></param>
        /// <returns></returns>
        [HttpPost]
        public Project_Employee CreateProjectEmployee(Project_EmployeeDTO projectEmployee)
        {
            var newProjectEmployee = new Project_Employee
            {
                EmployeeId = projectEmployee.EmployeeId,
                ProjectId = projectEmployee.ProjectId,
                Enable = projectEmployee.Enable
            };
            _context.ProjectEmployees.Add(newProjectEmployee);
            _context.SaveChanges();

            return newProjectEmployee;
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public void DeleteProjectEmployee(int id)
        {
            var projectEmployee = _context.ProjectEmployees.Find(id);
            if (projectEmployee != null)
            {
                _context.ProjectEmployees.Remove(projectEmployee);
                _context.SaveChanges();
            }
        }

        public ValidationResult ValidationProjectEmployee(int id, Project_EmployeeDTO projectEmployee)
        {
            ValidationResult validationResult = new ValidationResult
            {
                IsValid = true,
                Message = ""
            };

            ValidationResult validationIdResult = _validationService.ValidationSalary(id);
            if (!validationIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationIdResult.Message;
            }

            ValidationResult validationProjectIdResult = _validationService.ValidationId(projectEmployee.ProjectId, 2);
            if (!validationProjectIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationProjectIdResult.Message;
            }

            ValidationResult validationEmployeeIdResult = _validationService.ValidationId(projectEmployee.EmployeeId, 3);
            if (!validationEmployeeIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationEmployeeIdResult.Message;
            }


            return validationResult;
        }
    }
}
