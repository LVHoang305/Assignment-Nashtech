namespace EFCore.Models
{
	public class Employees
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int DepartmentId { get; set; }
		public DateTime JoinedDate { get; set; }

		public Departments Department { get; set; }
		public ICollection<Project_Employee> ProjectEmployee { get; set; }
		public Salaries Salary { get; set; }
	}
}

