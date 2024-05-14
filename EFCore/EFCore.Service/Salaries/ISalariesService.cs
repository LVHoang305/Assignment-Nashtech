using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public interface ISalariesService
    {
        public Task<ActionResult<IEnumerable<Salaries>>> GetSalaries();
        public Salaries CreateSalary(SalariesDTO salary);
        public void UpdateSalary(int id, SalariesDTO salary);
        public void DeleteSalary(int id);
        public ValidationResult ValidationSalaries(int id, SalariesDTO salary);

    }
}