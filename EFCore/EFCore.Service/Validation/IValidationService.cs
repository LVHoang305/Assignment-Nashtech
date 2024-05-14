using EFCore.Models;

namespace EFCore.Services
{
    public interface IValidationService
    {
        public ValidationResult ValidationName(string name);
        public ValidationResult ValidationDate(string time);
        public ValidationResult ValidationId(int id, int type);
        public ValidationResult ValidationSalary(int salary);
    }
}