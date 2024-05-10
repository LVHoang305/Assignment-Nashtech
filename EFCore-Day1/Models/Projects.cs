namespace EFCore.Models
{
	public class Projects
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Project_Employee> ProjectEmployees { set; get; }
	}
}

