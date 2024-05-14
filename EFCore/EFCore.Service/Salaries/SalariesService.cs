using EFCore.Repository;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public class SalariesService : ISalariesService
    {
        private readonly EFCoreContext _context;
        private readonly IValidationService _validationService;

        public SalariesService(EFCoreContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;

        }
        /// <summary>
        /// Get all Salaries Service
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Salaries>>> GetSalaries()
        {
            return  await _context.Salaries.Include(x => x.Employee).ToListAsync();
        }

        /// <summary>
        /// Get 1 Department Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Salaries GetSalaries(int id)
        {
            var Salary = _context.Salaries.Find(id);

            return Salary;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Salaries"></param>
        /// <returns></returns>
        public void UpdateSalary(int id, SalariesDTO salary)
        {
            var editSalary = _context.Salaries.Find(id);
            if (editSalary != null)
            {
                editSalary.EmployeeId = salary.EmployeeId;
                editSalary.Salary = salary.Salary;
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
        /// <param name="Salaries"></param>
        /// <returns></returns>
        [HttpPost]
        public Salaries CreateSalary(SalariesDTO salaries)
        {
            var newSalaries = new Salaries
            {
                EmployeeId = salaries.EmployeeId,
                Salary = salaries.Salary
            };
            _context.Salaries.Add(newSalaries);
            _context.SaveChanges();

            return newSalaries;
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public void DeleteSalary(int id)
        {
            var salary = _context.Salaries.Find(id);
            if (salary != null)
            {
                _context.Salaries.Remove(salary);
                _context.SaveChanges();
            }
        }
        public ValidationResult ValidationSalaries(int id, SalariesDTO salary)
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
                validationResult.Message = validationResult.Message + "Invalid Id. ";
            }

            ValidationResult validationEmployeeIdResult = _validationService.ValidationId(salary.EmployeeId,3);
            if (!validationEmployeeIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationEmployeeIdResult.Message;
            }

            ValidationResult validationNameResult = _validationService.ValidationSalary(salary.Salary);
            if (!validationNameResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationNameResult.Message;
            }

            return validationResult;
        }

    }
}
