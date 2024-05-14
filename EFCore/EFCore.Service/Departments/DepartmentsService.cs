using EFCore.Repository;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly EFCoreContext _context;
        private readonly IValidationService _validationService;

        public DepartmentsService(EFCoreContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }
        /// <summary>
        /// Get all Departments Service
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Departments>>> GetDepartments()
        {
            return  await _context.Departments.Include(x => x.Employees).ToListAsync();
        }

        /// <summary>
        /// Get 1 Department Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Departments GetDepartment(int id)
        {
            var departments = _context.Departments.Find(id);

            return departments;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public void UpdateDepartments(int id, DepartmentsDTO departments)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.Name = departments.Name;
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
        /// <param name="departments"></param>
        /// <returns></returns>
        [HttpPost]
        public Departments CreateDepartments(DepartmentsDTO departments)
        {
            var newDepartments = new Departments
            {
                Name = departments.Name
            };
            _context.Departments.Add(newDepartments);
            _context.SaveChanges();

            return newDepartments;
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public void DeleteDepartments(int id)
        {
            var departments = _context.Departments.Find(id);
            if (departments != null)
            {
                _context.Departments.Remove(departments);
                _context.SaveChanges();
            }
        }

        public ValidationResult ValidationDepartments(int id, DepartmentsDTO departments)
        {
            ValidationResult validationResult = new ValidationResult
            {
                IsValid = true,
                Message = ""
            };

            ValidationResult validationIdResult = _validationService.ValidationId(id, 1);
            if (!validationIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationIdResult.Message;
            }

            ValidationResult validationNameResult = _validationService.ValidationName(departments.Name);
            if (!validationNameResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationNameResult.Message;
            }

            return validationResult;
        }

    }
}
