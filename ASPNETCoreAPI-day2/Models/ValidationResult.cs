using System;
namespace ASPNETCoreAPI_day2.Models
{
	public class ValidationResult
	{
		public bool IsValid { get; set; } = true;
		public string msg { get; set; } = "";
	}
}

