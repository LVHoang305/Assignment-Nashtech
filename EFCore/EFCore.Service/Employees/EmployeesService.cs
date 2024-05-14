using EFCore.Repository;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly EFCoreContext _context;
        private readonly IValidationService _validationService;


        public EmployeesService(EFCoreContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;

        }
        /// <summary>
        /// Get all Departments Service
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            return  await _context.Employees.Include(x => x.Department).Include(y => y.Salary).Include(z => z.ProjectEmployee).ToListAsync();
        }

        /// <summary>
        /// Get 1 Department Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employees GetEmployees(int id)
        {
            var employee = _context.Employees.Find(id);

            return employee;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public void UpdateEmployees(int id, EmployeesDTO employees)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                employee.Name = employees.Name;
                employee.DepartmentId = employees.DepartmentId;
                employee.JoinedDate = Convert.ToDateTime(employees.JoinedDate);
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
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public Employees CreateEmployees(EmployeesDTO employees)
        {
            var newEmployees = new Employees
            {
                Name = employees.Name,
                DepartmentId = employees.DepartmentId,
                JoinedDate = Convert.ToDateTime(employees.JoinedDate)
        };
            _context.Employees.Add(newEmployees);
            _context.SaveChanges();

            return newEmployees;
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public void DeleteEmployees(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public ValidationResult ValidationEmployees(int id, EmployeesDTO employees)
        {
            ValidationResult validationResult = new ValidationResult
            {
                IsValid = true,
                Message = ""
            };

            ValidationResult validationIdResult = _validationService.ValidationId(id, 3);
            if (!validationIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationIdResult.Message;
            }

            ValidationResult validationNameResult = _validationService.ValidationName(employees.Name);
            if (!validationNameResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationNameResult.Message;
            }

            ValidationResult validationJoinedDateResult = _validationService.ValidationDate(employees.JoinedDate);
            if (!validationJoinedDateResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationJoinedDateResult.Message;
            }

            ValidationResult validationDepartmentIdResult = _validationService.ValidationId(id, 1);
            if (!validationDepartmentIdResult.IsValid)
            {
                validationResult.Message = validationResult.Message + validationDepartmentIdResult.Message;
            }

            return validationResult;
        }

        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartment()
        {
            var result = await (from e in _context.Employees
                                join d in _context.Departments on e.DepartmentId equals d.Id
                                select new
                                {
                                    e.Id,
                                    e.Name,
                                    DepartmentName = d.Name
                                }).ToListAsync();

            return result;
        }

        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjects()
        {
            var result = await (from e in _context.Employees
                                join pe in _context.ProjectEmployees on e.Id equals pe.EmployeeId into ej
                                from subPe in ej.DefaultIfEmpty()
                                join p in _context.Projects on subPe.ProjectId equals p.Id into pj
                                from subPj in pj.DefaultIfEmpty()
                                select new
                                {
                                    e.Id,
                                    e.Name,
                                    ProjectName = subPj != null ? subPj.Name : null
                                }).ToListAsync();

            return result;
        }

        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithSalaryAndJoinedDate()
        {
            var result = await(from e in _context.Employees
                               join s in _context.Salaries on e.Id equals s.EmployeeId
                               where s.Salary > 100 && e.JoinedDate >= new DateTime(2024, 1, 1)
                               select new
                               {
                                   e.Id,
                                   e.Name,
                                   e.JoinedDate,
                                   Salary = s.Salary
                               }).ToListAsync();

            return result;
        }

        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithProjectsSQL()
        {
            return await _context.Employees
                        .FromSqlRaw("SELECT e.Id, e.Name, p.Name AS ProjectName FROM Employees e LEFT JOIN Project_Employee pe ON e.Id = pe.EmployeeId LEFT JOIN Projects p ON pe.ProjectId = p.Id")
                        .ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithDepartmentSQL()
        {
            return await _context.Employees
                        .FromSqlRaw("SELECT e.Id, e.Name, d.Name AS DepartmentName FROM Employees e INNER JOIN Departments d ON e.DepartmentId = d.Id")
                        .ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployeesWithSalaryAndJoinedDateSQL()
        {
            return await _context.Employees
                        .FromSqlRaw("SELECT * FROM Employees WHERE Id IN (SELECT EmployeeId FROM Salaries WHERE Salary > 100) AND JoinedDate >= '2024-01-01'")
                        .ToListAsync();
        }

    }
}
