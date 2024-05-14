using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public interface IEmployeesService
    {
        public Task<ActionResult<IEnumerable<Employees>>> GetEmployees();
        public Employees GetEmployees(int id);
        public Employees CreateEmployees(EmployeesDTO employees);
        public void UpdateEmployees(int id, EmployeesDTO employees);
        public void DeleteEmployees(int id);
        public ValidationResult ValidationEmployees(int id, EmployeesDTO employees);
        public Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartment();
        public Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjects();
        public Task<ActionResult<IEnumerable<object>>> GetEmployeesWithSalaryAndJoinedDate();

        public Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjectsSQL();
        public Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartmentSQL();
        public Task<ActionResult<IEnumerable<Employees>>> GetEmployeesWithSalaryAndJoinedDateSQL();





    }
}