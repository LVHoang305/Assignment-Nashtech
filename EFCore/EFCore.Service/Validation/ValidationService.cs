using EFCore.Repository;
using EFCore.Models;

namespace EFCore.Services
{
    public class ValidationService : IValidationService
    {
        private static ValidationResult _result = new ValidationResult();

        private readonly EFCoreContext _context;

        public ValidationService(EFCoreContext context)
        {
            _context = context;
        }

        public ValidationResult ValidationName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _result.Message = _result.Message + "Invalid Name. ";
                _result.IsValid = false;
            }
            return _result; 
        }

        public ValidationResult ValidationDate(string time)
        {
            DateTime date;
            if (!DateTime.TryParse(time, out date))
            {
                _result.Message = _result.Message + "Invalid Joined Date. ";
                _result.IsValid = false;
            }
            return _result;
        }

        public ValidationResult ValidationId(int id, int type)
        {
            int max =  0;
            string name = "";
            switch (type)
            {
                case 1:
                    {
                        max = _context.Departments.Max(e => e.Id);
                        name = "Departments Id";
                        break;
                    }

                case 2:
                    {
                        max = _context.Projects.Max(e => e.Id);
                        name = "Projects Id";
                        break;
                    }

                case 3:
                    {
                        max = _context.Employees.Max(e => e.Id);
                        name = "Employee Id";
                        break;
                    }
            }
            if ((id <= 0) || (id > max))
            {
                _result.Message = _result.Message + "Invalid " + name;
                _result.IsValid = false;
            }
            return _result;
        }

        public ValidationResult ValidationSalary(int salary)
        {
            if (salary <= 0)
            {
                _result.Message = _result.Message + "Invalid Salary";
                _result.IsValid = false;
            }
            return _result;
        }

    }
}