namespace EFCore.Models
{
	public class Project_Employee
	{
		public int ProjectId { get; set; }
		public int EmployeeId { get; set; }
		public bool Enable { get; set; }

		public Projects Project { get; set; }
		public Employees Employee { get; set; }
	}
}

