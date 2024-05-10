using EFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Services
{
    public interface IDepartmentsService
    {
        public Task<ActionResult<IEnumerable<Departments>>> GetDepartments();
        public Departments GetDepartment(int id);
        public Departments CreateDepartments(DepartmentsDTO departments);
        public void UpdateDepartments(int id, DepartmentsDTO departments);
        public void DeleteDepartments(int id);
        public ValidationResult ValidationDepartments(int id, DepartmentsDTO departments);
    }
}