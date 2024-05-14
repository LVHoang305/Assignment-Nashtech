using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public interface IProjectEmployeeService
    {
        public Task<ActionResult<IEnumerable<Project_Employee>>> GetProjectEmployee();
        public Project_Employee GetProjectEmployee(int id);
        public Project_Employee CreateProjectEmployee(Project_EmployeeDTO projectEmployee);
        public void UpdateProjectEmployee(int id, Project_EmployeeDTO projectEmployee);
        public void DeleteProjectEmployee(int id);
        public ValidationResult ValidationProjectEmployee(int id, Project_EmployeeDTO projectEmployee);

    }
}