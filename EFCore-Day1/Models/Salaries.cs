namespace EFCore.Models
{
	public class Salaries
	{
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public int Salary { get; set; }

		public Employees Employee { get; set; }
	}
}

